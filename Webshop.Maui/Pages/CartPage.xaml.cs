using Webshop.Maui.Model;
using Webshop.Maui.Views;
using Webshop.Model;
using Webshop.Service;
using Webshop.ViewModel;


namespace Webshop.Maui.Pages;

public partial class CartPage : ContentPage
{
    private readonly CartService cartService;
    private readonly ProductService productService;

    CartViewModel viewModel => BindingContext as CartViewModel;

    public CartPage(CartService cartService, ProductService productService, CartViewModel cartViewModel)
    {
        BindingContext = cartViewModel;
        this.cartService = cartService;
        this.productService = productService;
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        ReloadCart();
    }

    private void ReloadCart()
    {
        decimal price = 0;
        CartList.Clear();
        foreach (CartItem item in cartService.GetCart())
        {
            price += item.Count * productService.GetProduct(item.ProductId).Price;
            CartItemView newCartItemView = new(item, productService.GetProduct(item.ProductId));

            CartList.Add(newCartItemView);
        }

        viewModel.TotalPrice = price;
    }

    private void OrderButton_Clicked(object sender, EventArgs e)
    {
        cartService.Order();
        this.GoToAsync("//OrderPage");
    }
}