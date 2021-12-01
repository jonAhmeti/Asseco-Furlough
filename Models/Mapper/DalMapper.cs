using Furlough.DAL.Models;

namespace Furlough.Models.Mapper
{
    public class DalMapper
    {
        public DAL.Models.User DalUserMap(User obj)
        {
            return new DAL.Models.User
            {
                Id = obj.Id,
                Username = obj.Username,
                Password = obj.Password,
                InsertDate = obj.InsertDate,
                RoleId = obj.RoleId,
                UpdateDate = obj.UpdateDate,
                UpdateBy = obj.UpdateBy,
            };
        }

        public DAL.Models.Employee DalEmployeeMap(Employee obj)
        {
            return new DAL.Models.Employee
            {
                Id=obj.Id,
                UserId = obj.UserId,
                Email = obj.Email,
                Name = obj.Name,
                PositionId = obj.PositionId,
                DepartmentId = obj.DepartmentId,
                JoinDate = obj.JoinDate,
            };
        }

        public DAL.Models.Department DalDepartmentMap(Department obj)
        {
            return new DAL.Models.Department
            {
                Id = obj.Id,
                Name = obj.Name
            };
        }

        public DAL.Models.Position DalPositionMap(Position obj)
        {
            return new DAL.Models.Position
            {
                Id = obj.Id,
                Title = obj.Title
            };
        }

        public DAL.Models.Role DalRoleMap(Role obj)
        {
            return new DAL.Models.Role
            {
                Id = obj.Id,
                Description = obj.Description,
                Title = obj.Title
            };
        }
    }
}
