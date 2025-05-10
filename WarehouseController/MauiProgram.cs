using Microcharts.Maui;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using WarehouseController.View.ProductView;
using WarehouseController.ViewModel.ProductVM;

namespace WarehouseController;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMicrocharts() // Add Microcharts.Maui package
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();

#endif
        builder.Services.AddTransient<AddProductPage>();
        builder.Services.AddTransient<AddProductViewModel>();

        return builder.Build();
    }
}

