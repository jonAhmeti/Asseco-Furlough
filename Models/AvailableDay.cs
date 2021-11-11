using System;
using System.Collections.Generic;

namespace Furlough.Models
{
    public partial class AvailableDay
    {
        public int EmployeeId { get; set; }
        public int BaseDays { get; set; }
        public int BonusDays { get; set; }
        public int PrevYearDays { get; set; }
        public int SpentDays { get; set; }

        public virtual Employee Employee { get; set; } = null!;
    }
}
