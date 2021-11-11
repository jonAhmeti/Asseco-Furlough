using System;
using System.Collections.Generic;

namespace Furlough.Models
{
    public partial class RequestStatus
    {
        public RequestStatus()
        {
            RequestHistories = new HashSet<RequestHistory>();
            Requests = new HashSet<Request>();
        }

        public byte Id { get; set; }
        public string Type { get; set; } = null!;

        public virtual ICollection<RequestHistory> RequestHistories { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}
