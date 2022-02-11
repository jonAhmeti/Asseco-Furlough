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
        public decimal Medical { get; set; }
        public decimal Yearly { get; set; }
        public decimal Overtime { get; set; }
        public decimal Birth { get; set; }
        public decimal Child { get; set; }
        public decimal Marriage { get; set; }
        public decimal Unpaid { get; set; }
        public decimal BloodDonation { get; set; }
        public decimal Maternity { get; set; }
        public decimal DeathOfRelative { get; set; }
    }
}
