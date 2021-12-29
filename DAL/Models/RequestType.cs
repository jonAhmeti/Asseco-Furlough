using System;
using System.Collections.Generic;

namespace Furlough.DAL.Models
{
    public partial class RequestType
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public string Description { get; set; }
    }
}
