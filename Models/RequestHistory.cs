using System;
using System.Collections.Generic;

namespace Furlough.Models
{
    public partial class RequestHistory
    {
        public RequestHistory()
        {
            SpentDaysHistories = new HashSet<SpentDaysHistory>();
        }

        public int Id { get; set; }
        public int RequestId { get; set; }
        public int AlteredBy { get; set; }
        public DateTime AlteredOn { get; set; }
        public byte AlteredTo { get; set; }

        public virtual User AlteredByNavigation { get; set; } = null!;
        public virtual RequestStatus AlteredToNavigation { get; set; } = null!;
        public virtual Request Request { get; set; } = null!;
        public virtual ICollection<SpentDaysHistory> SpentDaysHistories { get; set; }
    }
}
