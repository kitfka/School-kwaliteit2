using Webshop.Data;
using Webshop.Model;

namespace Webshop.ViewModel;

public class UserViewModel : ViewModelBase
{
    private bool loggedIn;

    public bool LoggedIn
    {
        get { return loggedIn; }
        set
        {
            loggedIn = value;
            NotifyPropertyChanged(nameof(NotLoggedIn));
            NotifyPropertyChanged();
        }
    }

    public bool NotLoggedIn
    {
        get { return !loggedIn; }
        set
        {
            loggedIn = !value;
            NotifyPropertyChanged(nameof(LoggedIn));
            NotifyPropertyChanged();
        }
    }

    private string username = "";

    public string UserName
    {
        get { return username; }
        set
        {
            username = value;
            NotifyPropertyChanged();
        }
    }

    private string password = "";

    public string Password
    {
        get { return password; }
        set
        {
            password = value;
            NotifyPropertyChanged();
        }
    }

    private bool showRegister;

    public bool ShowRegister
    {
        get { return showRegister; }
        set
        {
            showRegister = value;
            NotifyPropertyChanged();
        }
    }




    public LoginUser GetLoginUser()
    {
        return new LoginUser()
        {
            Email = username,
            Password = password,
        };
    }

    public RegisterUser GetRegisterUser()
    {
        return new RegisterUser()
        {
            Email = username,
            Password = password,
            Name = username,
        };
    }
}
