using WarehouseController.View.ProductView;
using WarehouseController.View.WarehouseView;


namespace WarehouseController
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddProductPage), typeof(AddProductPage));
            Routing.RegisterRoute(nameof(EditProductPage), typeof(EditProductPage));
            Routing.RegisterRoute(nameof(DetailProductPage), typeof(DetailProductPage));
            Routing.RegisterRoute(nameof(AddWarehousePage), typeof(AddWarehousePage));
            Routing.RegisterRoute(nameof(EditWarehousePage), typeof(EditWarehousePage));
            Routing.RegisterRoute(nameof(DetailWarehousePage), typeof(DetailWarehousePage));

        }
    }
}
