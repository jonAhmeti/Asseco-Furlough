using Microsoft.Data.SqlClient;
using System.Data;

namespace Furlough.DAL
{
    public class RequestHistory
    {
        private readonly FurloughContext _context;

        public RequestHistory(FurloughContext context)
        {
            _context = context;
        }

        public bool Add(Models.RequestHistory obj)
        {
            try
            {
                using var connection = new SqlConnection(_context.GetConnection());
                using var command = new SqlCommand("sp_requestHistoryAdd", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@RequestId", obj.RequestId);
                command.Parameters.AddWithValue("@Message", obj.Message == null ? DBNull.Value : obj.Message);
                command.Parameters.AddWithValue("@AlteredByUserId", obj.AlteredByUserId);
                command.Parameters.AddWithValue("@PreviousRequestStatusId", obj.PreviousRequestStatusId);
                command.Parameters.AddWithValue("@PreviousRequestTypeId", obj.PreviousRequestTypeId);
                command.Parameters.AddWithValue("@PreviousDates", obj.PreviousDates);

                connection.Open();
                return command.ExecuteNonQuery() > 0;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();
                throw;
            }
            
        }
    }
}
