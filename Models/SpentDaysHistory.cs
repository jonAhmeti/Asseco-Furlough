using System;
using System.Collections.Generic;

namespace Furlough.Models
{
    public partial class SpentDaysHistory
    {
        public int Id { get; set; }
        public int? RequestHistoryId { get; set; }
        public int BaseDays { get; set; }
        public int BonusDays { get; set; }
        public int PrevYearDays { get; set; }

        public virtual RequestHistory? RequestHistory { get; set; }
    }
}
