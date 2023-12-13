using System.Collections.Generic;
using Team4.GroupProject.BusinessObjects.Models;

namespace Team4.GroupProject.BusinessServices.Interface
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetAll();
        Employee GetById(int id);
        IEnumerable<Employee> Search(string keyword);
        bool Add(Employee employee);
        bool Update(Employee employee);
        bool Delete(int id);
        Employee GetByPhone(string phone);
        bool CheckLogin(string phone, string password);
    }
}
