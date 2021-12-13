using Microsoft.Data.SqlClient;
using System.Data;

namespace Furlough.DAL
{
    public class Request
    {
        private readonly FurloughContext _context;

        public Request(FurloughContext context)
        {
            _context = context;
        }
        public bool Add(Models.Request obj)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_requestAdd", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@DateFrom", obj.DateFrom);
            command.Parameters.AddWithValue("@DateUntil", obj.DateUntil);
            command.Parameters.AddWithValue("@RequestedByUserId", obj.RequestedByUserId);
            command.Parameters.AddWithValue("@RequestStatusId", obj.RequestStatusId);
            command.Parameters.AddWithValue("@PaidDays", obj.PaidDays);
            command.Parameters.AddWithValue("@RequestTypeId", obj.RequestTypeId);

            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }

        public bool Edit(Models.Request obj)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_requestEdit", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@DateFrom", obj.DateFrom);
            command.Parameters.AddWithValue("@DateUntil", obj.DateUntil);
            command.Parameters.AddWithValue("@RequestedByUserId", obj.RequestedByUserId);
            command.Parameters.AddWithValue("@RequestStatusId", obj.RequestStatusId);
            command.Parameters.AddWithValue("@PaidDays", obj.PaidDays);
            command.Parameters.AddWithValue("@RequestTypeId", obj.RequestTypeId);

            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }

        //Change Model to reflect the actual db model
        public IEnumerable<Models.RequestByDepartment> GetByDepartment(int departmentId)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_requestGetByDepartment", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@DepartmentId", departmentId);

            connection.Open();
            return RequestByDepartmentMapper(command.ExecuteReader());
        }

        //Object mapper; reader to model
        public IEnumerable<Models.Request> Mapper(SqlDataReader reader)
        {
            var listObj = new List<Models.Request>();
            try
            {
                while (reader.Read())
                {
                    listObj.Add(new Models.Request()
                    {
                        Id = reader.GetInt32("Id"),
                        DateFrom = reader.GetDateTime("DateFrom"),
                        DateUntil = reader.GetDateTime("DateUntil"),
                        PaidDays = reader.GetInt32("PaidDays"),
                        RequestedByUserId = reader.GetInt32("RequestedByUserId"),
                        RequestedOn = reader.GetDateTime("RequestedOn"),
                        RequestStatusId = reader.GetByte("RequestStatusId"),
                    });
                }
                return listObj;
            }
            catch (Exception e)
            {
                var error = e.Message;
                throw;
            }
        }

        public IEnumerable<Models.RequestByDepartment> RequestByDepartmentMapper(SqlDataReader reader)
        {
            var listObj = new List<Models.RequestByDepartment>();
            try
            {
                while (reader.Read())
                {
                    listObj.Add(new Models.RequestByDepartment
                    {
                        DepartmentId = reader.GetInt32("DepartmentId"),
                        DepartmentName = reader.GetString("DepartmentName"),
                        EmployeeName = reader.GetString("EmployeeName"),
                        RequestDateFrom = reader.GetDateTime("RequestDateFrom"),
                        RequestDateUntil = reader.GetDateTime("RequestDateUntil"),
                        RequestedByUserId = reader.GetInt32("RequestedByUserId"),
                        RequestedOn = reader.GetDateTime("RequestedOn"),
                        RequestId = reader.GetInt32("RequestId"),
                        EmployeeId = reader.GetInt32("EmployeeId")
                    });
                }
                return listObj;
            }
            catch (Exception e)
            {
                var error = e.Message;
                throw;
            }
        }
    }
}
