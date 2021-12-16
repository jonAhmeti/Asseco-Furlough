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
                        Type = reader.GetString("Type")
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
