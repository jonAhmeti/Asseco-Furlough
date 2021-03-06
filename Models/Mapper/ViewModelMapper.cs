namespace Furlough.Models.Mapper
{
    public class ViewModelMapper
    {
        public void UserEmployeeMap(Models.UserEmployee userEmployee, out Models.User user, out Models.Employee employee)
        {
            user = new Models.User
            {
                Id = userEmployee.UserId,
                InsertDate = userEmployee.InsertDate,
                LUBUserId = userEmployee.LUBUserId,
                Password = userEmployee.Password,
                Username = userEmployee.Username,
                RoleId = userEmployee.RoleId
            };

            employee = new Models.Employee
            {
                Email = userEmployee.Email,
                LUBUserId = userEmployee.LUBUserId,
                UserId = userEmployee.UserId,
                DepartmentId = userEmployee.DepartmentId,
                Id = userEmployee.EmployeeId,
                Name = userEmployee.Name,
                Phone = userEmployee.Phone,
                PositionId = userEmployee.PositionId,
                WorkStartDate = userEmployee.WorkStartDate
            };
        }

        public Models.User UserMap(DAL.Models.User obj)
        {
            return new Models.User
            {
                Id = obj.Id,
                Username = obj.Username,
                Password = "", //reset password
                InsertDate = obj.InsertDate,
                RoleId = obj.RoleId,
                LUD = obj.LUD,
                LUN = obj.LUN,
                LUBUserId = obj.LUBUserId,
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
                Phone = obj.Phone,
                WorkStartDate = obj.WorkStartDate
            };
        }

        public Models.Department DepartmentMap(DAL.Models.Department obj)
        {
            return new Models.Department
            {
                Id = obj.Id,
                Name = obj.Name
            };
        }

        public Models.Position PositionMap(DAL.Models.Position obj)
        {
            return new Models.Position
            {
                Id = obj.Id,
                Title = obj.Title
            };
        }

        public Models.Role RoleMap(DAL.Models.Role obj)
        {
            return new Models.Role
            {
                Title = obj.Title,
                Description = obj.Description,
                Id = obj.Id
            };
        }

        public Models.DepartmentPositions DepartmentRolesMap(DAL.Models.DepartmentPositions obj)
        {
            return new Models.DepartmentPositions
            {
                Id = obj.Id,
                DepartmentId = obj.DepartmentId,
                PositionId = obj.PositionId
            };
        }

        public Models.Request RequestMap(DAL.Models.Request obj)
        {
            //var datesStringArray = obj.Dates.Split(",");
            //var datesArray = new DateTime[datesStringArray.Length];
            //for (int i = 0; i < datesStringArray.Length; i++)
            //{
            //    var YYYYMMDD = datesStringArray[i].Split('/');
            //    datesArray[i] = (new DateTime(int.Parse(YYYYMMDD[0]), int.Parse(YYYYMMDD[1]), int.Parse(YYYYMMDD[2])));
            //}
            return new Models.Request
            {
                Id = obj.Id,
                Dates = obj.Dates,
                DaysAmount = obj.DaysAmount,
                RequestedByUserId = obj.RequestedByUserId,
                RequestTypeId = obj.RequestTypeId,
                RequestedOn = obj.RequestedOn,
                RequestStatusId = obj.RequestStatusId,
                Reason = obj.Reason,
                LUD = obj.LUD,
                LUN = obj.LUN,
                LUBUserId = obj.LUBUserId
            };
        }
    }
}
