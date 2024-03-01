namespace Webshop.Maui.Views;

public partial class ProductView : ContentView
{
    public static readonly BindableProperty CardTitleProperty = BindableProperty.Create(nameof(CardTitle), typeof(string), typeof(ProductView));

    public string CardTitle
    {
        get => (string)GetValue(ProductView.CardTitleProperty);
        set => SetValue(ProductView.CardTitleProperty, value);
    }


    public static readonly BindableProperty DescriptionProperty = BindableProperty.Create(nameof(Description), typeof(string), typeof(ProductView));

    public string Description
    {
        get => (string)GetValue(ProductView.DescriptionProperty);
        set => SetValue(ProductView.DescriptionProperty, value);
    }


    public static readonly BindableProperty PriceProperty = BindableProperty.Create(nameof(Price), typeof(Decimal), typeof(ProductView));

    public Decimal Price
    {
        get => (Decimal)GetValue(ProductView.PriceProperty);
        set => SetValue(ProductView.PriceProperty, value);
    }


    public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(nameof(ImageSource), typeof(string), typeof(ProductView), "sky.jpg");

    public string ImageSource
    {
        get => (string)GetValue(ProductView.ImageSourceProperty);
        set => SetValue(ProductView.ImageSourceProperty, value);
    }


    public static readonly BindableProperty ProductIdProperty = BindableProperty.Create(nameof(ProductId), typeof(Guid), typeof(ProductView), Guid.Empty);

    public Guid ProductId
    {
        get => (Guid)GetValue(ProductView.ProductIdProperty);
        set => SetValue(ProductView.ProductIdProperty, value);
    }



    public static readonly BindableProperty OnlyEmitProperty = BindableProperty.Create(nameof(OnlyEmit), typeof(bool), typeof(ProductView));

    public bool OnlyEmit
    {
        get => (bool)GetValue(ProductView.OnlyEmitProperty);
        set => SetValue(ProductView.OnlyEmitProperty, value);
    }

    public event EventHandler Clicked;

    public ProductView()
    {
        InitializeComponent();
        

    }

    private void InvokeClicked()
    {
        Clicked?.Invoke(this, EventArgs.Empty);
    }

    private async void BuyButton_Clicked(object sender, EventArgs e)
    {
        InvokeClicked();
        if (OnlyEmit) { return; }

        //await Shell.GoToAsync("//SettingsPage");

        //var s = (Shell)App.Current.MainPage;
        // https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/shell/navigation?view=net-maui-7.0
        var navigationParameter = new Dictionary<string, object>
        {
            { "id", ProductId }
        };
        //await s.GoToAsync($"ProductPage", navigationParameter);
        await AppShell.Instance.GoToAsync($"ProductPage", navigationParameter);
        // werkt maar is best raar...

        //var vm = new ViewModel.ProductViewModel() { Product = new Webshop.Model.Product() { Description="This is the description!", Name="Test product" } };
        //await s.Navigation.PushAsync(new ProductPage(vm));
    }

    private async void ProductImageButton_Clicked(object sender, EventArgs e)
    {
        InvokeClicked();
        if (OnlyEmit) { return; }

        // https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/shell/navigation?view=net-maui-7.0
        
        var navigationParameter = new Dictionary<string, object>
        {
            { "id", ProductId }
        };
        
        await AppShell.Instance.GoToAsync($"ProductPage", navigationParameter);
    }
}