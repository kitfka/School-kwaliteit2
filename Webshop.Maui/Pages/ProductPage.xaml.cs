
using Webshop.Service;
using Webshop.ViewModel;


namespace Webshop.Maui.Pages;

[QueryProperty(nameof(Guid), "id")]
public partial class ProductPage : ContentPage
{

    ProductViewModel viewModel => BindingContext as ProductViewModel;
    private readonly ProductService productService;
    private readonly CartService cartService;


    public Guid Guid
    {
        set
        {
            viewModel.Product = productService.GetProduct(value);
            ReloadVariants(value);
        }
    }

    public ProductPage(ProductViewModel productViewModel, ProductService productService, CartService cartService)
    {
        this.productService = productService;
        this.cartService = cartService;
        BindingContext = productViewModel;
        InitializeComponent();
    }

    private void ReloadVariants(Guid value)
    {
        VariantList.Clear();
        var ver = productService.GetVariantsOfProduct(value);

        foreach (var item in ver)
        {
            ImageButton imageButton = new()
            {
                Source = item.ImageSource,
            };
            VariantList.Add(imageButton);
        }
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        var s = (Shell)App.Current.MainPage;

        await s.GoToAsync($".."); // yess this is wierd, the method is not a task ending with async. Try it and understand why this is not the case for now.
    }

    private void CartButton_Clicked(object sender, EventArgs e)
    {
        cartService.AddToCart(viewModel.Product, 1);
    }
}