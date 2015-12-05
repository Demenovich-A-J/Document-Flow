using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DAL.Interfaces;
using Database = EntityModels.Database;

namespace DAL.AbstractRepository
{
    public abstract class DataRepository<T> : IDataRepository<T>
        where T : class
    {
        public virtual void Add(T item)
        {
            using (var context = new Database())
            {
                context.Entry(item).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public virtual void Update(T item)
        {
            using (var context = new Database())
            {
                context.Entry(item).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public virtual void Remove(T item)
        {
            using (var context = new Database())
            {
                context.Entry(item).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public virtual IEnumerable<T> GetAll(Func<T, bool> predicate)
        {
            IEnumerable<T> list;
            using (var context = new Database())
            {
                list = context
                    .Set<T>()
                    .AsNoTracking()
                    .Where(predicate)
                    .ToList();
            }
            return list ?? new List<T>();
        }

        public abstract Task<T> FindById(int id);
    }
}