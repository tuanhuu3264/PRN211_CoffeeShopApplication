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
    public class CategoryService : ICategoryService
    {
        public bool Add(Category category)
        {
            var existingCategory = CategoryRepository.GetInstance().GetOne((c) => c.Name.ToLower().Trim() == category.Name.ToLower().Trim());
            if (existingCategory != null) { return false; }
            category.Name = category.Name.Trim();
            if (CategoryRepository.GetInstance().Add(category)) { return true; }
            return false;
        }

        public bool Delete(int id)
        {
            var existingCategory = CategoryRepository.GetInstance().GetOne((c) => c.Id == id);
            if (existingCategory == null)
            {
                return false;
            }
            if (CategoryRepository.GetInstance().Remove(existingCategory)) { return true; }
            return false;
        }

        public IEnumerable<Category> GetAll() => CategoryRepository.GetInstance().Get((c) => true);

        public Category GetById(int id) => CategoryRepository.GetInstance().GetOne((c) => c.Id == id);

        public IEnumerable<Category> Search(string keyword)
        {
            throw new NotImplementedException();
        }

        public bool Update(Category category)
        {
            var existingCategory = CategoryRepository.GetInstance().GetOne((c) => c.Id == category.Id);
            if (existingCategory == null)
            {
                return false;
            }
            existingCategory.Name = category.Name.Trim();
            if (CategoryRepository.GetInstance().Update(existingCategory)) return true;
            return false;
        }
    }
}
