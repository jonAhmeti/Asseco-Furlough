using System;
using System.Collections.Generic;

namespace Furlough.DAL.Models
{
    public partial class Role
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;

    }
}
