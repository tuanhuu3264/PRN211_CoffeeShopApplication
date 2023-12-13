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
    public class ProductService : IProductService
    {
        public ProductService() { }
        public bool Add(Product product)
        {
            if (ProductRepository.GetInstance().Add(product)) return true;
            return false;
        }

        public bool Delete(int id)
        {
            var existingProduct = ProductRepository.GetInstance().GetOne((p) => p.Id == id);
            if (existingProduct == null)
            {
                return false;
            }
            if (ProductRepository.GetInstance().Remove(existingProduct)) return true;
            return false;

        }

        public IEnumerable<Product> GetAll() =>ProductRepository.GetInstance().Get((p)=>true);

        public IEnumerable<Product> GetByCategoryId(int id) => ProductRepository.GetInstance().Get(m => m.CategoryId == id);
        public Product GetById(int id) => ProductRepository.GetInstance().GetOne((p)=>p.Id==id);

        public IEnumerable<Product> Search(string keyword, int categoryId)
        {
            IEnumerable<Product> products = new List<Product>();
            if (string.IsNullOrEmpty(keyword.Trim())) products = ProductRepository.GetInstance().Get(p => true);
            else products = ProductRepository.GetInstance().Get(p=>p.Name.ToLower().Contains(keyword.ToLower().Trim())||p.Description.ToLower().Contains(keyword.ToLower().Trim()));
            if(categoryId!=0) products = products.Where(p => p.CategoryId == categoryId);
            return products;
        }
        

        public bool Update(Product product)
        {
            var existingProduct = ProductRepository.GetInstance().GetOne((p) => p.Id == product.Id);
            if (existingProduct == null)
            {
                return false;
            }
            existingProduct.Price = product.Price;
            existingProduct.Description = product.Description;
            existingProduct.Name = product.Name;    
            existingProduct.CategoryId = product.CategoryId;
            existingProduct.Quantity = product.Quantity;    
            if (ProductRepository.GetInstance().Update(existingProduct)) return true;
            return false;
        }

        public bool UpdateNumberDecrease(int id, int quanity)
        {
            var existingProduct = ProductRepository.GetInstance().GetOne((p) => p.Id == id);
            if (existingProduct == null)
            {
                return false;
            }
            if (existingProduct.Quantity < quanity) return false;
            existingProduct.Quantity -= quanity;
            if (ProductRepository.GetInstance().Update(existingProduct)) return true;
            return false;
        }

        public bool UpdateNumberIncrease(int id, int quanity)
        {
            var existingProduct = ProductRepository.GetInstance().GetOne((p) => p.Id == id);
            if (existingProduct == null)
            {
                return false;
            }
            existingProduct.Quantity += quanity;
            if (ProductRepository.GetInstance().Update(existingProduct)) return true;
            return false;
        }
    }
}
