using System;
using System.Collections.Generic;

namespace Furlough.DAL.Models
{
    public partial class User
    {
        public User()
        {
            Employees = new HashSet<Employee>();
            InverseUpdateByNavigation = new HashSet<User>();
            RequestHistories = new HashSet<RequestHistory>();
            Requests = new HashSet<Request>();
        }

        public int Id { get; set; }
        public int RoleId { get; set; }
        public DateTime InsertDate { get; set; }
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public DateTime LUD { get; set; }
        public int LUN { get; set; }
        public int LUBUserId { get; set; }

        public virtual Role Role { get; set; } = null!;
        public virtual User? UpdateByNavigation { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
        public virtual ICollection<User> InverseUpdateByNavigation { get; set; }
        public virtual ICollection<RequestHistory> RequestHistories { get; set; }
        public virtual ICollection<Request> Requests { get; set; }
    }
}
