using System;
using System.Collections.Generic;

namespace Furlough.DAL.Models
{
    public partial class RequestHistory
    {
        public int Id { get; set; }
        public int RequestId { get; set; }
        public int AlteredBy { get; set; }
        public DateTime AlteredOn { get; set; }
        public byte AlteredTo { get; set; }

    }
}
