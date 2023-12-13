using System.Collections.Generic;
using Team4.GroupProject.BusinessObjects.Models;

namespace Team4.GroupProject.BusinessServices.Interface
{
    public interface IBillService
    {
        IEnumerable<Bill> GetAll();
        Bill GetById(int id);
        IEnumerable<Bill> Search(string keyword);
        bool Add(Bill bill);
        bool Update(Bill bill);
        bool Delete(int orderId);
        Bill GetByCustomerIdAndDate(int customerId, DateTime date);
        IEnumerable<Bill> GetByEmployeeId(int employeeId);
        IEnumerable<Bill> GetByCustomerId(int customerId);
    }
}
