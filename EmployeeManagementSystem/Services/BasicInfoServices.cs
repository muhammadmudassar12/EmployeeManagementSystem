﻿using EmployeeManagementSystem.Models;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using EmployManagementSystemAPIs.Connection;
using System.Threading.Tasks;

namespace EmployManagementSystemAPIs.Services.BasicInfoServices
{
    class BasicInfoServices 
    {
        public async Task<List<BasicInfo>> GetAllBasicInfo()
        {
            using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                var basicinfo = await connection.QueryAsync<BasicInfo>("GetAllBasicInfo", null, commandType: CommandType.StoredProcedure);
                return basicinfo.ToList();
            }
        }
        public async Task<BasicInfo> GetBasicInfoById(int id)
        {
            using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                var basicinfo = await connection.QueryAsync<BasicInfo>("GetBasicInfoById", parameters, commandType: CommandType.StoredProcedure);
                return basicinfo.FirstOrDefault();
            }
        }
        public async Task<int> PostBasicInfo(BasicInfo basicinfo)
        {
            using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Name", basicinfo.Name);
                parameters.Add("@Email", basicinfo.Email);
                parameters.Add("@Address", basicinfo.Address);
                parameters.Add("@Gender", basicinfo.Gender);
                parameters.Add("@Position", basicinfo.Position);
                parameters.Add("@LastInsertedId", dbType: DbType.Int32, direction: ParameterDirection.Output);

                await connection.ExecuteAsync("PostBasicInfo", parameters, commandType: CommandType.StoredProcedure);

                int LastInsertedId = parameters.Get<int>("@LastInsertedId");

                return LastInsertedId;
            }
        }
        public async Task<int> UpdateBasicInfo(BasicInfo basicinfo)
        {
            using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Name", basicinfo.Name);
                parameters.Add("@Email", basicinfo.Email);
                parameters.Add("@Address", basicinfo.Address);
                parameters.Add("@Gender", basicinfo.Gender);
                parameters.Add("@Position", basicinfo.Position);
                parameters.Add("@Id", basicinfo.Id);
                var result = await connection.ExecuteAsync("UpdateBasicInfo", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<int> DeleteBasicInfo(int Id)
        {
            using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", Id);
                var result = await connection.ExecuteAsync("DeleteBasicInfo", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
