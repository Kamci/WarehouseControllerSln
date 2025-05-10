using System.Diagnostics;
using WarehouseController.Model;
using WarehouseController.Services;

namespace WarehouseController
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DependencyService.Register<ProductDataStore>();
            DependencyService.Register<WarehouseDataStore>();
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