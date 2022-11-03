using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WEBAPI.Class;
using WEBAPI.IRepository;
using WEBAPI.IRepository.IAdapterRepository;
using WEBAPI.Models;

namespace WEBAPI.Helper
{
    public class JWTMiddleware
    {
        private IJsonSerializer _serializer = new JsonNetSerializer();
        private IDateTimeProvider _provider = new UtcDateTimeProvider();
        private IBase64UrlEncoder _urlEncoder = new JwtBase64UrlEncoder();
        private IJwtAlgorithm _algorithm = new HMACSHA256Algorithm();

        private readonly RequestDelegate _request;
        private readonly IConfiguration _config;

        public JWTMiddleware(RequestDelegate request, IConfiguration config)
        {
            _request = request;
            _config = config;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            context.Items["token_expiry"] = false;
            if (token != null)
                userContext(context, token); 
                
            await _request(context);
        }

        public void userContext(HttpContext context, string token)
        {
            try
             {
                IJwtValidator _validator = new JwtValidator(_serializer, _provider);
                IJwtDecoder decoder = new JwtDecoder(_serializer, _validator, _urlEncoder, _algorithm);
                var token1 = decoder.DecodeToObject<JWTToken>(token);
                DateTimeOffset dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(token1.exp);
                var token_expiry = dateTimeOffset.LocalDateTime;

                if(DateTime.Now > token_expiry)
                {
                    context.Items["token_expiry"] = true;
                }
                else
                {
                    context.Items["token_expiry"] = false;
                    var tokenHandler = new JwtSecurityTokenHandler();
                    tokenHandler.ValidateToken(token, new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["AuthManager:key"])),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero,
                    }, out SecurityToken validateToken);
                    var jwtToken = (JwtSecurityToken)validateToken;
                    AutorizeModel user = new AutorizeModel();
                    //user.middlename = jwtToken.Claims.First(x => x.Type == "middleName").Value;
                    user.firstname = jwtToken.Claims.First(x => x.Type == "firstname").Value;
                    user.lastname = jwtToken.Claims.First(x => x.Type == "lastname").Value;

                    context.Items["Users"] = user;
                }
                

            }
            catch (Exception ex)
            {
                return;
            }
           
        }
         
    }
}
