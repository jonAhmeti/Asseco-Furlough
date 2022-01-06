using System.ComponentModel.DataAnnotations;

namespace Furlough.DAL.Models
{
    public class AvailableDayGetAll
    {
        public int EmployeeId { get; set; }
        [Display(Name = "Employee")]
        public string EmployeeName { get; set; }
        [Display(Name = "Department")]
        public string EmployeeDepartment { get; set; }
        [Display(Name = "Position")]
        public string EmployeePosition { get; set; }
        public int Medical { get; set; }
        public int Yearly { get; set; }
        public int Overtime { get; set; }
        public int Birth { get; set; }
        public int Child { get; set; }
        public int Marriage { get; set; }
        public int Unpaid { get; set; }
        public int BloodDonation { get; set; }
        public int Maternity { get; set; }
        public int DeathOfRelative { get; set; }
    }
}
