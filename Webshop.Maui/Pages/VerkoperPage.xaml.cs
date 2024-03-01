using Webshop.Maui.Views;
using Webshop.Model;
using Webshop.Service;

namespace Webshop.Maui.Pages;

public partial class VerkoperPage : ContentPage
{
	private readonly ProductService productService;
	public VerkoperPage(ProductService productService)
	{
		this.productService = productService;
		InitializeComponent();
		SpikeLoad();
	}


	public void SpikeLoad()
	{
		ProductStack.Children.Clear();

        var r = productService.GetMyProducts();
		
        foreach (var item in r)
        {
            ProductView productView = new()
            {
                CardTitle = item.Name,
                ProductId = item.Id,
                Description = item.Description,
                OnlyEmit = true,
            };

            productView.Clicked += ProductView_Clicked;

            ProductStack.Children.Add(productView);
        }
    }

    private void ProductView_Clicked(object sender, EventArgs e)
    {
        if (sender is ProductView product)
        {
            var s = (Shell)App.Current.MainPage;

            var navigationParameter = new Dictionary<string, object>
            {
                { "id", product.ProductId }
            };

            s.GoToAsync($"EditProductPage", navigationParameter);
        }
    }

    private void CreateNewProductButton_Clicked(object sender, EventArgs e)
    {
        var s = (AppShell)App.Current.MainPage;
        s.GoToAsync($"EditProductPage");
        //s.VerkoperShellContent.IsVisible = true;
        
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        SpikeLoad();
    }
}