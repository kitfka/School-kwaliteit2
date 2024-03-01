using Webshop.Model.Config;

namespace Webshop.Maui
{
    public partial class App : Application
    {

        public App(Config config)
        {
            InitializeComponent();

            AppTheme theme = (AppTheme)(int)Config.UITheme;
            Application.Current.UserAppTheme = theme;
            MainPage = new AppShell();
        }
    }
}