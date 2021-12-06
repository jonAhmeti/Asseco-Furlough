using System;
using System.Collections.Generic;

namespace Furlough.DAL.Models
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

        public virtual Department Department { get; set; } = null!;
        public virtual Position Position { get; set; } = null!;
        public virtual User User { get; set; } = null!;
        public virtual AvailableDay AvailableDay { get; set; } = null!;
    }
}
