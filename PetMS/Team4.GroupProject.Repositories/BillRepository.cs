using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team4.GroupProject.BusinessObjects.Models;

namespace Team4.GroupProject.Repositories
{
    public class BillRepository : PetShoppContext, IRepository<Bill>
    {
        private static BillRepository _instance; 
        private BillRepository() { 
        }
        public static BillRepository GetInstance()
        {
            if(_instance== null) _instance = new BillRepository();
            return _instance;
        }
        public bool Add(Bill entity)
        {   
            this.Bills.Add(entity);
            this.SaveChanges();
            return true;
        }

        public IEnumerable<Bill> Get(Func<Bill, bool> func) => this.Bills.Include(b=>b.Customer).Include(b=>b.Employee).ToList().Where(b=>func(b)).ToList();

        public Bill GetOne(Func<Bill, bool> func) => this.Bills.Include(b => b.Customer).Include(b => b.Employee).ToList().FirstOrDefault(b => func(b));

        public bool Remove(Bill entity)
        {
            this.Bills.Remove(entity);
            this.SaveChanges();
            return true;
        }

        public bool Update(Bill entity)
        {
            this.Bills.Update(entity); 
            this.SaveChanges(); 
            return true;
        }
    }
}
