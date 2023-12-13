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
    public class BillDetailService : IBillDetailService
    {
        public BillDetailService() { }
        public bool Add(BillDetail category)
        {
            var existingBillDetail = BillDetailRepository.GetInstance().GetOne(bd => bd.ProductId == category.ProductId && bd.BillId == category.BillId);
            if (existingBillDetail != null) { return false; }
            if (BillDetailRepository.GetInstance().Add(category)) return true;
            return false;
        }

        public bool AddRange(List<BillDetail> list)
        {
            foreach (var item in list)
            {
                var existingBillDetail = BillDetailRepository.GetInstance().GetOne(bd => bd.ProductId == item.ProductId && bd.BillId == item.BillId);
                if (existingBillDetail != null) { return false; }
            }
            if (BillDetailRepository.GetInstance().AddRange(list)) return true;
            return false;
        }

        public bool Delete(int orderId)
        {
            var listBillDetails = BillDetailRepository.GetInstance().Get(bd => bd.BillId == orderId);
            if (listBillDetails == null) { return false; }
            foreach (var item in listBillDetails)
            {
                BillDetailRepository.GetInstance().Remove(item);
            }
            return true;
        }

        public IEnumerable<BillDetail> GetAll() => BillDetailRepository.GetInstance().Get(bd => true);

        public IEnumerable<BillDetail> GetByBillId(int orderId) => BillDetailRepository.GetInstance().Get(bd => bd.BillId == orderId);

        public IEnumerable<BillDetail> GetByProductId(int productId) => BillDetailRepository.GetInstance().Get(bd => bd.ProductId == productId);

        public bool Update(BillDetail category)
        {
            var existingBillDetail = BillDetailRepository.GetInstance().GetOne(bd => bd.ProductId == category.ProductId && bd.BillId == category.BillId);
            if (existingBillDetail == null) { return false; }
            existingBillDetail.Price = category.Price;
            existingBillDetail.Quantity = category.Quantity;
            existingBillDetail.ProductId = category.ProductId;
            existingBillDetail.Total = existingBillDetail.Price * existingBillDetail.Quantity;
            if (BillDetailRepository.GetInstance().Update(category)) return true;
            return false;
        }

    }
}
