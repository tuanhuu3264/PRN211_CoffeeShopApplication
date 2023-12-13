using System;
using System.Collections.Generic;
using System.Linq;
using Team4.GroupProject.BusinessObjects.Models;

namespace Team4.GroupProject.Repositories
{
    public class BillDetailRepository : PetShoppContext, IRepository<BillDetail>
    {
        private static BillDetailRepository _instance;

        private BillDetailRepository()
        {
        }

        public static BillDetailRepository GetInstance()
        {
            if (_instance == null) _instance = new BillDetailRepository();
            return _instance;
        }

        public bool Add(BillDetail entity)
        {
            BillDetails.Add(entity);
            SaveChanges();
            return true;
        }
        public bool AddRange(List<BillDetail> entity)
        {
            BillDetails.AddRange(entity);
            SaveChanges();
            return true;
        }

        public IEnumerable<BillDetail> Get(Func<BillDetail, bool> func) => this.BillDetails.ToList().Where(bd => func(bd)).ToList();

        public BillDetail GetOne(Func<BillDetail, bool> func) => this.BillDetails.ToList().FirstOrDefault(bd => func(bd));

        public bool Remove(BillDetail entity)
        {
            BillDetails.Remove(entity);
            SaveChanges();
            return true;
        }

        public bool Update(BillDetail entity)
        {
            this.BillDetails.Update(entity);
            SaveChanges();
            return true;
        }

    }
}
