using System.ComponentModel.DataAnnotations;

namespace Furlough.Models
{
    public class DepartmentRoles
    {
        public int Id { get; set; }

        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [Display(Name = "Role")]
        public int RoleId { get; set; }
    }
}
