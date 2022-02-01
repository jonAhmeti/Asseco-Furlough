using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Furlough.Models
{
    public partial class Employee
    {
        [Display(Name = "id", ResourceType = typeof(Resources.Models.Employee.Display))]
        public int Id { get; set; }
        [Required]
        [Display(Name = "userId", ResourceType = typeof(Resources.Models.Employee.Display))]
        public int UserId { get; set; }
        public DateTime JoinDate { get; set; }
        [Required]
        [Display(Name = "position", ResourceType = typeof(Resources.Models.Employee.Display))]
        public int PositionId { get; set; }
        [Required]
        [Display(Name = "department", ResourceType = typeof(Resources.Models.Employee.Display))]
        public int DepartmentId { get; set; }
        [Required]
        [Display(Name = "email", ResourceType = typeof(Resources.Models.Employee.Display))]
        public string Email { get; set; } = null!;
        [Required]
        [Display(Name = "name", ResourceType = typeof(Resources.Models.Employee.Display))]
        public string Name { get; set; } = null!;
        [Display(Name = "phone", ResourceType = typeof(Resources.Models.Employee.Display))]
        public string? Phone { get; set; }
        [Required]
        [Display(Name = "workStartDate", ResourceType = typeof(Resources.Models.Employee.Display))]
        public DateTime WorkStartDate { get; set; }
        [Display(Name = "lubUserId", ResourceType = typeof(Resources.Models.Employee.Display))]
        public int LUBUserId { get; set; }
    }
}
