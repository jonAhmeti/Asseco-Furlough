using Furlough.DAL.Models;

namespace Furlough.Models.Mapper
{
    public class DalMapper
    {
        public DAL.Models.AvailableDay DalAvailableDayMap(AvailableDay obj)
        {
            return new DAL.Models.AvailableDay
            {
                EmployeeId = obj.EmployeeId,
                Medical = obj.Medical,
                Yearly = obj.Yearly,
                Child = obj.Child,
                Birth = obj.Birth,
                BloodDonation = obj.BloodDonation,
                DeathOfRelative = obj.DeathOfRelative,
                Marriage = obj.Marriage,
                Maternity = obj.Maternity,
                Overtime = obj.Overtime,
                Unpaid = obj.Unpaid
            };
        }

        public DAL.Models.User DalUserMap(User obj)
        {
            return new DAL.Models.User
            {
                Id = obj.Id,
                Username = obj.Username,
                Password = obj.Password,
                InsertDate = obj.InsertDate,
                RoleId = obj.RoleId,
                LUD = obj.LUD,
                LUBUserId = obj.LUBUserId,
                LUN = obj.LUN,
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
                Phone = obj.Phone,
                WorkStartDate = obj.WorkStartDate,
                LUBUserId = obj.LUBUserId
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

        public DAL.Models.DepartmentPositions DalDepartmentRolesMap(DepartmentPositions obj)
        {
            return new DAL.Models.DepartmentPositions
            {
                Id = obj.Id,
                DepartmentId = obj.DepartmentId,
                PositionId = obj.PositionId
            };
        }

        public DAL.Models.Request DalRequestMap(Request obj)
        {
            //var datesString = "";
            //for (int i = 0; i < obj.Dates.Count(); i++)
            //{
            //    var date = $"{obj.Dates.ElementAt(i).Year}/{obj.Dates.ElementAt(i).Month}/{obj.Dates.ElementAt(i).Day}";
            //    if (i == obj.Dates.Count() - 1)
            //    {
            //        datesString += date;
            //    }
            //    else
            //    {
            //        datesString += date + ",";
            //    }
            //}

            return new DAL.Models.Request
            {
                Id = obj.Id,
                DaysAmount = obj.DaysAmount,
                RequestedByUserId = obj.RequestedByUserId,
                RequestedOn = obj.RequestedOn,
                RequestStatusId = obj.RequestStatusId == 0 ? (byte)1 : obj.RequestStatusId,
                RequestTypeId = obj.RequestTypeId,
                Dates = obj.Dates,
                Reason = obj.Reason,
                LUD = obj.LUD,
                LUN = obj.LUN,
                LUBUserId = obj.LUBUserId
            };
        }

        public DAL.Models.RequestType DalRequestTypeMap(RequestType obj)
        {
            return new DAL.Models.RequestType
            {
                Id = obj.Id,
                Type = obj.Type,
                Description = obj.Description
            };
        }
    }
}
