using Webshop.Model;
using Webshop.Service;

namespace Webshop.ViewModel;
public class EditProductViewModel : ViewModelBase
{
    readonly ProductService productService;

    private Product product;

    public Product Product
    {
        get { return product; }
        set { 
            product = value;
            NotifyPropertyChanged();
        }
    }


    public void SetProduct(Guid productId)
    {
        if (productId == Guid.Empty) { return; }

        var result = productService.GetProduct(productId);
        Product = result;
    }

    public void SaveProduct()
    {
        if (Product.Id == Guid.Empty) 
        { 
            productService.Create(Product);
            return; 
        }

        productService.Update(Product);
    }


    public EditProductViewModel(ProductService productService) 
    {
        this.productService = productService;
        product = new();
    }


}
