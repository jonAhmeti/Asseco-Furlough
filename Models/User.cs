using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Furlough.Models
{
    public partial class User
    {
        //public User()
        //{
        //    Employees = new HashSet<Employee>();
        //    InverseUpdateByNavigation = new HashSet<User>();
        //    RequestHistories = new HashSet<RequestHistory>();
        //    Requests = new HashSet<Request>();
        //}

        public int Id { get; set; }
        
        [Display(Name = "Role")]
        public int RoleId { get; set; }

        [Display(Name = "Insert date")]
        public DateTime InsertDate { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

        [Display(Name = "Last updated by")]
        public int LUBUserId { get; set; }
        [Display(Name = "Update date")]
        public DateTime LUD { get; set; }
        [Display(Name = "Total no. of updates")]
        public int LUN { get; set; }

        //    public virtual Role Role { get; set; } = null!;
        //    public virtual User? UpdateByNavigation { get; set; }
        //    public virtual ICollection<Employee> Employees { get; set; }
        //    public virtual ICollection<User> InverseUpdateByNavigation { get; set; }
        //    public virtual ICollection<RequestHistory> RequestHistories { get; set; }
        //    public virtual ICollection<Request> Requests { get; set; }
        //}
    }
}
