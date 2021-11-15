using System;
using System.Collections.Generic;

namespace Furlough.DAL.Models
{
    public partial class Request
    {
        public int Id { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateUntil { get; set; }
        public int? RequestedBy { get; set; }
        public DateTime RequestedOn { get; set; }
        public byte Status { get; set; }
        public int PaidDays { get; set; }
        public int Type { get; set; }
    }
}
