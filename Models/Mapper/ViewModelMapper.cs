namespace Furlough.Models.Mapper
{
    public class ViewModelMapper
    {
        public Models.User UserMap(DAL.Models.User obj)
        {
            return new Models.User
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

        public Models.Employee EmployeeMap(DAL.Models.Employee obj)
        {
            return new Models.Employee
            {
                Id = obj.Id,
                UserId = obj.UserId,
                Email = obj.Email,
                Name = obj.Name,
                PositionId = obj.PositionId,
                DepartmentId = obj.DepartmentId,
                JoinDate = obj.JoinDate,
            };
        }
    }
}
