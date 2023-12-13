using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team4.GroupProject.BusinessObjects.Models;

namespace Team4.GroupProject.BusinessServices.Interface
{
    public interface ICustomerService
    {
        public IEnumerable<Customer> GetAll(); 
        public Customer GetById(int id);
        public IEnumerable<Customer> Search(string keyword);
        public bool Add(Customer customer);
        public bool Update(Customer customer);
        public bool Delete(int id);
        public Customer GetByPhone(string phone);

    }
}
