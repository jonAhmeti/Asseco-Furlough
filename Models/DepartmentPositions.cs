using System.ComponentModel.DataAnnotations;

namespace Furlough.Models
{
    public class DepartmentPositions
    {
        public int Id { get; set; }

        [Display(Name = "department", ResourceType = typeof(Resources.Models.DepartmentPositions.Display))]
        public int DepartmentId { get; set; }

        [Display(Name = "position", ResourceType = typeof(Resources.Models.DepartmentPositions.Display))]
        public int PositionId { get; set; }
    }
}
