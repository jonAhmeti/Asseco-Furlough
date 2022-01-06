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
        public IEnumerable<Models.AvailableDayGetAll> GetAll()
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_availableDaysGetAll", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            connection.Open();
            return DetailedMapper(command.ExecuteReader());
        }

        public Models.AvailableDay GetByEmployeeId(int employeeId)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_availableDaysGetByEmployeeId", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@EmployeeId", employeeId);

            connection.Open();
            return Mapper(command.ExecuteReader()).FirstOrDefault();
        }

        public bool SetDays(int employeeId, string requestType, int daysAmount)
        {
            try
            {
                using var connection = new SqlConnection(_context.GetConnection());
                using var command = new SqlCommand("sp_availableDaysSet", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@EmployeeId", employeeId);
                command.Parameters.AddWithValue("@LeaveType", requestType);
                command.Parameters.AddWithValue("@DaysAmount", daysAmount);

                connection.Open();
                var res = command.ExecuteNonQuery();
                return res > 0;

            }
            catch (Exception e)
            {
                return false;
            }
            
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
                        Maternity = reader.GetInt32("Maternity"),
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

        public IEnumerable<Models.AvailableDayGetAll> DetailedMapper(SqlDataReader reader)
        {
            var listObj = new List<Models.AvailableDayGetAll>();
            try
            {
                while (reader.Read())
                {
                    listObj.Add(new Models.AvailableDayGetAll
                    {
                        EmployeeId = reader.GetInt32("EmployeeId"),
                        EmployeeName = reader.GetString("EmployeeName"),
                        EmployeeDepartment = reader.GetString("EmployeeDepartment"),
                        EmployeePosition = reader.GetString("EmployeePosition"),
                        Medical = reader.GetInt32("Medical"),
                        Yearly = reader.GetInt32("Yearly"),
                        Overtime = reader.GetInt32("Overtime"),
                        Birth = reader.GetInt32("Birth"),
                        Child = reader.GetInt32("Child"),
                        Marriage = reader.GetInt32("Marriage"),
                        Unpaid = reader.GetInt32("Unpaid"),
                        BloodDonation = reader.GetInt32("BloodDonation"),
                        Maternity = reader.GetInt32("Maternity"),
                        DeathOfRelative = reader.GetInt32("DeathOfRelative"),
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
