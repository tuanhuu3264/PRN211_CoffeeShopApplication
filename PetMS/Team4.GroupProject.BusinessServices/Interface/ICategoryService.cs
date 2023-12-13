using System.Collections.Generic;
using Team4.GroupProject.BusinessObjects.Models;

namespace Team4.GroupProject.BusinessServices.Interface
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetAll();
        Category GetById(int id);
        IEnumerable<Category> Search(string keyword);
        bool Add(Category category);
        bool Update(Category category);
        bool Delete(int id);
    }
}
