using System.Collections.Generic;

namespace BL.Interfaces
{
    public interface IRepositoryHandler<T>
        where T : class
    {
        void Add(T item);
        void Update(T item);
        void Remove(T item);
        T FindById(int id);

        IEnumerable<T> GetAll();
    }
}