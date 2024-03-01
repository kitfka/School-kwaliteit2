using System.Text;
using Webshop.Service;
using Webshop.Model;

namespace Webshop.Maui.Pages;

public partial class OrderPage : ContentPage
{
    private readonly CartService cartService;
    private readonly ProductService productService;
    public OrderPage(CartService cartService, ProductService productService)
    {
        this.productService = productService;
        this.cartService = cartService;
        InitializeComponent();
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();

        ReloadOrders();
    }

    private void ReloadOrders()
    {
        OrderList.Clear();
        foreach (var item in cartService.GetUserOrder())
        {
            StringBuilder sb = new();
            sb.AppendLine("Products:");
            // Small hack
            foreach (OrderItem orderItem in item.OrderItems)
            {
                var product = productService.GetProduct(orderItem.ProductId);
                sb.Append($"{orderItem.Count} X ");
                sb.AppendLine(product.Name);
            }

            sb.AppendLine($"Total: € {item.TotalPrice}");

            Label newLabel = new()
            {
                Text = sb.ToString(),
            };
            OrderList.Add(newLabel);
        }
    }
}