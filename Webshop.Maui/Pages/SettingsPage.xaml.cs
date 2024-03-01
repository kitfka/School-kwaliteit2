using Webshop.Service;
using Webshop.ViewModel;

namespace Webshop.Maui.Pages;

public partial class SettingsPage : ContentPage
{
    ConfigViewModel viewModel => BindingContext as ConfigViewModel;


    public SettingsPage(ConfigViewModel configViewModel)
    {
        BindingContext = configViewModel;
        InitializeComponent();
    }

    private void Picker_SelectedIndexChanged(object sender, EventArgs e)
    {
        AppTheme theme = (AppTheme)(int)viewModel.UITheme;
        Application.Current.UserAppTheme = theme;
    }
}