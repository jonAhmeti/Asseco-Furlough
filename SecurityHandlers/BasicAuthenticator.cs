using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

namespace Furlough.SecurityHandlers
{
    public class BasicAuthenticator : AuthenticationHandler<BasicAuthenticatorOptions>
    {
        public BasicAuthenticator(
            IOptionsMonitor<BasicAuthenticatorOptions> options, 
            ILoggerFactory logger, 
            UrlEncoder encoder, 
            ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            throw new NotImplementedException();
        }
    }
}
