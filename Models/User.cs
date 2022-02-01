using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Furlough.Models
{
    public partial class User
    {
        [Display(Name = "id", ResourceType = typeof(Resources.Models.User.Display))]
        public int Id { get; set; }
        [Display(Name = "roleId", ResourceType = typeof(Resources.Models.User.Display))]
        public int RoleId { get; set; }
        [Display(Name = "insertDate", ResourceType = typeof(Resources.Models.User.Display))]
        public DateTime InsertDate { get; set; }
        [Display(Name = "username", ResourceType = typeof(Resources.Models.User.Display))]
        public string Username { get; set; } = null!;
        [Display(Name = "password", ResourceType = typeof(Resources.Models.User.Display))]
        public string Password { get; set; } = null!;
        [Display(Name = "lubUserId", ResourceType = typeof(Resources.Models.User.Display))]
        public int LUBUserId { get; set; }
        [Display(Name = "lud", ResourceType = typeof(Resources.Models.User.Display))]
        public DateTime LUD { get; set; }
        [Display(Name = "lun", ResourceType = typeof(Resources.Models.User.Display))]
        public int LUN { get; set; }
    }
}
