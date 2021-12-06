using Microsoft.Data.SqlClient;
using System.Data;

namespace Furlough.DAL
{
    public class PositionHistory
    {
        private FurloughContext _context;

        public PositionHistory(FurloughContext context)
        {
            _context = context;
        }

        public bool Add(Models.PositionHistory obj)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_positionHistoryAdd", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@EmployeeId", obj.EmployeeId);
            command.Parameters.AddWithValue("@PositionId", obj.PositionId);
            command.Parameters.AddWithValue("@SetByUserId", obj.SetByUserId);

            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }
    }
}
