using System;
using System.Collections.Generic;

namespace Furlough.Models
{
    public partial class Position
    {
        //public Position()
        //{
        //    Employees = new HashSet<Employee>();
        //}

        public int Id { get; set; }
        public string Title { get; set; } = null!;

        //public virtual ICollection<Employee> Employees { get; set; }
    }
}
