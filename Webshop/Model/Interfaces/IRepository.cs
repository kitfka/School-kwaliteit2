using System.Linq.Expressions;

namespace Webshop.Model.Interfaces;

public interface IRepository<T> where T : BaseEntity
{
    T GetById(Guid id);
    IEnumerable<T> List();
    IEnumerable<T> List(int skip, int size);
    IEnumerable<T> List(Expression<Func<T, bool>> predicate);
    Guid Add(T entity);
    void Delete(T entity);
    void Edit(T entity);
}
