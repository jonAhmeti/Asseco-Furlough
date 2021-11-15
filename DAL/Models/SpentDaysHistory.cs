﻿using System;
using System.Collections.Generic;

namespace Furlough.DAL.Models
{
    public partial class SpentDaysHistory
    {
        public int Id { get; set; }
        public int? RequestHistoryId { get; set; }
        public int BaseDays { get; set; }
        public int BonusDays { get; set; }
        public int PrevYearDays { get; set; }
    }
}
