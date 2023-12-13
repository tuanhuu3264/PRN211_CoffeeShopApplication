using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Team4.GroupProject.BusinessObjects.Models;

namespace Team4.GroupProject.Repositories
{
    public class CategoryRepository : PetShoppContext, IRepository<Category>
    {
        private static CategoryRepository _instance;
        private CategoryRepository()
        {

        }
        public static CategoryRepository GetInstance()
        {
            if (_instance == null) _instance = new CategoryRepository(); 
            return _instance;
        }
        public bool Add(Category entity)
        {
            Categories.Add(entity);
            SaveChanges();
            return true;
        }

        public IEnumerable<Category> Get(Func<Category, bool> func) => this.Categories.ToList().Where((c)=>func(c)).ToList();

        public Category GetOne(Func<Category, bool> func) => this.Categories.ToList().FirstOrDefault((c) => func(c));

        public bool Remove(Category entity)
        {
            Categories.Remove(entity); 
            SaveChanges();
            return true;
        }

        public bool Update(Category entity)
        {
            this.Categories.Update(entity);
            SaveChanges(); 
            return true;
        }
    }
}
