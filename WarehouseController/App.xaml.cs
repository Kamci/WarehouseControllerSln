using System.Diagnostics;
using System.Net.Http;
using WarehouseController.Model;
using WarehouseController.Services;
using WarehouseController.Services.DataStores;
using WarehouseController.Services.Implementations;
using WarehouseController.Services.Interfaces;
using WarehouseController.View;
using WarehouseController.ViewModel.Dashboard;

namespace WarehouseController
{
    public partial class App : Application
    {
        
        public App()
        {

            InitializeComponent();


            DependencyService.Register<ProductDataStore>(); 
            DependencyService.Register<UserDataStore>();
            DependencyService.Register<WarehouseDataStore>();
            DependencyService.Register<SupplierDataStore>();
            DependencyService.Register<ShipmentDataStore>();
            DependencyService.Register<CategoryDataStore>();
            DependencyService.Register<OrderDataStore>();
           

            AppDomain.CurrentDomain.UnhandledException += (sender, e) =>
            {
                Debug.WriteLine($"[UnhandledException] {e.ExceptionObject}");
            };

            TaskScheduler.UnobservedTaskException += (sender, e) =>
            {
                Debug.WriteLine($"[UnobservedTaskException] {e.Exception}");
            };
            
        }

    
        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}