using System;
using System.Collections.Generic;

namespace Team4.GroupProject.BusinessObjects.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Bills = new HashSet<Bill>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }

        public virtual ICollection<Bill> Bills { get; set; }
    }
}
