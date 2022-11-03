using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI.IRepository.IAdapterRepository;
using WEBAPI.Models;

namespace WEBAPI.Class
{
    public class UserClass : IUser
    {
        private readonly IConfiguration _config;
        private SqlConnection conn;
        private readonly IWebHostEnvironment _webHosting;

        public UserClass(IConfiguration config, IWebHostEnvironment webHosting)
        {
            _config = config;
            conn = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]);
            _webHosting = webHosting;
        }

        //public string getpath(UserModel model)
        //{
        //    //var model1 = new UserModel1();
        //    //model1.firstname = model.firstname;
        //    //return "";
        //}
        public async Task<ServiceResponse<object>> insertUser(UserModel model)
        {
            var service = new ServiceResponse<object>();
            try
            {
                var param = new DynamicParameters();
                var property = model.GetType().GetProperties();
                foreach (var item in property)
                {
                    var name = item.Name;
                    var value = item.GetValue(model);
                    param.Add(name, value);
                }
                param.Add("retval", dbType: DbType.Int32, direction: ParameterDirection.Output);

                var res = await conn.QueryAsync("usp_AddUserAccount", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                var retval = param.Get<int>("retval");
                if(retval == 100)
                {
                    service.Data = res;
                    service.ResponseCode = 200;
                    service.Message = "Success";
                }
                else
                {
                    service.Data = null;
                    service.ResponseCode = 300;
                    service.Message = "Failed";
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

        public async Task<ServiceResponse<object>> updateUser(UserModel model, string photo, int id)
        {
            var service = new ServiceResponse<object>();
            try
            {

                var param = new DynamicParameters();
                var property = model.GetType().GetProperties();
                foreach (var item in property)
                {
                    var name = item.Name;
                    var value = item.GetValue(model);
                    param.Add(name, value);
                }
                param.Add("id", id);
                param.Add("retval", DbType.Int32, direction: ParameterDirection.Output);
                var result = await conn.QueryAsync("usp_UpdateUser", param, commandType: CommandType.StoredProcedure).FirstOrDefault();
                var retval = param.Get<int>("retval");

                if(retval == 100)
                {
                    service.Data = result;
                    service.ResponseCode = 200;
                    service.Message = "Success";
                }
                else
                {
                    service.Message = "Failed";
                    service.ResponseCode = 300;
                    service.Data = null;
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
    }
}
