using System;
using System.Collections.Generic;

namespace Furlough.DAL.Models
{
    public partial class AvailableDay
    {
        public int EmployeeId { get; set; }
        public int BaseDays { get; set; }
        public int BonusDays { get; set; }
        public int PrevYearDays { get; set; }
        public int SpentDays { get; set; }
    }
}
