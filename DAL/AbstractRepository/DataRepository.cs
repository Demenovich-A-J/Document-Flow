using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using DAL.Interfaces;
using Database = EntityModels.Entities;

namespace DAL.AbstractRepository
{
    public abstract class DataRepository<T, K> : IDataRepository<T>
        where T : class
        where K : class
    {
        public virtual void Add(T item)
        {
            using (var context = new Database())
            {
                context.Entry(ConvertToEntity(item)).State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public virtual void Update(T item)
        {
            using (var context = new Database())
            {
                context.Entry(ConvertToEntity(item)).State = EntityState.Modified;
                context.SaveChanges();
            }
        }

        public virtual void Remove(T item)
        {
            using (var context = new Database())
            {
                context.Entry(ConvertToEntity(item)).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public virtual IEnumerable<T> GetAll()
        {
            IEnumerable<T> list;
            using (var context = new Database())
            {
                list = context
                    .Set<K>()
                    .AsNoTracking()
                    .ToList()
                    .Select(x => ConvertToModel(x))
                    .ToList();
            }
            return list ?? new List<T>();
        }

        public T FindById(int id)
        {
            T modelItem;

            using (var contex = new Database())
            {
                var item = contex.Set<K>().Find(id);
                modelItem = ConvertToModel(item);
            }

            return modelItem;
        }

        protected abstract T ConvertToModel(K item);
        protected abstract K ConvertToEntity(T item);
    }
}