using System.ComponentModel.DataAnnotations;

namespace Furlough.Models
{
    public partial class Department
    {
        [Display(Name = "id", ResourceType = typeof(Resources.Models.Department.Display))]
        public int Id { get; set; }
        [Display(Name = "name", ResourceType = typeof(Resources.Models.Department.Display))]
        public string Name { get; set; } = null!;

    }
}
