using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Furlough.Models
{
    public partial class Employee
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }
        public DateTime JoinDate { get; set; }
        [Required]
        public int PositionId { get; set; }
        [Required]
        public int DepartmentId { get; set; }
        public string Email { get; set; } = null!;
        [Required]
        public string Name { get; set; } = null!;
        public string? Phone { get; set; }
        [Required]
        public DateTime WorkStartDate { get; set; }
        public int LUBUserId { get; set; }
    }
}
