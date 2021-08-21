using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WEBAPI.IRepository;
using WEBAPI.Models;

namespace WEBAPI.Class
{
    public class AuthenticationClass : IAuthenticate
    {
        private readonly IConfiguration _config;
        private SqlConnection con;
        private readonly string key;
        public AuthenticationClass(IConfiguration config)
        {
            _config = config;
            con = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]);
            key = _config["AuthManager:key"];

        }

        public async Task<ServiceResponse<object>> getUser(string username, string password)
        {
            var service = new ServiceResponse<object>();
            try
            {
                
                var param = new DynamicParameters();
    
                param.Add("username", username);
                param.Add("_password", password);
                param.Add("retval", dbType: DbType.Int32, direction:ParameterDirection.Output);
                var result = await con.QueryAsync<dynamic>("getUser", param, commandType: CommandType.StoredProcedure);
                var retval = param.Get<int>("retval");

                if (retval.Equals(20))
                {
                    var model = new UserModel();
                    model.firstname = result.First().firstname;
                    model.middlename = result.First().middlename;
                    model.lastname = result.First().lastname;
                    model.username = result.First().username;
                    service.Data = new { Token =  JToken(model) };
                    service.ResponseCode = 200;
                    service.Message = "Success";
                }
                else
                {
                    service.ResponseCode = 400;
                    service.Message = "Invalid username or password";
                }

            }
            catch (Exception ex)
            {
                service.Data = ex.Message;
                service.ResponseCode = 400;
                service.Message = "Exception Error";
            }
            return service;
        }
        public string JToken(UserModel model)
        {
            var result2 = string.Empty;
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["AuthManager:key"]));
                var credentials = new SigningCredentials(tokenKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
                    new Claim("firstname",model.firstname),
                    new Claim("lastname", model.lastname),
                    new Claim("middlename", model.middlename),
                    new Claim(JwtRegisteredClaimNames.Sub, "Sub"),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var token = new JwtSecurityToken(
                issuer: _config["AuthManager:Issuer"],
                audience: _config["AuthManager:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: credentials
                );

                result2 = tokenHandler.WriteToken(token);

                if(result2 == "")
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return result2;
        }
    }
}
