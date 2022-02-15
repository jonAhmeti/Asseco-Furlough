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
        public bool SetAllDays(int employeeId, decimal yearlyDays)
        {
            try
            {
                using var connection = new SqlConnection(_context.GetConnection());
                using var command = new SqlCommand("sp_availableDaysSetAll", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@EmployeeId", employeeId);
                command.Parameters.AddWithValue("@YearlyDays", yearlyDays);

                connection.Open();
                var res = command.ExecuteNonQuery();
                return res > 0;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Delete(int employeeId)
        {
            try
            {
                using var connection = new SqlConnection(_context.GetConnection());
                using var command = new SqlCommand("sp_availableDaysDeleteByEmployeeId", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@EmployeeId", employeeId);

                connection.Open();
                var res = command.ExecuteNonQuery();
                return res > 0;

            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool Edit(Models.AvailableDay obj)
        {
            try
            {
                using var connection = new SqlConnection(_context.GetConnection());
                using var command = new SqlCommand("sp_availableDaysEdit", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };

                command.Parameters.AddWithValue("@EmployeeId", obj.EmployeeId);
                command.Parameters.AddWithValue("@Medical", obj.Medical);
                command.Parameters.AddWithValue("@Yearly", obj.Yearly);
                command.Parameters.AddWithValue("@Overtime", obj.Overtime);
                command.Parameters.AddWithValue("@Birth", obj.Birth);
                command.Parameters.AddWithValue("@Child", obj.Child);
                command.Parameters.AddWithValue("@Marriage", obj.Marriage);
                command.Parameters.AddWithValue("@Unpaid", obj.Unpaid);
                command.Parameters.AddWithValue("@BloodDonation", obj.BloodDonation);
                command.Parameters.AddWithValue("@Maternity", obj.Maternity);
                command.Parameters.AddWithValue("@DeathOfRelative", obj.DeathOfRelative);
                command.Parameters.AddWithValue("@PrevYearly", obj.DeathOfRelative);



                connection.Open();
                var res = command.ExecuteNonQuery();
                return res > 0;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public bool SetDays(int employeeId, string requestType, decimal daysAmount)
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
                        Birth = reader.GetDecimal("Birth"),
                        BloodDonation = reader.GetDecimal("BloodDonation"),
                        Child = reader.GetDecimal("Child"),
                        Maternity = reader.GetDecimal("Maternity"),
                        DeathOfRelative = reader.GetDecimal("DeathOfRelative"),
                        Marriage = reader.GetDecimal("Marriage"),
                        Medical = reader.GetDecimal("Medical"),
                        Overtime = reader.GetDecimal("Overtime"),
                        Unpaid = reader.GetDecimal("Unpaid"),
                        Yearly = reader.GetDecimal("Yearly"),
                        PrevYearly = reader.GetDecimal("PrevYearly")
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
                        Medical = reader.GetDecimal("Medical"),
                        Yearly = reader.GetDecimal("Yearly"),
                        Overtime = reader.GetDecimal("Overtime"),
                        Birth = reader.GetDecimal("Birth"),
                        Child = reader.GetDecimal("Child"),
                        Marriage = reader.GetDecimal("Marriage"),
                        Unpaid = reader.GetDecimal("Unpaid"),
                        BloodDonation = reader.GetDecimal("BloodDonation"),
                        Maternity = reader.GetDecimal("Maternity"),
                        DeathOfRelative = reader.GetDecimal("DeathOfRelative"),
                        PrevYearly = reader.GetDecimal("PrevYearly")
                        
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
