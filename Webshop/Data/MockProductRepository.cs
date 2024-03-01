using System.Linq.Expressions;
using Webshop.Model;
using Webshop.Model.Interfaces;

namespace Webshop.Data;

public class MockProductRepository : IRepository<Product>
{
    private readonly IList<Product> _products = MockData.Products;


    public Guid Add(Product entity)
    {
        entity.Id = Guid.NewGuid();
        _products.Add(entity);
        return entity.Id;
    }

    public void Delete(Product entity)
    {
        _products.Remove(entity);
    }

    public void Edit(Product entity)
    {
        Delete(GetById(entity.Id));
        Add(entity);
    }

    public Product GetById(Guid id)
    {
        foreach (var item in _products)
        {
            if (item.Id == id)
            {
                return item;
            }
        }
        return new Product();
    }

    public IEnumerable<Product> List()
    {
        return _products;
    }

    public IEnumerable<Product> List(Expression<Func<Product, bool>> predicate)
    {
        return _products.Where(predicate.Compile());
    }

    public IEnumerable<Product> List(int skip, int size)
    {
        return _products.AsQueryable().Skip(skip).Take(size);
    }
}
