using Microsoft.Data.SqlClient;
using System.Data;

namespace Furlough.DAL
{
    public class Department
    {
        private readonly FurloughContext _context;

        public Department(FurloughContext context)
        {
            _context = context;
        }

        public bool Add(Models.Department obj)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_departmentAdd", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@Name", obj.Name);

            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }

        public bool Edit(Models.Department obj)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_departmentEdit", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Id", obj.Id);
            command.Parameters.AddWithValue("@Name", obj.Name);

            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }

        public bool Delete(int id)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_departmentDelete", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }

        public Models.Department GetById(int id)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_departmentGetById", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            return Mapper(command.ExecuteReader()).FirstOrDefault();
        }

        public Models.Department GetByName(string name)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_userGetByUsername", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Name", name);

            connection.Open();
            return Mapper(command.ExecuteReader()).FirstOrDefault();
        }

        public IEnumerable<Models.Department> GetAll()
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_departmentGetAll", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            connection.Open();
            return Mapper(command.ExecuteReader());
        }
        public Furlough.Models.UserEmployee GetDepartmentManager(int departmentId)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_util_getDepartmentManager", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@DepartmentId", departmentId);

            connection.Open();
            return UserEmployeeMapper(command.ExecuteReader());
        }
        public IEnumerable<Models.DepartmentChart> DepartmentChart()
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_util_getDepartmentsVacationNumber", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            connection.Open();
            return ChartMapper(command.ExecuteReader());
        }

        //Object mapper; reader to model
        private IEnumerable<Models.Department> Mapper(SqlDataReader reader)
        {
            var listObj = new List<Models.Department>();
            try
            {
                while (reader.Read())
                {
                    listObj.Add(new Models.Department()
                    {
                        Id = reader.GetInt32("Id"),
                        Name = reader.GetString("Name")
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

        private Furlough.Models.UserEmployee UserEmployeeMapper(SqlDataReader reader)
        {
            var obj = new Furlough.Models.UserEmployee();
            try
            {
                while (reader.Read())
                {
                    obj.Name = reader.GetString("Name");
                    obj.Email = reader.GetString("Email");
                    obj.Username = reader.GetString("Username");
                    obj.Phone = reader["Phone"] == DBNull.Value ? null : reader.GetString("Phone");
                    obj.EmployeeId = reader.GetInt32("EmployeeId");
                    obj.UserId = reader.GetInt32("UserId");
                }

                reader.Close();
                return obj;
            }
            catch (Exception e)
            {
                var error = e.Message;
                throw;
            }
        }

        private IEnumerable<Models.DepartmentChart> ChartMapper(SqlDataReader reader)
        {
            var list = new List<Models.DepartmentChart>();
            try
            {
                while (reader.Read())
                {
                    list.Add(new Models.DepartmentChart
                    {
                        Department = reader.GetString("Department"),
                        NoEmployees = reader.GetInt32("NoEmployees")
                    });
                }
                reader.Close();
                return list;
            }
            catch (Exception e)
            {
                var error = e.Message;
                throw;
            }
        }
    }
}
