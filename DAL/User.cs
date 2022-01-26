using Microsoft.Data.SqlClient;
using System.Data;


namespace Furlough.DAL
{
    public class User
    {
        private readonly FurloughContext _context;

        public User(FurloughContext context)
        {
            _context = context;
        }
        public int Add(Models.User obj)
        {
            try
            {
                using var connection = new SqlConnection(_context.GetConnection());
                using var command = new SqlCommand("sp_userAdd", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Username", obj.Username);
                command.Parameters.AddWithValue("@Password", obj.Password);
                command.Parameters.AddWithValue("@RoleId", obj.RoleId);
                command.Parameters.AddWithValue("@LUBUserId", obj.LUBUserId);
                connection.Open();
                return (int)command.ExecuteScalar();
            }

            catch (Exception e)
            {
                return 0;
                throw;
            }
        }
        
        public bool Edit(Models.User obj)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_userEdit", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@UserId", obj.Id);
            command.Parameters.AddWithValue("@RoleId", obj.RoleId);
            command.Parameters.AddWithValue("@LUBUserId", obj.LUBUserId);
            command.Parameters.AddWithValue("@Username", obj.Username);
            command.Parameters.AddWithValue("@Password", obj.Password);
            connection.Open();

            return command.ExecuteNonQuery() > 0;
        }

        public bool Delete(int id)
        {
            try
            {
                using var connection = new SqlConnection(_context.GetConnection());
                using var command = new SqlCommand("sp_userDelete", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", id);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("User DAL error onDelete: " + e.Message);
                Console.ResetColor();

                return false;
            }
        }

        public IEnumerable<Models.User> GetUnattachedToEmployees()
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_userUnlinkedToEmployee", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            connection.Open();

            return Mapper(command.ExecuteReader());
        }

        public Models.User GetById(int id)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_userGetById", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Id", id);
            connection.Open();

            return Mapper(command.ExecuteReader()).FirstOrDefault();
        }
        public IEnumerable<Models.User> GetByDepartmentId(int id)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_userGetByDepartmentId", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@DepartmentId", id);
            connection.Open();

            return Mapper(command.ExecuteReader());
        }

        public Models.User GetByUsername(string username)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_userGetByUsername", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Username", username);
            connection.Open();

            return Mapper(command.ExecuteReader(), true).FirstOrDefault();
        }

        public IEnumerable<Models.User> GetAll()
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_userGetAll", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            connection.Open();

            return Mapper(command.ExecuteReader());
        }

        //Object mapper; reader to model
        public IEnumerable<Models.User> Mapper(SqlDataReader reader, bool getPassword = false)
        {
            var objList = new List<Models.User>();
            try
            {
                while (reader.Read())
                {
                    objList.Add(new Models.User()
                    {
                        Id = reader.GetInt32("Id"),
                        InsertDate = reader.GetDateTime("InsertDate"),
                        Username = reader.GetString("Username"),
                        RoleId = reader.GetInt32("RoleId"),
                        Password = getPassword ? reader.GetString("Password") : "",
                        LUD = reader.GetDateTime("LUD"),
                        LUN = reader.GetInt32("LUN"),
                        LUBUserId = reader.GetInt32("LUBUserId")
                    });
                }
                return objList;
            }
            catch (Exception e)
            {
                var error = e.Message;
                throw;
            }
            
        }
    }
}
