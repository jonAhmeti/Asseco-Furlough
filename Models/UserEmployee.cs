using System.ComponentModel.DataAnnotations;

namespace Furlough.Models
{
    public class UserEmployee
    {
        //User Section
        #region User
        [Display(Name = "userId", ResourceType = typeof(Resources.Models.UserEmployee.Display))]
        public int UserId { get; set; }
        [Display(Name = "roleId", ResourceType = typeof(Resources.Models.UserEmployee.Display))]
        public int RoleId { get; set; }
        [Display(Name = "insertDate", ResourceType = typeof(Resources.Models.UserEmployee.Display))]
        public DateTime InsertDate { get; set; }
        [Display(Name = "username", ResourceType = typeof(Resources.Models.UserEmployee.Display))]
        public string Username { get; set; } = null!;
        [Display(Name = "password", ResourceType = typeof(Resources.Models.UserEmployee.Display))]
        public string? Password { get; set; }
        [Display(Name = "lubUserId", ResourceType = typeof(Resources.Models.UserEmployee.Display))]
        public int LUBUserId { get; set; }
        #endregion

        //Employee Section
        #region Employee
        [Display(Name = "employeeId", ResourceType = typeof(Resources.Models.UserEmployee.Display))]
        public int EmployeeId { get; set; }
        [Required]
        [Display(Name = "position", ResourceType = typeof(Resources.Models.UserEmployee.Display))]
        public int PositionId { get; set; }
        [Required]
        [Display(Name = "department", ResourceType = typeof(Resources.Models.UserEmployee.Display))]
        public int DepartmentId { get; set; }
        [Required]
        [Display(Name = "email", ResourceType = typeof(Resources.Models.UserEmployee.Display))]
        public string Email { get; set; } = null!;
        [Required]
        [Display(Name = "name", ResourceType = typeof(Resources.Models.UserEmployee.Display))]
        public string Name { get; set; } = null!;
        [Display(Name = "phone", ResourceType = typeof(Resources.Models.UserEmployee.Display))]
        public string? Phone { get; set; }
        [Required]
        [Display(Name = "workStartDate", ResourceType = typeof(Resources.Models.UserEmployee.Display))]
        public DateTime WorkStartDate { get; set; }
        #endregion
    }
}
