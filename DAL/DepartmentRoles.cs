using Microsoft.Data.SqlClient;
using System.Data;

namespace Furlough.DAL
{
    // To be implemented
    public class DepartmentRoles
    {
        private readonly FurloughContext _context;

        public DepartmentRoles(FurloughContext context)
        {
            _context = context;
        }

        public bool Add(Models.DepartmentRoles obj)
        {
            try
            {
                using var connection = new SqlConnection(_context.GetConnection());
                using var command = new SqlCommand("sp_departmentRolesAdd", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@DepartmentId", obj.DepartmentId);
                command.Parameters.AddWithValue("@RoleId", obj.RoleId);

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
                using var command = new SqlCommand("sp_departmentRolesDeleteById", connection)
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

        public bool DeleteByDepartmentIdRoleId(int departmentId, int roleId)
        {
            try
            {
                using var connection = new SqlConnection(_context.GetConnection());
                using var command = new SqlCommand("sp_departmentRolesDeleteByDepartmentIdRoleId", connection)
                {
                    CommandType = CommandType.StoredProcedure
                };
                command.Parameters.AddWithValue("@DepartmentId", departmentId);
                command.Parameters.AddWithValue("@RoleId", roleId);

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
    }
}