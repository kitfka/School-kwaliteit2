using LiteDB;
using System.Linq.Expressions;
using Webshop.Model;
using Webshop.Model.Interfaces;

namespace Webshop.Api.Data
{
    public class LiteRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly LiteDBService _liteDBService;

        private ILiteCollection<T> Collection => _liteDBService.data.GetCollection<T>();

        public LiteRepository(LiteDBService service)
        {
            _liteDBService = service;
        }
        public Guid Add(T entity)
        {
            return Collection.Insert(entity).AsGuid;
        }

        public void Delete(T entity)
        {
            Collection.Delete(entity.Id);
        }

        public void Edit(T entity)
        {
            Collection.Update(entity);
        }

        public T GetById(Guid id)
        {
            T result = Collection.FindById(id);
            // TODO: This could fail and return null.
            return result;
        }

        public IEnumerable<T> List()
        {
            return Collection.FindAll();
        }

        public IEnumerable<T> List(int skip, int size)
        {
            return Collection.Query().Skip(skip).Limit(size).ToEnumerable();
        }

        public IEnumerable<T> List(Expression<Func<T, bool>> predicate)
        {
            return Collection.Find(predicate);
        }
    }
}
