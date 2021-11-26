using Microsoft.Data.SqlClient;
using System.Data;

namespace Furlough.DAL
{
    public class Department
    {
        private readonly FurloughContext _context;

        public Department(FurloughContext context)
        {
            _context = context;
        }

        public bool Add(Models.Department obj)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_departmentAdd", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@Name", obj.Name);

            return command.ExecuteNonQuery() > 0;
        }

        public bool Edit(Models.Department obj)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_departmentEdit", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Id", obj.Id);
            command.Parameters.AddWithValue("@Name", obj.Name);

            return command.ExecuteNonQuery() > 0;
        }

        public bool Delete(int id)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_departmentDelete", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Id", id);

            return command.ExecuteNonQuery() > 0;
        }

        public Models.Department GetById(int id)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_departmentGetById", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Id", id);

            return Mapper(command.ExecuteReader()).FirstOrDefault();
        }

        public Models.Department GetByName(string name)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_userGetByUsername", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Name", name);

            return Mapper(command.ExecuteReader()).FirstOrDefault();
        }

        //Object mapper; reader to model
        public IEnumerable<Models.Department> Mapper(SqlDataReader reader)
        {
            var listObj = new List<Models.Department>();
            try
            {
                while (reader.Read())
                {
                    listObj.Add(new Models.Department()
                    {
                        Id = reader.GetInt32("Id"),
                        Name = reader.GetString("Name")
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
