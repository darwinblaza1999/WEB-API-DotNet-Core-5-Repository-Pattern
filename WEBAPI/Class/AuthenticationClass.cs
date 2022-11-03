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
                param.Add("password", password);
                param.Add("retval", dbType: DbType.Int32, direction:ParameterDirection.Output);
                var result = await con.QueryAsync<dynamic>("usp_Login", param, commandType: CommandType.StoredProcedure);
                var retval = param.Get<int>("retval");

                if (retval.Equals(100))
                {
                    var model = new UserModel();
                    model.firstname = result.First().Firstname;
                    model.lastname = result.First().Lastname;
                    model.email = result.First().Email;
                    service.Data = new { Token =  JToken(model) };
                    service.ResponseCode = 200;
                    service.Message = "Success";
                }
                else
                {
                    service.ResponseCode = 400;
                    service.Message = "Unauthorized access user";
                }

            }
            catch (Exception ex)
            {
                service.Data = ex.Message;
                service.ResponseCode = 500;
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
                result2 =  ex.Message;
            }
            return result2;
        }

        public async Task<ServiceResponse<object>> RefreshToken(TokenModel token)
        {
            var service = new ServiceResponse<object>();
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                tokenHandler.ValidateToken(token.token, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _config["AuthManager:Issuer"],
                    ValidAudience = _config["AuthManager:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AuthManager:Key"]))
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var fname = jwtToken.Claims.First(x => x.Type == "firstname").Value;
                var lname = jwtToken.Claims.First(x => x.Type == "lastname").Value;
                if(fname == token.firstname && lname == token.lastname)
                {
                    var model = new UserModel();
                    model.firstname = token.firstname;
                    model.lastname = token.lastname;

                    service.Data = JToken(model);
                    service.ResponseCode = 200;

                }
            }
            catch (Exception ex)
            {

                service.Data = null;
                service.ResponseCode = 500;
                service.Message = ex.Message;
            }
            return service;
        }
    }
}
