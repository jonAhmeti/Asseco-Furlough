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
                        RequestedOn = reader.GetDateTime("RequestedOn")
                        //StatusId instead of Status
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
