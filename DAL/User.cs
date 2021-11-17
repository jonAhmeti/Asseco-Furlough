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
        public uint Add(Models.User obj)
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
                connection.Open();
                return (uint)command.ExecuteScalar();
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

            command.Parameters.AddWithValue("@RoleId", obj.RoleId);
            command.Parameters.AddWithValue("@UpdateBy", obj.UpdateBy);
            command.Parameters.AddWithValue("@Username", obj.Username);
            command.Parameters.AddWithValue("@Password", obj.Password);

            return command.ExecuteNonQuery() > 0;
        }

        public bool Delete(int id)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_userDelete", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Id", id);

            return command.ExecuteNonQuery() > 0;
        }

        public Models.User GetById(int id)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_userGetById", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Id", id);

            return Mapper(command.ExecuteReader());
        }

        public Models.User GetByUsername(int username)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_userGetByUsername", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Username", username);

            return Mapper(command.ExecuteReader());
        }

        //Object mapper; reader to model
        public Models.User Mapper(SqlDataReader reader)
        {
            return new Models.User()
            {
                Id = reader.GetInt32("Id"),
                InsertDate = reader.GetDateTime("InsertDate"),
                Username = reader.GetString("Username"),
                Password = reader.GetString("Password"),
                RoleId = reader.GetInt32("RoleId"),
                UpdateBy = reader.GetInt32("UpdateBy"),
                UpdateDate = reader.GetDateTime("UpdateDate")
            };
        }
    }
}
