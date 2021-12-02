using Microsoft.Data.SqlClient;
using System.Data;

namespace Furlough.DAL
{
    // To be implemented
    public class DepartmentPositions
    {
        private readonly FurloughContext _context;

        public DepartmentPositions(FurloughContext context)
        {
            _context = context;
        }

        public bool Add(Models.DepartmentPositions obj)
        {
            try
            {
                using var connection = new SqlConnection(_context.GetConnection());
                using var command = new SqlCommand("sp_departmentPositionsAdd", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@DepartmentId", obj.DepartmentId);
                command.Parameters.AddWithValue("@PositionId", obj.PositionId);

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

        #region Delete Methods
        public bool DeleteById(int id)
        {
            try
            {
                using var connection = new SqlConnection(_context.GetConnection());
                using var command = new SqlCommand("sp_departmentPositionsDeleteById", connection)
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

        public bool DeleteByDepartmentIdRoleId(int departmentId, int positionId)
        {
            try
            {
                using var connection = new SqlConnection(_context.GetConnection());
                using var command = new SqlCommand("sp_departmentPositionsDeleteByDepartmentIdRoleId", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@DepartmentId", departmentId);
                command.Parameters.AddWithValue("@PositionId", positionId);

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
        #endregion

        public IEnumerable<Models.DepartmentPositions> GetPositionsByDepartmentId(int departmentId)
        {
            try
            {
                using var connection = new SqlConnection(_context.GetConnection());
                using var command = new SqlCommand("sp_departmentPositionsGetByDepartmentId", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@DepartmentId", departmentId);

                connection.Open();
                return Mapper(command.ExecuteReader());
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();
                return new List<Models.DepartmentPositions>();
            }
        }

        public IEnumerable<Models.DepartmentPositions> GetPositionsNotInDepartment(int departmentId)
        {
            try
            {
                using var connection = new SqlConnection(_context.GetConnection());
                using var command = new SqlCommand("sp_departmentPositionsGetNotInDepartmentId", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@DepartmentId", departmentId);

                connection.Open();
                return Mapper(command.ExecuteReader());
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();
                return new List<Models.DepartmentPositions>();
            }
        }

        public bool UpdateDepartmentPositions(int departmentId, string positionsId)
        {
            try
            {
                using var connection = new SqlConnection(_context.GetConnection());
                using var command = new SqlCommand("sp_departmentPositionsUpdate", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@DepartmentId", departmentId);
                command.Parameters.AddWithValue("@PositionsId", positionsId);

                connection.Open();
                return command.ExecuteNonQuery() > -1;
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ResetColor();
                return false;
            }
        }
        public IEnumerable<Models.DepartmentPositions> Mapper(SqlDataReader reader)
        {
            try
            {
                var list = new List<Models.DepartmentPositions>();

                while (reader.Read())
                {
                    list.Add(new Models.DepartmentPositions
                    {
                        Id = reader.GetInt32("Id"),
                        DepartmentId = reader.GetInt32("DepartmentId"),
                        PositionId = reader.GetInt32("PositionId")
                    });
                }

                return list;
            }
            catch (Exception e)
            {
                var error = e.Message;
                throw;
            }
            
        }

    }
}