using System;
using System.Collections.Generic;

namespace Furlough.DAL.Models
{
    public partial class RequestHistory
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int AlteredByUserId { get; set; }
        public DateTime AlteredOn { get; set; }
        public string? Message { get; set; }
        public int PreviousRequestStatusId { get; set; }
        public int PreviousRequestTypeId { get; set; }
        public string PreviousDates { get; set; }
    }
}
