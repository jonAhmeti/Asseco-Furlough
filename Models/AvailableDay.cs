using System;
using System.Collections.Generic;

namespace Furlough.Models
{
    public partial class AvailableDay
    {
        public int EmployeeId { get; set; }
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
