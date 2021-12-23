using Microsoft.Data.SqlClient;
using System.Data;

namespace Furlough.DAL
{
    public class AvailableDays
    {
        private readonly FurloughContext _context;

        public AvailableDays(FurloughContext context)
        {
            _context = context;
        }

        public IEnumerable<Models.AvailableDay> GetByEmployeeId(int employeeId)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_availableDaysGetByEmployeeId", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@EmployeeId", employeeId);

            connection.Open();
            return Mapper(command.ExecuteReader());
        }

        //Object mapper; reader to model
        public IEnumerable<Models.AvailableDay> Mapper(SqlDataReader reader)
        {
            var listObj = new List<Models.AvailableDay>();
            try
            {
                while (reader.Read())
                {
                    listObj.Add(new Models.AvailableDay()
                    {
                        EmployeeId = reader.GetInt32("EmployeeId"),
                        Birth = reader.GetInt32("Birth"),
                        BloodDonation = reader.GetInt32("BloodDonation"),
                        Child = reader.GetInt32("Child"),
                        Confinement = reader.GetInt32("Confinement"),
                        DeathOfRelative = reader.GetInt32("DeathOfRelative"),
                        Marriage = reader.GetInt32("Marriage"),
                        Medical = reader.GetInt32("Medical"),
                        Overtime = reader.GetInt32("Overtime"),
                        Unpaid = reader.GetInt32("Unpaid"),
                        Yearly = reader.GetInt32("Yearly")
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
