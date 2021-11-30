﻿using Microsoft.Data.SqlClient;
using System.Data;

namespace Furlough.DAL
{
    public class Employee
    {
        private readonly FurloughContext _context;

        public Employee(FurloughContext context)
        {
            _context = context;
        }
        public bool Add(Models.Employee obj)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_employeeAdd", connection)
            {
                CommandType = CommandType.StoredProcedure
            };
            command.Parameters.AddWithValue("@UserId", obj.UserId);
            command.Parameters.AddWithValue("@PositionId", obj.PositionId);
            command.Parameters.AddWithValue("@DepartmentId", obj.DepartmentId);
            command.Parameters.AddWithValue("@Email", obj.Email);
            command.Parameters.AddWithValue("@Name", obj.Name);
            command.Parameters.AddWithValue("@JoinDate", obj.JoinDate);

            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }

        public bool Edit(Models.Employee obj)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_employeeEdit", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Id", obj.Id);
            command.Parameters.AddWithValue("@PositionId", obj.PositionId);
            command.Parameters.AddWithValue("@DepartmentId", obj.DepartmentId);
            command.Parameters.AddWithValue("@Email", obj.Email);
            command.Parameters.AddWithValue("@Name", obj.Name);

            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }

        public bool Delete(int id)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_employeeDelete", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            return command.ExecuteNonQuery() > 0;
        }

        public Models.Employee GetById(int id)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_employeeGetById", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            return Mapper(command.ExecuteReader()).FirstOrDefault();
        }

        public Models.Employee GetByUserId(int userId)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_employeeGetByUserId", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@UserId", userId);

            connection.Open();
            return Mapper(command.ExecuteReader()).FirstOrDefault();
        }

        public Models.Employee GetByEmail(string email)
        {
            using var connection = new SqlConnection(_context.GetConnection());
            using var command = new SqlCommand("sp_employeeGetByEmail", connection)
            {
                CommandType = CommandType.StoredProcedure
            };

            command.Parameters.AddWithValue("@Email", email);

            connection.Open();
            return Mapper(command.ExecuteReader()).FirstOrDefault();
        }

        //Object mapper; reader to model
        public IEnumerable<Models.Employee> Mapper(SqlDataReader reader)
        {
            var listObj = new List<Models.Employee>();
            try
            {
                while (reader.Read())
                {
                    listObj.Add(new Models.Employee()
                    {
                        Id = reader.GetInt32("Id"),
                        UserId = reader.GetInt32("UserId"),
                        PositionId = reader.GetInt32("PositionId"),
                        Name = reader.GetString("Name"),
                        DepartmentId = reader.GetInt32("DepartmentId"),
                        Email = reader.GetString("Email"),
                        JoinDate = reader.GetDateTime("JoinDate")
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
