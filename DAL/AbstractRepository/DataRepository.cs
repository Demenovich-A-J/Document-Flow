using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.AbstractRepository
{
    public abstract class DataRepository<T> : IDataRepository<T>
        where T : class
    {
        public virtual void Add(T item)
        {
            using (var context = new EntityModels.Database())
            {
                context.Entry(item).State = System.Data.Entity.EntityState.Added;
                context.SaveChanges();
            }
        }

        public virtual void Update(T item)
        {
            using (var context = new EntityModels.Database())
            {
                context.Entry(item).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }

        public virtual void Remove(T item)
        {
            using (var context = new EntityModels.Database())
            {
                context.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public abstract Task<T> FindById(int id);

        public virtual IEnumerable<T> GetAll(Func<T, bool> predicate)
        {
            IEnumerable<T> list;
            using (var context = new EntityModels.Database())
            {
                list = context
                    .Set<T>()
                    .AsNoTracking()
                    .Where(predicate)
                    .ToList();
            }
            return list ?? new List<T>();
        }
    }
}
