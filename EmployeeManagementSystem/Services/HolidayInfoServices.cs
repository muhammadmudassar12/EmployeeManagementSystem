using Dapper;
using System.Data.SqlClient;
using System.Data;
using EmployManagementSystemAPIs.Connection;
using System.Collections.Generic;
using System.Threading.Tasks;
using EmployeeManagementSystem.Models;
using System.Linq;

namespace EmployManagementSystemAPIs.Services.HolidayInfoServices
{
     class HolidayInfoServices 
    {
        public async Task<List<HolidayInfo>> GetAllHolidayInfo()
        {
            using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                var holidayinfo = await connection.QueryAsync<HolidayInfo>("GetAllHolidayInfo", null, commandType: CommandType.StoredProcedure);
                return holidayinfo.ToList();
            }
        }
        public async Task<HolidayInfo> GetHolidayInfoById(int id)
        {
            using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", id);
                var holidayinfo = await connection.QueryAsync<HolidayInfo>("GetHolidayInfoById", parameters, commandType: CommandType.StoredProcedure);
                return holidayinfo.FirstOrDefault();
            }
        }
        public async Task<int> PostHolidayInfo(HolidayInfo holidayinfo)
        {
            using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();             
                parameters.Add("@HolidayMonth", holidayinfo.HolidayMonth);
                parameters.Add("@Holidays", holidayinfo.Holidays);
                parameters.Add("@Leaves", holidayinfo.Leaves);
                parameters.Add("@TotalHolidays", holidayinfo.TotalHolidays);
                parameters.Add("@BasicId", holidayinfo.BasicId);
                parameters.Add("@LastInsertedId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                await connection.ExecuteAsync("PostHolidayInfo", parameters, commandType: CommandType.StoredProcedure);
                int LastInsertedId = parameters.Get<int>("@LastInsertedId");
                return LastInsertedId;
            }
        }

        public async Task<int> UpdateHolidayInfo(HolidayInfo holidayinfo)
        {
            using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@HolidayMonth", holidayinfo.HolidayMonth);
                parameters.Add("@Holidays", holidayinfo.Holidays);
                parameters.Add("@Leaves", holidayinfo.Leaves);
                parameters.Add("@TotalHolidays", holidayinfo.TotalHolidays);
                parameters.Add("@BasicId", holidayinfo.BasicId);
                parameters.Add("@Id", holidayinfo.Id);
                var result = await connection.ExecuteAsync("UpdateHolidayInfo", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
        public async Task<int> DeleteHolidayInfo(int Id)
        {
            using (IDbConnection connection = new SqlConnection(DBConnection.dbConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@Id", Id);
                var result = await connection.ExecuteAsync("DeleteHolidayInfo", parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}


