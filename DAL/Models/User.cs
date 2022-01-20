using System;
using System.Collections.Generic;

namespace Furlough.DAL.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public int RoleId { get; set; }
        public DateTime InsertDate { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime LUD { get; set; }
        public int LUN { get; set; }
        public int LUBUserId { get; set; }
    }
}
