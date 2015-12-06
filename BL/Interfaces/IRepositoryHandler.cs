using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BL.Interfaces
{
    public interface IRepositoryHandler<T>
        where T : class
    {
        void Add(T item);
        void Update(T item);
        void Remove(T item);
        Task<T> FindById(int id);

        IEnumerable<T> GetAll(Func<T, bool> predicate);
    }
}