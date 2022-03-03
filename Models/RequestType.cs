using System;
using System.Collections.Generic;

namespace Furlough.Models
{
    public partial class RequestType
    {
        #region Icon Types
        public static readonly string[] IconMedical = { "<i class=\"fa-solid fa-stethoscope\"></i>", "#A0CED9" };
        public static readonly string[] IconPrevYearly = {"<i class=\"fa-regular fa-calendar\"></i>", "#449DD1"};
        public static readonly string[] IconBloodDonation = {"<span class=\"material-icons-round\">bloodtype</span>", "#DE3C4B"};
        public static readonly string[] IconYearly = {"<i class=\"fa-solid fa-calendar-days\"></i>", "#FFC09F"};
        public static readonly string[] IconOvertime = {"<i class=\"fa-solid fa-business-time\"></i>", "#3bc199"};
        public static readonly string[] IconChild = {"<span class=\"material-icons-round\">family_restroom</span>", "#f9ad3b"};
        public static readonly string[] IconMarriage = {"<i class=\"fa-solid fa-martini-glass-citrus\"></i>", "#917bbf"};
        public static readonly string[] IconUnpaid = {"<span class=\"material-icons-round\">money_off</span>", "#A0CED9"};
        public static readonly string[] IconMaternity = {"<span class=\"material-icons-round\">chair</span>", "#F7B801"};
        public static readonly string[] IconBirth = {"<span class=\"material-icons-round\">pregnant_woman</span>", "#D33F49"};
        public static readonly string[] IconDeathOfRelative = {"<i class=\"fa-solid fa-heart\"></i>", "#DF1B48"};
        #endregion

        public int Id { get; set; }
        public string Type { get; set; } = null!;

        public string Description { get; set; }

        public virtual ICollection<Request> Requests { get; set; }
    }
}
