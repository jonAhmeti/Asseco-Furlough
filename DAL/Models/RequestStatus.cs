using System;
using System.Collections.Generic;

namespace Furlough.DAL.Models
{
    public partial class RequestStatus
    {
        public byte Id { get; set; }
        public string Type { get; set; } = null!;

    }
}
