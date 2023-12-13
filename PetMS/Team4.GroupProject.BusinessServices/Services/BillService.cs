using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team4.GroupProject.BusinessObjects.Models;
using Team4.GroupProject.BusinessServices.Interface;
using Team4.GroupProject.Repositories;

namespace Team4.GroupProject.BusinessServices.Services
{
    public class BillService : IBillService
    {   
        public BillService() 
        { 
        }
        public bool Add(Bill bill)
        {
            if(BillRepository.GetInstance().Add(bill)) return true;
            return false;
        }

        public bool Delete(int orderId)
        {
            var listBillDetails = BillDetailRepository.GetInstance().Get((bd) => bd.BillId == orderId);
            foreach(var billDetail in listBillDetails)
            {
                BillDetailRepository.GetInstance().Remove(billDetail);
            }
            var bill = BillRepository.GetInstance().GetOne((b) => b.Id == orderId);
            if (BillRepository.GetInstance().Remove(bill)) return true;
            return false;
        }

        public IEnumerable<Bill> GetAll() => BillRepository.GetInstance().Get((b)=>true);

        public IEnumerable<Bill> GetByCustomerId(int customerId) => BillRepository.GetInstance().Get(b => b.CustomerId == customerId);

        public Bill GetByCustomerIdAndDate(int customerId, DateTime date) => BillRepository.GetInstance().GetOne(b=>b.CustomerId==customerId && b.Date==date);

        public IEnumerable<Bill> GetByEmployeeId(int employeeId) => BillRepository.GetInstance().Get(b=>b.EmployeeId==employeeId);
        public Bill GetById(int id) => BillRepository.GetInstance().GetOne((b) => b.Id == id); 

        public IEnumerable<Bill> Search(string keyword)
        {
            throw new NotImplementedException();
        }

        public bool Update(Bill bill)
        {
            var existingBill = BillRepository.GetInstance().GetOne((b)=>b.Id==bill.Id);
            if (existingBill == null) return false; 
            existingBill.Id = bill.Id;
            existingBill.Date = bill.Date;
            existingBill.CustomerId = bill.CustomerId;
            existingBill.EmployeeId = bill.EmployeeId;
            existingBill.Total = bill.Total;
            if(BillRepository.GetInstance().Update(existingBill)) return true;
            return false;
        }
    }
}
