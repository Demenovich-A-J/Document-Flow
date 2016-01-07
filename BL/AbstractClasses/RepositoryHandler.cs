using System.Collections.Generic;
using System.Linq;
using BL.Interfaces;
using DAL.Interfaces;

namespace BL.AbstractClasses
{
    public abstract class RepositoryHandler<T, K> : IRepositoryHandler<T>
        where T : class
        where K : class
    {
        protected readonly IDataRepository<K> _repository;

        public RepositoryHandler(IDataRepository<K> repository)
        {
            _repository = repository;
        }

        public void Add(T item)
        {
            _repository.Add(ConvertToDalModel(item));
        }

        public void Update(T item)
        {
            _repository.Update(ConvertToDalModel(item));
        }

        public void Remove(T item)
        {
            _repository.Remove(ConvertToDalModel(item));
        }

        public IEnumerable<T> GetAll()
        {
            return _repository.GetAll().Select(ConvertToModel).ToList();
        }

        public T FindById(int id)
        {
            var item = _repository.FindById(id);
            return ConvertToModel(item);
        }

        protected abstract T ConvertToModel(K item);
        protected abstract K ConvertToDalModel(T item);
    }
}