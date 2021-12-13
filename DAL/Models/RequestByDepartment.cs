﻿namespace Furlough.DAL.Models
{
    public class RequestByDepartment
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public string EmployeeName { get; set; }
        public int EmployeeId { get; set; }
        public int RequestId { get; set; }
        public int RequestedByUserId { get; set; }
        public DateTime RequestDateFrom { get; set; }
        public DateTime RequestDateUntil { get; set; }
        public DateTime RequestedOn { get; set; }
    }
}