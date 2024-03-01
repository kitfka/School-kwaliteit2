using System.Reflection;
using Webshop.Maui.Model;
using Webshop.Model;
using Webshop.Model.Config;
using Webshop.Model.Interfaces;
using Webshop.Service;

namespace Webshop.Maui;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .RegisterServices()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });


        var app = builder.Build();
        serviceProvider = app.Services; // HACK
        return app;
    }


    //https://github.com/dotnet/maui/issues/5507
    public static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
    {

        var currentAssembly = Assembly.GetExecutingAssembly();
        var otherAssembly = Assembly.GetAssembly(typeof(ViewModelBase));

        // Transient objects lifetime services are created each time they are requested.
        // This lifetime works best for lightweight, stateless services.
        foreach (var type in currentAssembly.DefinedTypes.Where(e => e.IsSubclassOf(typeof(Page)) || e.IsSubclassOf(typeof(ViewModelBase))))
        {
            builder.Services.AddTransient(type.AsType());
        }

        foreach (var type in otherAssembly.DefinedTypes.Where(e => e.IsSubclassOf(typeof(ViewModelBase))))
        {
            builder.Services.AddTransient(type.AsType());
        }

        foreach (var type in currentAssembly.DefinedTypes.Where(e => e.IsSubclassOf(typeof(View))))
        {
            builder.Services.AddTransient(type.AsType());
        }

        // Oke, other services.
        builder.Services.AddSingleton<ApiService>();
        builder.Services.AddTransient<ProductService>();
        builder.Services.AddTransient<UserService>();
        builder.Services.AddSingleton<CartService>();
        builder.Services.AddTransient<Config>();

        return builder;
    }


    // hack https://github.com/dotnet/maui/discussions/8363
    static IServiceProvider serviceProvider;

    public static TService GetService<TService>()
        => serviceProvider.GetService<TService>();


}