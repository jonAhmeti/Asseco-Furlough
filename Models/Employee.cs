using System;
using System.Collections.Generic;

namespace Furlough.Models
{
    public partial class Employee
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime JoinDate { get; set; }
        public int PositionId { get; set; }
        public int DepartmentId { get; set; }
        public string Email { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Phone { get; set; }
        public DateTime WorkStartDate { get; set; }
        public int LUBUserId { get; set; }
    }
}
