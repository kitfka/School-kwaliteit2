namespace Webshop.Maui.Pages;

public partial class SpikeWebPage : ContentPage
{
    public SpikeWebPage()
    {
        InitializeComponent();
        SpikeWebView.Source = "https://cith.nl";
    }

    private async void SpikeWebView_Navigated(object sender, WebNavigatedEventArgs e)
    {
        string x = await SpikeWebView.EvaluateJavaScriptAsync("getCookie(RequestVerificationToken)");
        if (x != null) { }
    }
}