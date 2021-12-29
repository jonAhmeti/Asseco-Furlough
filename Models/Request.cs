using System;
using System.Collections.Generic;

namespace Furlough.Models
{
    public partial class Request
    {
        public int Id { get; set; }
        public IEnumerable<DateTime> Dates { get; set; }
        public int RequestedByUserId { get; set; }
        public DateTime RequestedOn { get; set; }
        public byte RequestStatusId { get; set; }
        public int DaysAmount { get; set; }
        public int RequestTypeId { get; set; }
        public string Reason { get; set; }
    }
}
