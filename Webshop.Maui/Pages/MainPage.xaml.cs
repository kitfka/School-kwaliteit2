using Webshop.ViewModel;

namespace Webshop.Maui.Pages;

public partial class MainPage : ContentPage
{
    MainViewModel viewModel => BindingContext as MainViewModel;

    public MainPage(MainViewModel viewModel)
    {
        BindingContext = viewModel;
        InitializeComponent();



    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        viewModel.IncreaseCounter();


        SemanticScreenReader.Announce(CounterBtn.Text);
    }

    private void TestButton_Clicked(object sender, EventArgs e)
    {
        TestImage.Source = new UriImageSource
        {
            //Uri = new Uri("https://aka.ms/campus.jpg"),
            Uri = new Uri("https://localhost:5014/Admin/ASP.NET-Core-GitServer/raw/main/GitServer/wwwroot/img/folder-16.png"),
            CacheValidity = new TimeSpan(10, 0, 0, 0)
        };
    }
}

