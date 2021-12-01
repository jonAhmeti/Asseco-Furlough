using System.ComponentModel.DataAnnotations;

namespace Furlough.Models
{
    public class DepartmentPositions
    {
        public int Id { get; set; }

        [Display(Name = "Department")]
        public int DepartmentId { get; set; }

        [Display(Name = "Position")]
        public int PositionId { get; set; }
    }
}
