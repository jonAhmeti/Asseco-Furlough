using System;
using System.Collections.Generic;

namespace Furlough.DAL.Models
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
        public int? RequestedByUserId { get; set; }
        public DateTime RequestedOn { get; set; }
        public byte RequestStatusId { get; set; }
        public int PaidDays { get; set; }
        public int RequestTypeId { get; set; }
        public virtual RequestStatus RequestStatus { get; set; } = null!;
        public virtual RequestType RequestType { get; set; } = null!;
        public virtual User? RequestedByNavigation { get; set; }
        public virtual ICollection<RequestHistory> RequestHistories { get; set; }
    }
}
