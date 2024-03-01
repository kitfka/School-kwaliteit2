using Webshop.Model;

namespace Webshop.Service;

public class ProductService
{
    private readonly IList<ProductVariant> variants = MockData.ProductVariants;

    private readonly ApiService ApiService;

    public ProductService(ApiService ApiService)
    {
        this.ApiService = ApiService;
    }

    public IList<Product> GetProducts()
    {
        return ApiService.GetAndDeserializeAsync<List<Product>>("api/Product").Result;
    }

    public IList<Product> GetSearchResults(string search)
    {
        if (string.IsNullOrEmpty(search))
        {
            return GetProducts();
        }
        return ApiService.GetAndDeserializeAsync<List<Product>>($"api/Product/search/{search}").Result;
    }

    public Product GetProduct(Guid Id)
    {
        return ApiService.GetAndDeserializeAsync<Product>($"api/Product/{Id}").Result;
    }

    public ProductVariant GetProductVariant(Guid Id)
    {
        foreach (var item in variants)
        {
            if (item.Id == Id) return item;
        }
        return variants[0];
    }

    public Product GetProductOfVariant(Guid Id)
    {
        ProductVariant product = GetProductVariant(Id);
        return GetProduct(product.ProductId);
    }

    public IList<ProductVariant> GetVariantsOfProduct(Guid ProductId)
    {
        return ApiService.GetAndDeserializeAsync<List<ProductVariant>>($"api/Product/{ProductId}/Variants").Result;
    }

    #region Verkoper
    
    public Guid Create(Product newProduct)
    {
        // TODO, auth check.
        var result = ApiService.PostAsync("api/Product", newProduct).Result;
        var raw = result.Content.ReadAsStringAsync().Result;
        return Guid.Parse(raw.Trim('"')); // this will not work. you
    }

    public IList<Product> GetMyProducts()
    {
        return ApiService.GetAndDeserializeAsync<List<Product>>("api/Product/MyProducts").Result;
    }

    public void Update(Product product)
    {
        if (product.Id != Guid.Empty)
        {
            _ = ApiService.PatchAsync($"api/Product/{product.Id}", product);
        }
    }

    #endregion
}
