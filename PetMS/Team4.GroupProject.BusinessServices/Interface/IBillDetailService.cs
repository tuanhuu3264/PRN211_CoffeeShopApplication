using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team4.GroupProject.BusinessObjects.Models;

namespace Team4.GroupProject.BusinessServices.Interface
{
    public interface IBillDetailService
    {
        IEnumerable<BillDetail> GetAll();
        IEnumerable<BillDetail> GetByBillId(int orderId);
        bool Add(BillDetail category);
        bool AddRange (List<BillDetail> list); 
        bool Update(BillDetail category);
        bool Delete(int orderId);
        IEnumerable<BillDetail> GetByProductId(int productId);
     
    }
}
