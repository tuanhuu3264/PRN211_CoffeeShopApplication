using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team4.GroupProject.Repositories
{
    public interface IRepository<T>
    {
        public T GetOne(Func<T, bool> func);
        public IEnumerable<T> Get(Func<T, bool> func);
        public bool Add (T entity);
        public bool Remove (T entity);
        public bool Update (T entity);
    }
}
