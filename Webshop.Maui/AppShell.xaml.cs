using Webshop.Maui.Pages;
using Webshop.Service;

namespace Webshop.Maui;

public partial class AppShell : Shell
{
    public static AppShell Instance => (AppShell)App.Current.MainPage;

    public bool IsLoggedIn
    {
        get => VerkoperShellContent.IsVisible;
        set => VerkoperShellContent.IsVisible = value;
    }

    public AppShell()
    {
        RegisterRoutes();
        InitializeComponent();
    }

    private void RegisterRoutes()
    {
        Routing.RegisterRoute(nameof(ProductPage), typeof(ProductPage));
        Routing.RegisterRoute(nameof(EditProductPage), typeof(EditProductPage));
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        Instance.GoToAsync("//UserPage");
    }

    private void Button2_Clicked(object sender, EventArgs e)
    {
        Instance.GoToAsync("//CartPage");
    }

    private void Button3_Clicked(object sender, EventArgs e)
    {
        Instance.GoToAsync("//ProductListPage");
    }
}