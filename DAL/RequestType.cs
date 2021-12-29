using Microsoft.Data.SqlClient;
using System.Data;

namespace Furlough.DAL
{
    public class RequestType
    {
        private FurloughContext _context;

        public RequestType(FurloughContext context)
        {
            _context = context;
        }

        public IEnumerable<Models.RequestType> GetAll()
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_requestTypeGetAll", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            connection.Open();
            return Mapper(command.ExecuteReader());
        }

        public bool Delete(int id)
        {
            try
            {
                using var connection = new SqlConnection(_context.GetConnection());
                using var command = new SqlCommand("sp_requestTypeDelete", connection)
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
                Console.WriteLine(e.Message);
                Console.ResetColor();
                return false;
            }
            
        }
        public Models.RequestType GetById(int id)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_requestTypeGetById", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            return Mapper(command.ExecuteReader()).FirstOrDefault();
        }

        public bool Add(Models.RequestType obj)
        {
            try
            {
                using var connection = new SqlConnection(_context.GetConnection());
                using var command = new SqlCommand("sp_requestTypeAdd", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Type", obj.Type);
                command.Parameters.AddWithValue("@Description", obj.Description);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();

                return false;
            }
        }
        public bool Edit(Models.RequestType obj)
        {
            try
            {
                using var connection = new SqlConnection(_context.GetConnection());
                using var command = new SqlCommand("sp_requestTypeEdit", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@Id", obj.Id);
                command.Parameters.AddWithValue("@Type", obj.Type);
                command.Parameters.AddWithValue("@Description", obj.Description);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();

                return false;
            }
        }
        //Object mapper; reader to model
        public IEnumerable<Models.RequestType> Mapper(SqlDataReader reader)
        {
            var listObj = new List<Models.RequestType>();
            try
            {
                while (reader.Read())
                {
                    listObj.Add(new Models.RequestType()
                    {
                        Id = reader.GetInt32("Id"),
                        Type = reader.GetString("Type"),
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
