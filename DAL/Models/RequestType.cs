using System;
using System.Collections.Generic;

namespace Furlough.DAL.Models
{
    public partial class RequestType
    {
        public RequestType()
        {
            Requests = new HashSet<Request>();
        }

        public int Id { get; set; }
        public string Type { get; set; } = null!;

        public virtual ICollection<Request> Requests { get; set; }
    }
}
