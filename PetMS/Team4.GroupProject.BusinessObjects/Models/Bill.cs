using System;
using System.Collections.Generic;

namespace Team4.GroupProject.BusinessObjects.Models
{
    public partial class Bill
    {
        public Bill()
        {
            BillDetails = new HashSet<BillDetail>();
        }

        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public int EmployeeId { get; set; }
        public decimal? Total { get; set; }

        public virtual Customer Customer { get; set; } = null!;
        public virtual Employee Employee { get; set; } = null!;
        public virtual ICollection<BillDetail> BillDetails { get; set; }
    }
}
