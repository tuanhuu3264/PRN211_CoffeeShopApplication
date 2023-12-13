using System;
using System.Collections.Generic;
using System.Linq;
using Team4.GroupProject.BusinessObjects.Models;

namespace Team4.GroupProject.Repositories
{
    public class EmployeeRepository : PetShoppContext, IRepository<Employee>
    {
        private static EmployeeRepository _instance;

        private EmployeeRepository()
        {
        }

        public static EmployeeRepository GetInstance()
        {
            if (_instance == null) _instance = new EmployeeRepository();
            return _instance;
        }

        public bool Add(Employee entity)
        {
            Employees.Add(entity);
            SaveChanges();
            return true;
        }

        public IEnumerable<Employee> Get(Func<Employee, bool> func) => this.Employees.ToList().Where(e => func(e)).ToList();

        public Employee GetOne(Func<Employee, bool> func) => this.Employees.ToList().FirstOrDefault(e => func(e));

        public bool Remove(Employee entity)
        {
            Employees.Remove(entity);
            SaveChanges();
            return true;
        }

        public bool Update(Employee entity)
        {
            this.Employees.Update(entity);
            SaveChanges();
            return true;
        }
    }
}
