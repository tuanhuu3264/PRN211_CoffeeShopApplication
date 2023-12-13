using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Team4.GroupProject.BusinessObjects.Models;

namespace Team4.GroupProject.Repositories
{
    public class ProductRepository : PetShoppContext, IRepository<Product>
    {
        private static ProductRepository _instance;
        private ProductRepository()
        {
        }

        public static ProductRepository GetInstance()
        {
            if (_instance == null) _instance = new ProductRepository();
            return _instance;
        }

        public bool Add(Product entity)
        {
            Products.AddAsync(entity);
            SaveChangesAsync();
            return true;
        }

        public IEnumerable<Product> Get(Func<Product, bool> func) => this.Products.Include(p => p.Category).ToList().Where(p => func(p)).ToList();

        public Product GetOne(Func<Product, bool> func) => this.Products.Include(p=>p.Category).ToList().FirstOrDefault(p => func(p));

        public bool Remove(Product entity)
        {
            Products.Remove(entity);
            SaveChangesAsync();
            return true;
        }

        public bool Update(Product entity)
        {
            this.Products.Update(entity);
            SaveChangesAsync();
            return true;
        }
    }
}
