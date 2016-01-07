using System.Collections.Generic;

namespace DAL.Interfaces
{
    public interface IDataRepository<T>
        where T : class
    {
        void Add(T item);
        void Update(T item);
        void Remove(T item);
        IEnumerable<T> GetAll();
        T FindById(int id);
    }
}