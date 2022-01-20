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
            command.Parameters.AddWithValue("@Dates", obj.Dates);
            command.Parameters.AddWithValue("@RequestedByUserId", obj.RequestedByUserId);
            command.Parameters.AddWithValue("@RequestStatusId", obj.RequestStatusId);
            command.Parameters.AddWithValue("@DaysAmount", obj.DaysAmount);
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
            command.Parameters.AddWithValue("@Dates", obj.Dates);
            command.Parameters.AddWithValue("@RequestedByUserId", obj.RequestedByUserId);
            command.Parameters.AddWithValue("@RequestStatusId", obj.RequestStatusId);
            command.Parameters.AddWithValue("@DaysAmount", obj.DaysAmount);
            command.Parameters.AddWithValue("@RequestTypeId", obj.RequestTypeId);

            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }

        public IEnumerable<Models.RequestByDepartment> GetAllByRowCount(int rowCount, int departmentId = -1)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_requestGetAllByRowCount", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@RowCount", rowCount);

            if (departmentId != -1)
                command.Parameters.AddWithValue("@DepartmentId", departmentId);

            connection.Open();
            return RequestByDepartmentMapper(command.ExecuteReader());
        }

        public IEnumerable<Models.Request> GetByUser(int userId, int requestStatusId = 0)
        {
            try
            {
                using var connection = new SqlConnection(_context.GetConnection());
                using var command = new SqlCommand("sp_requestGetOfEmployee", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@RequestStatusId", requestStatusId); //if 0, returns all requests of user
                command.Parameters.AddWithValue("@UserId", userId);

                connection.Open();
                return Mapper(command.ExecuteReader());
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();
                return null;
            }
            
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

        public IEnumerable<Models.Request> GetAll()
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_requestGetAll", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            connection.Open();
            return Mapper(command.ExecuteReader());
        }

        public Models.Request GetById(int id)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_requestGetById", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            return Mapper(command.ExecuteReader()).FirstOrDefault();
        }

        public bool DeleteById(int id)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_requestDeleteById", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            return command.ExecuteNonQuery() > 0;
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
                        Dates = reader.GetString("Dates"),
                        DaysAmount = reader.GetInt32("DaysAmount"),
                        RequestedByUserId = reader.GetInt32("RequestedByUserId"),
                        RequestedOn = reader.GetDateTime("RequestedOn"),
                        RequestStatusId = reader.GetByte("RequestStatusId"),
                        RequestTypeId = reader.GetInt32("RequestTypeId"),
                        Reason = reader["Reason"] == DBNull.Value ? "" : reader.GetString("Reason"),
                        LUD = reader.GetDateTime("LUD"),
                        LUN = reader.GetInt32("LUN")
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
                        EmployeeName = reader.GetString("EmployeeName"),
                        RequestDates = reader.GetString("RequestDates"),
                        RequestedByUserId = reader.GetInt32("RequestedByUserId"),
                        RequestedOn = reader.GetDateTime("RequestedOn"),
                        RequestId = reader.GetInt32("RequestId"),
                        EmployeeId = reader.GetInt32("EmployeeId"),
                        EmployeePositionId = reader.GetInt32("EmployeePositionId"),
                        RequestStatusId = reader.GetByte("RequestStatusId"),
                        RequestDaysAmount = reader.GetInt32("RequestDaysAmount"),
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
