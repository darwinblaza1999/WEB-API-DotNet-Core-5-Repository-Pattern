using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI.IRepository;
using WEBAPI.Models;

namespace WEBAPI.Class
{
    public class EmployeeClass : IEmployee
    {
        private readonly IConfiguration _config;
        private SqlConnection conn;
        public EmployeeClass(IConfiguration config)
        {
            _config = config;
            conn = new SqlConnection(_config["ConnectionStrings:DefaultConnection"]);
        }

        public async Task<ServiceResponse<object>> insert(EmployeeModel model)
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
                param.Add("Retval", dbType:System.Data.DbType.Int32, direction:System.Data.ParameterDirection.Output);
                var connect = conn;
                var res = connect.Query("usp_Insert", param, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                var retval = param.Get<int>("Retval").ToString();

                    service.Data = res;
                    service.ResponseCode = Int32.Parse(retval);
                    service.Message = "Success";

            }
            catch (SqlException sql)
            {

                service.Data = sql.Message;
                service.ResponseCode = 400;
                service.Message = "Sql Exception";
            }
            catch (Exception ex)
            {
                service.Data = ex.Message;
                service.ResponseCode = 400;
                service.Message = "Exception Error";
            }
            return service;
        }
        public async Task<ServiceResponse<object>> getbyID(int id)
        {
            var service = new ServiceResponse<object>();
            try
            {
                var param = new DynamicParameters();
                param.Add("id", id);
                var res = conn.Query("usp_getData", param, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                var resCode = res.code;
                if(resCode == 10)
                {
                    service.Data = res;
                    service.ResponseCode = 200;
                    service.Message = "No Record Data";
                }
                else
                {
                    service.Data = res;
                    service.ResponseCode = 200;
                    service.Message = "Found Data";
                }
            }
            catch (SqlException sql)
            {

                service.Data = sql.Message;
                service.ResponseCode = 400;
                service.Message = "Sql Exception";
            }
            catch (Exception ex)
            {

                service.Data = ex.Message;
                service.ResponseCode = 400;
                service.Message = "Exception Error";
            }
            return service;
        }

        public async Task<ServiceResponse<object>> updatebyID(EmployeeModel model, int id)
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
                param.Add("Id", id);
                param.Add("retval", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                var res = conn.Query("usp_update", param, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
                var retval = param.Get<int>("retval");

                service.Data = res;
                service.ResponseCode = retval;
                service.Message = "Success";
            }
            catch (SqlException sql)
            {

                service.Data = sql.Message;
                service.ResponseCode = 400;
                service.Message = "SqlException Error";
            }
            catch (Exception ex)
            {

                service.Data = ex.Message;
                service.ResponseCode = 400;
                service.Message = "Exception Error";
            }
            return service;
        }

        public async Task<ServiceResponse<object>> DeleteEmployee(int id)
        {
            var service = new ServiceResponse<object>();
            try
            {
                var param = new DynamicParameters();
                param.Add("Id", id);

                var res = conn.Query("usp_Delete", param, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();

                if(res.result == 10)
                {
                    service.Data = res;
                    service.ResponseCode = 200;
                    service.Message = "Success";
                }
                else
                {
                    service.Data = res;
                    service.ResponseCode = 200;
                    service.Message = "Failed";
                }
           
            }
            catch(SqlException sql)
            {
                service.Data = new
                {
                    Message = sql.Message,
                    Error = sql.Errors
                };
                service.ResponseCode = 400;
                service.Message = "SqlConnection Error";
            }
            catch (Exception ex)
            {
                service.Data = ex.Message;
                service.ResponseCode = 400;
                service.Message = "Exception Error";
            }
            return service;
        }

        public async Task<ServiceResponse<object>> GetAllData()
        {
            var service = new ServiceResponse<object>();
            try
            {
                var param = new DynamicParameters();
                param.Add("retval", dbType: System.Data.DbType.Int32, direction: System.Data.ParameterDirection.Output);
                var result = conn.Query("usp_GetAllData", param, commandType: System.Data.CommandType.StoredProcedure).ToList();
                var retval = param.Get<int>("retval").ToString();

                service.Data = result;
                service.ResponseCode = Int32.Parse(retval);
                service.Message = "Success";
            }
            catch (SqlException sql)
            {

                service.Data = sql.Message;
                service.ResponseCode = 400;
                service.Message = "SqlException";
            }
            catch (Exception ex)
            {

                service.Data = ex.Message;
                service.ResponseCode = 400;
                service.Message = "Exception";
            }
            return service;
        }
    }
}
