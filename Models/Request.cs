using System;
using System.Collections.Generic;

namespace Furlough.Models
{
    public partial class Request
    {
        public Request()
        {
            RequestHistories = new HashSet<RequestHistory>();
        }

        public int Id { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateUntil { get; set; }
        public int? RequestedBy { get; set; }
        public DateTime RequestedOn { get; set; }
        public byte Status { get; set; }
        public int PaidDays { get; set; }
        public int Type { get; set; }

        public virtual User? RequestedByNavigation { get; set; }
        public virtual RequestStatus StatusNavigation { get; set; } = null!;
        public virtual RequestType TypeNavigation { get; set; } = null!;
        public virtual ICollection<RequestHistory> RequestHistories { get; set; }
    }
}
