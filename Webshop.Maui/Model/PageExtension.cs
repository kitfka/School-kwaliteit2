namespace Webshop.Maui.Model;

public static class PageExtension
{
    public static void GoToAsync(this ContentPage page, ShellNavigationState state)
    {
        var s = (Shell)App.Current.MainPage;
        s.GoToAsync(state);
    }

    public static void GoToAsync(this ContentPage page, ShellNavigationState state, bool animate)
    {
        var s = (Shell)App.Current.MainPage;
        s.GoToAsync(state, animate);
    }

    public static void GoToAsync(this ContentPage page, ShellNavigationState state, IDictionary<string, object> parameters)
    {
        var s = (Shell)App.Current.MainPage;
        s.GoToAsync(state, parameters);
    }

    public static void GoToAsync(this ContentPage page, ShellNavigationState state, bool animate, IDictionary<string, object> parameters)
    {
        var s = (Shell)App.Current.MainPage;
        s.GoToAsync(state, animate, parameters);
    }
}