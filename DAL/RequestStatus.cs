using Microsoft.Data.SqlClient;
using System.Data;

namespace Furlough.DAL
{
    public class RequestStatus
    {
        private readonly FurloughContext _context;

        public RequestStatus(FurloughContext context)
        {
            _context= context;
        }

        public IEnumerable<Models.RequestStatus> GetAll()
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_requestStatusGetAll", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            connection.Open();
            return Mapper(command.ExecuteReader());
        }

        public IEnumerable<Models.RequestStatus> Mapper(SqlDataReader reader)
        {
            var listObj = new List<Models.RequestStatus>();
            try
            {
                while (reader.Read())
                {
                    listObj.Add(new Models.RequestStatus
                    {
                        Id = reader.GetByte("Id"),
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
