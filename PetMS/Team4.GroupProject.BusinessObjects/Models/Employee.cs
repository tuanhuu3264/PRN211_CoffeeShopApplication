using System;
using System.Collections.Generic;

namespace Team4.GroupProject.BusinessObjects.Models
{
    public partial class Employee
    {
        public Employee()
        {
            Bills = new HashSet<Bill>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Password { get; set; } = null!;

        public virtual ICollection<Bill> Bills { get; set; }
    }
}
