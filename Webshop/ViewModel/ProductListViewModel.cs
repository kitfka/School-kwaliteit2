using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Webshop.Model;
using Webshop.Service;

namespace Webshop.ViewModel;

public class ProductListViewModel : ViewModelBase
{
    private readonly ProductService productService;

    private string search = "";

    public string Search
    {
        get { return search; }
        set { 
            search = value;
            NotifyPropertyChanged();
            SearchProducts(value);
        }
    }





    public IList<Product> Products { get; set; }

    // https://stackoverflow.com/questions/50928891/how-to-implement-pagination-in-asp-net-core-razor-pages

    private int page;
    public int Page { 
        get
        {
            return page;
        }
        set
        {
            page = value;
            NotifyPropertyChanged();
        }
    }

    public int Skip => (Page - 1) * Size;

    public int Size { get; set; } = 10;

    public ProductListViewModel(ProductService productService)
    {
        this.productService = productService;

        Products = [];

        SearchProducts("");
    }

    
    private void SearchProducts(string value)
    {
        var searchProducts = productService.GetSearchResults(value);

        Products.Clear();

        foreach (var item in searchProducts)
        {
            Products.Add(item);
        }
        NotifyPropertyChanged(nameof(Products));
    }
}
