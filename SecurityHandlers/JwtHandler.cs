using Furlough.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Furlough.Models.Mapper;

namespace Furlough.SecurityHandlers
{
    public class JwtHandler
    {
        private readonly IConfiguration _config;
        private readonly DAL.Employee _dalEmployee;
        private readonly ViewModelMapper vmMapper = new();

        public JwtHandler(IConfiguration configuration, DAL.Employee dalEmployee)
        {
            _config = configuration;
            _dalEmployee = dalEmployee;
        }

        public string GenerateToken(DAL.Models.User user)
        {
            var employee = vmMapper.EmployeeMap(_dalEmployee.GetByUserId(user.Id));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Token"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims: new Claim[]
                {
                    new Claim("RoleId", user.RoleId.ToString()),
                    new Claim("Email", user.Username),
                    new Claim("Name", employee.Name),
                    new Claim("PositionId", employee.PositionId.ToString()),
                    new Claim("DepartmentId", employee.DepartmentId.ToString())
                },
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
