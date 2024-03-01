using Webshop.Service;
using Webshop.Model;
using Webshop.ViewModel;
using Newtonsoft.Json.Linq;
using Webshop.Maui.Views;
using System.Linq;

namespace Webshop.Maui.Pages;

public partial class ProductListPage : ContentPage
{
    ProductListViewModel viewModel => BindingContext as ProductListViewModel;

    public ProductListPage(ProductListViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();
        ReloadProductList();
        viewModel.PropertyChanged += ViewModel_PropertyChanged;
    }

    private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(viewModel.Products))
        {
            ReloadProductList();
        }
    }


    private void ReloadProductList()
    {
        List<IView> toRemove = [];
        List<Guid> toAdd = [];

        toAdd.AddRange(viewModel.Products.Select(item => item.Id));

        // check what is allready in product list, and remove the rest
        foreach (var productView in ProductList.Children.Cast<ProductView>())
        {
            if (viewModel.Products.Any(x => x.Id == productView.ProductId))
            {
                toAdd.Remove(productView.ProductId);
            }
            else
            {
                toRemove.Add(productView);
            }
        }

        // remove items not in search
        foreach (var removeId in toRemove)
        {
            ProductList.Remove(removeId);
        }

        foreach (var product in viewModel.Products.Where(product => toAdd.Exists(x => x == product.Id)))
        {
            ProductView newProductView = new()
            {
                CardTitle = product.Name,
                Description = product.Description,
                Price = product.Price,
                ProductId = product.Id,
                ImageSource = product.ImageSource,
            };
            ProductList.Children.Add(newProductView);
        }

        // todo, order children?
        // Well that is broken in Windows...
    }
}