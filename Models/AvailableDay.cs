using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Furlough.Models
{
    public partial class AvailableDay
    {
        [Display(Name = "employeeId", ResourceType = typeof(Resources.Models.AvailableDay.Display))]
        public int EmployeeId { get; set; }
        [Display(Name = "medical", ResourceType = typeof(Resources.Models.AvailableDay.Display))]
        public decimal Medical { get; set; }
        [Display(Name = "yearly", ResourceType = typeof(Resources.Models.AvailableDay.Display))]
        public decimal Yearly { get; set; }
        [Display(Name = "overtime", ResourceType = typeof(Resources.Models.AvailableDay.Display))]
        public decimal Overtime { get; set; }
        [Display(Name = "birth", ResourceType = typeof(Resources.Models.AvailableDay.Display))]
        public decimal Birth { get; set; }
        [Display(Name = "child", ResourceType = typeof(Resources.Models.AvailableDay.Display))]
        public decimal Child { get; set; }
        [Display(Name = "marriage", ResourceType = typeof(Resources.Models.AvailableDay.Display))]
        public decimal Marriage { get; set; }
        [Display(Name = "unpaid", ResourceType = typeof(Resources.Models.AvailableDay.Display))]
        public decimal Unpaid { get; set; }
        [Display(Name = "bloodDonation", ResourceType = typeof(Resources.Models.AvailableDay.Display))]
        public decimal BloodDonation { get; set; }
        [Display(Name = "maternity", ResourceType = typeof(Resources.Models.AvailableDay.Display))]
        public decimal Maternity { get; set; }
        [Display(Name = "deathOfRelative", ResourceType = typeof(Resources.Models.AvailableDay.Display))]
        public decimal DeathOfRelative { get; set; }
    }
}
