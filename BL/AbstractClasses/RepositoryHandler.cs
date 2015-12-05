using BL.Interfaces;
using DAL.AbstractRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.AbstractClasses
{
    public abstract class RepositoryHandler<T> : IRepositoryHandler<T>
        where T : class
    {
        protected DataRepository<T> _repository;

        public RepositoryHandler(DataRepository<T> repository)
        {
            _repository = repository;
        }

        public void Add(T item)
        {
            _repository.Add(item);
        }

        public void Update(T item)
        {
            _repository.Update(item);
        }

        public void Remove(T item)
        {
            _repository.Remove(item);
        }

        public IEnumerable<T> GetAll(Func<T, bool> predicate)
        {
            return _repository.GetAll(predicate);
        }

        public async Task<T> FindById(int id)
        {
           return await _repository.FindById(id);
        }
    }
}
