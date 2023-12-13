using System;
using System.Collections.Generic;
using System.Linq;
using Team4.GroupProject.BusinessObjects.Models;

namespace Team4.GroupProject.Repositories
{
    public class CustomerRepository : PetShoppContext, IRepository<Customer>
    {
        private static CustomerRepository _instance;

        private CustomerRepository()
        {
        }

        public static CustomerRepository GetInstance()
        {
            if (_instance == null) _instance = new CustomerRepository();
            return _instance;
        }

        public bool Add(Customer entity)
        {
            Customers.Add(entity);
            SaveChanges();
            return true;
        }

        public IEnumerable<Customer> Get(Func<Customer, bool> func) => this.Customers.ToList().Where(c => func(c));

        public Customer GetOne(Func<Customer, bool> func) => this.Customers.ToList().FirstOrDefault(c => func(c));

        public bool Remove(Customer entity)
        {
            Customers.Remove(entity);
            SaveChanges();
            return true;
        }

        public bool Update(Customer entity)
        {
            this.Customers.Update(entity);
            SaveChanges();
            return true;
        }
    }
}
