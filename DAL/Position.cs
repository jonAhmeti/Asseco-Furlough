using Microsoft.Data.SqlClient;
using System.Data;

namespace Furlough.DAL
{
    public class Position
    {
        private readonly FurloughContext _context;

        public Position(FurloughContext context)
        {
            _context = context;
        }

        public bool Add(string title)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_positionAdd", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

           command.Parameters.AddWithValue("@Title", title);

            return command.ExecuteNonQuery() > 0;
        }

        public bool Edit(Models.Position obj)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_positionEdit", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Id", obj.Id);
            command.Parameters.AddWithValue("@Title", obj.Title);

            return command.ExecuteNonQuery() > 0;
        }

        public bool Delete(int id)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_positionDelete", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Id", id);

            return command.ExecuteNonQuery() > 0;
        }

        public bool GetById(int id)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_positionGetById", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Id", id);

            return command.ExecuteNonQuery() > 0;
        }

        public bool GetByTitle(string title)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_positionGetByTitle", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Title", title);

            return command.ExecuteNonQuery() > 0;
        }


        //Object mapper; reader to model
        public Models.Position Mapper(SqlDataReader reader)
        {
            return new Models.Position()
            {
                Id = reader.GetInt32("Id"),
                Title = reader.GetString("Title")
            };
        }
    }
}
