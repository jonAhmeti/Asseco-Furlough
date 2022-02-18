using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security;
using System.Security.Cryptography;

namespace Furlough.SecurityHandlers
{
    public class PasswordHasher
    {
        private readonly byte[] _salt = new byte[128 / 8];
        private readonly string _saltString;
        private readonly string _password;
        private readonly Random _random = new Random();
        private readonly string _hashedPassword;

        public static readonly char[] allowedSymbols = "!@#$%^&*()_-+=[{]};:>|./?".ToCharArray();
        public PasswordHasher(string password)
        {
            _random.NextBytes(_salt);
            _saltString = Convert.ToBase64String(_salt);
            _password = password;

            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
           _hashedPassword = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: _password,
                salt: _salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
        }

        public string GetHashedPassword()
        {
            return _hashedPassword;
        }

        public string GetSalt()
        {
            return _saltString;
        }

        public string GetHashWithSalt(string? salt = null, string? hashedPassword = null)
        {
            salt ??= _saltString; //gives salt variable the value of _saltString only if salt is null
            if (salt.Length != 24)
                throw new NotSupportedException("Salt length must be 24 characters");

            hashedPassword ??= _hashedPassword;
            //adds first half of the salt to the beginning of the hashed password and the second half after
            return string.Concat(salt.AsSpan(0, 12), hashedPassword, salt.AsSpan(12,  12));
        }

        public bool VerifyPassword(string hashedPassword)
        {
            try
            {
                //get salt from hashedPassword
                var salt = new string(hashedPassword[..12].ToArray());
                salt += new string(hashedPassword.Reverse().ToArray()[..12].Reverse().ToArray());

                var loginPasswordHash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: _password,
                    salt: Convert.FromBase64String(salt),
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 100000,
                    numBytesRequested: 256 / 8));

                return GetHashWithSalt(salt, loginPasswordHash) == hashedPassword;
            }
            catch (Exception e)
            {
                string extraMessage = "\n**Reason probably: user's DB password is not long enough to contain the salt"; //salt length should be 24 characters
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message + extraMessage);
                Console.ResetColor();
                return false;
            }
        }

        public static string Generate(int length, int numberOfNonAlphanumericCharacters)
        {
            if (length < 1 || length > 128)
            {
                throw new ArgumentException(nameof(length));
            }

            if (numberOfNonAlphanumericCharacters > length || numberOfNonAlphanumericCharacters < 0)
            {
                throw new ArgumentException(nameof(numberOfNonAlphanumericCharacters));
            }

            using (var rng = RandomNumberGenerator.Create())
            {
                var byteBuffer = new byte[length];

                rng.GetBytes(byteBuffer);

                var count = 0;
                var characterBuffer = new char[length];

                for (var iter = 0; iter < length; iter++)
                {
                    var i = byteBuffer[iter] % 87;

                    if (i < 10)
                    {
                        characterBuffer[iter] = (char)('0' + i);
                    }
                    else if (i < 36)
                    {
                        characterBuffer[iter] = (char)('A' + i - 10);
                    }
                    else if (i < 62)
                    {
                        characterBuffer[iter] = (char)('a' + i - 36);
                    }
                    else
                    {
                        characterBuffer[iter] = allowedSymbols[i - 62];
                        count++;
                    }
                }

                if (count >= numberOfNonAlphanumericCharacters)
                {
                    return new string(characterBuffer);
                }

                int j;
                var rand = new Random();

                for (j = 0; j < numberOfNonAlphanumericCharacters - count; j++)
                {
                    int k;
                    do
                    {
                        k = rand.Next(0, length);
                    }
                    while (!char.IsLetterOrDigit(characterBuffer[k]));

                    characterBuffer[k] = allowedSymbols[rand.Next(0, allowedSymbols.Length)];
                }

                return new string(characterBuffer);
            }
        }
    }
}
