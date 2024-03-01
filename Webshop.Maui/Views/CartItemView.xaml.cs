using Webshop.Model;

namespace Webshop.Maui.Views;

public partial class CartItemView : ContentView
{

    public static readonly BindableProperty CartItemProperty = BindableProperty.Create(nameof(CartItem), typeof(CartItem), typeof(CartItemView));

    public CartItem CartItem
    {
        get => (CartItem)GetValue(CartItemView.CartItemProperty);
        set => SetValue(CartItemView.CartItemProperty, value);
    }


    public static readonly BindableProperty ProductProperty = BindableProperty.Create(nameof(Product), typeof(Product), typeof(CartItemView));


    public string PriceText
    {
        get
        {
            if (CartItem == null || Product == null)
            {
                // THIS BREAKS when we do it the same way as Productview...
                return "N/A";
            }
            return $"€ {CartItem.Count * Product.Price}";
        }
    }
    public Product Product
    {
        get => (Product)GetValue(CartItemView.ProductProperty);
        set => SetValue(CartItemView.ProductProperty, value);
    }


    public CartItemView()
    {
        InitializeComponent();


        OnPropertyChanged(nameof(PriceText));
    }

    public CartItemView(CartItem cartItem, Product product)
    {
        CartItem = cartItem;
        Product = product;
        InitializeComponent();
    }
}