using Microcharts.Maui;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using SkiaSharp.Views.Maui.Controls.Hosting;
using WarehouseController.View.ProductView;
using WarehouseController.View.SupplierView;
using WarehouseController.View.UserView;
using WarehouseController.View.WarehouseView;
using WarehouseController.ViewModel.ProductVM;
using WarehouseController.ViewModel.SupplierVM;
using WarehouseController.ViewModel.UserVM;
using WarehouseController.ViewModel.WarehouseVM;

namespace WarehouseController;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder.UseSkiaSharp();
        builder
            .UseMauiApp<App>()
            .UseMicrocharts() 
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
        builder.Services.AddTransient<AddUserPage>();
        builder.Services.AddTransient<AddUserViewModel>();
        builder.Services.AddTransient<AddWarehousePage>();
        builder.Services.AddTransient<AddWarehouseViewModel>();
        builder.Services.AddTransient<AddSupplierPage>();
        builder.Services.AddTransient<AddSupplierViewModel>();

        return builder.Build();
    }
}

