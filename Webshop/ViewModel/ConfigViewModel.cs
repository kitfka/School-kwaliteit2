using Webshop.Data;
using Webshop.Model;
using Webshop.Model.Config;

namespace Webshop.ViewModel;

public class ConfigViewModel : ViewModelBase
{
    public ConfigViewModel()
    {
    }


    private bool testBool = Config.TestBool;

    public bool TestBool
    {
        get { return testBool; }
        set
        {
            testBool = value;
            Config.TestBool = value;
            NotifyPropertyChanged();
        }
    }

    private string testString = Config.TestString;

    public string TestString
    {
        get { return testString; }
        set
        {
            testString = value;
            Config.TestString = value;
            NotifyPropertyChanged();
        }
    }

    private UITheme uiTheme = Config.UITheme;

    public UITheme UITheme
    {
        get { return uiTheme; }
        set
        {
            uiTheme = value;
            Config.UITheme = value;
            NotifyPropertyChanged();
        }
    }

    private BaseUrlOption baseUrlOption = Config.BaseUrlOption;

    public BaseUrlOption BaseUrlOption
    {
        get { return baseUrlOption; }
        set
        {
            baseUrlOption = value;
            Config.BaseUrlOption = value;
            NotifyPropertyChanged();
        }
    }

}
