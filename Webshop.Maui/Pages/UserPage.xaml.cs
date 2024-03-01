using Webshop.Service;
using Webshop.ViewModel;

namespace Webshop.Maui.Pages;

public partial class UserPage : ContentPage
{
    private readonly UserService _userService;

    UserViewModel viewModel => BindingContext as UserViewModel;

    public UserPage(UserService userService, UserViewModel userViewModel)
    {
        BindingContext = userViewModel;
        _userService = userService;
        InitializeComponent();

#if DEBUG
        // To make development a bit faster
        viewModel.UserName = "Admin@example.com";
#endif
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {

        await _userService.LoginAsync(viewModel.GetLoginUser());

        viewModel.LoggedIn = _userService.IsLoggedIn();
        AppShell.Instance.IsLoggedIn = viewModel.LoggedIn;
    }

    private async void RegisterButton_Clicked(object sender, EventArgs e)
    {
        await _userService.RegisterAsync(viewModel.GetRegisterUser());

        viewModel.LoggedIn = _userService.IsLoggedIn();
        AppShell.Instance.IsLoggedIn = viewModel.LoggedIn;
    }

    private void LogoutButton_Clicked(object sender, EventArgs e)
    {
        _userService.Logout();
        viewModel.LoggedIn = _userService.IsLoggedIn();
        AppShell.Instance.IsLoggedIn = viewModel.LoggedIn;
    }
}