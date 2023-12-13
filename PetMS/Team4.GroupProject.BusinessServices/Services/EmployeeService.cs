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
    public class EmployeeService : IEmployeeService
    {
        public EmployeeService() { }
        public bool Add(Employee employee)
        {
            var existingEmployee = EmployeeRepository.GetInstance().GetOne(e => e.Phone == employee.Phone);
            if (existingEmployee != null) { return false; }
            if (EmployeeRepository.GetInstance().Add(employee)) return true;
            return false;
        }

        public bool CheckLogin(string phone, string password) => EmployeeRepository.GetInstance().GetOne(e => e.Phone.Equals(phone.Trim()) && e.Password.Equals(password)) != null;

        public bool Delete(int id)
        {
            var existingEmployee = EmployeeRepository.GetInstance().GetOne(e => e.Id == id);
            if (existingEmployee == null) { return false; }
            if (EmployeeRepository.GetInstance().Remove(existingEmployee)) return true;
            return false;
        }

        public IEnumerable<Employee> GetAll() => EmployeeRepository.GetInstance().Get(e => true);

        public Employee GetById(int id) => EmployeeRepository.GetInstance().GetOne(e => e.Id == id);

        public Employee GetByPhone(string phone) => EmployeeRepository.GetInstance().GetOne(e=>e.Phone==phone);
        public IEnumerable<Employee> Search(string keyword) => EmployeeRepository.GetInstance().Get(e => e.Name.ToLower().Contains(keyword.ToLower().Trim())
               || e.Address.ToLower().Contains(keyword.ToLower().Trim()) || e.Phone.Contains(keyword.Trim()));

        public bool Update(Employee employee)
        {
            var existingEmployee = EmployeeRepository.GetInstance().GetOne(e => e.Id == employee.Id);
            if (existingEmployee == null) { return false; }
            var conditionPhone = EmployeeRepository.GetInstance().GetOne(e => e.Phone == employee.Phone && e.Id != employee.Id);
            if (conditionPhone != null) { return false; }
            existingEmployee.Name = employee.Name;
            existingEmployee.Phone = employee.Phone;
            existingEmployee.Address = employee.Address;
            existingEmployee.Password = employee.Password;
            if (EmployeeRepository.GetInstance().Update(existingEmployee)) { return true; }
            return false;
        }
    }
}
