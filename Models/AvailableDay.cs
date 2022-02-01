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
        public int Medical { get; set; }
        [Display(Name = "yearly", ResourceType = typeof(Resources.Models.AvailableDay.Display))]
        public int Yearly { get; set; }
        [Display(Name = "overtime", ResourceType = typeof(Resources.Models.AvailableDay.Display))]
        public int Overtime { get; set; }
        [Display(Name = "birth", ResourceType = typeof(Resources.Models.AvailableDay.Display))]
        public int Birth { get; set; }
        [Display(Name = "child", ResourceType = typeof(Resources.Models.AvailableDay.Display))]
        public int Child { get; set; }
        [Display(Name = "marriage", ResourceType = typeof(Resources.Models.AvailableDay.Display))]
        public int Marriage { get; set; }
        [Display(Name = "unpaid", ResourceType = typeof(Resources.Models.AvailableDay.Display))]
        public int Unpaid { get; set; }
        [Display(Name = "bloodDonation", ResourceType = typeof(Resources.Models.AvailableDay.Display))]
        public int BloodDonation { get; set; }
        [Display(Name = "maternity", ResourceType = typeof(Resources.Models.AvailableDay.Display))]
        public int Maternity { get; set; }
        [Display(Name = "deathOfRelative", ResourceType = typeof(Resources.Models.AvailableDay.Display))]
        public int DeathOfRelative { get; set; }
    }
}
