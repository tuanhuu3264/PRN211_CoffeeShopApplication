using System.Collections.Generic;
using Team4.GroupProject.BusinessObjects.Models;

namespace Team4.GroupProject.BusinessServices.Interface
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product GetById(int id);
        IEnumerable<Product> Search(string keyword, int categoryId);
        bool Add(Product product);
        bool Update(Product product);
        bool Delete(int id);
        bool UpdateNumberDecrease(int id, int quanity);
        bool UpdateNumberIncrease(int id, int quanity);
        IEnumerable<Product> GetByCategoryId(int id);
    }
}