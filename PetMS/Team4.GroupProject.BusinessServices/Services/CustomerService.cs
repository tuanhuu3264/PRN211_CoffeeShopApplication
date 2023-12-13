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
    public class CustomerService : ICustomerService
    {   
        public CustomerService() { }
        public bool Add(Customer customer)
        {
            var existingCustomer = CustomerRepository.GetInstance().GetOne(e => e.Phone == customer.Phone);
            if (existingCustomer != null) { return false; }
            if (CustomerRepository.GetInstance().Add(customer)) return true;
            return false;
        }

        public bool Delete(int id)
        {
            var existingCustomer = CustomerRepository.GetInstance().GetOne(c => c.Id == id);
            if (existingCustomer == null) { return false; }
            if (CustomerRepository.GetInstance().Remove(existingCustomer)) return true;
            return false;
        }

        public IEnumerable<Customer> GetAll() => CustomerRepository.GetInstance().Get(c => true);

        public Customer GetById(int id) => CustomerRepository.GetInstance().GetOne(c => c.Id == id);

        public Customer GetByPhone(string phone) => CustomerRepository.GetInstance().GetOne(c=>c.Phone == phone);

        public IEnumerable<Customer> Search(string keyword) => CustomerRepository.GetInstance().Get(c=>c.Name.ToLower().Contains(keyword.ToLower()) || c.Phone.Contains(keyword));

        public bool Update(Customer customer)
        {
            var existingCustomer = CustomerRepository.GetInstance().GetOne(c => c.Id == customer.Id);
            var condtionPhone = CustomerRepository.GetInstance().GetOne(c => c.Id != customer.Id && c.Phone == customer.Phone);

            if (existingCustomer == null) { return false; }
            if (condtionPhone != null) { return false; }
            existingCustomer.Name = customer.Name;
            existingCustomer.Phone = customer.Phone;
            existingCustomer.Address = customer.Address;
            if (CustomerRepository.GetInstance().Update(existingCustomer)) return true;
            return false;
        }
    }
}
