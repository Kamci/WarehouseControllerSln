using System.Diagnostics;
using WarehouseController.View;
using WarehouseController.View.CategoryView;
using WarehouseController.View.OrderView;
using WarehouseController.View.ProductView;
using WarehouseController.View.ShipmentView;
using WarehouseController.View.SupplierView;
using WarehouseController.View.UserView;
using WarehouseController.View.WarehouseView;


namespace WarehouseController
{
    [DebuggerDisplay($"{{{nameof(GetDebuggerDisplay)}(),nq}}")]
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(DashBoard), typeof(DashBoard));
            Routing.RegisterRoute(nameof(AddProductPage), typeof(AddProductPage));
            Routing.RegisterRoute(nameof(EditProductPage), typeof(EditProductPage));
            Routing.RegisterRoute(nameof(DetailProductPage), typeof(DetailProductPage));
            Routing.RegisterRoute(nameof(AddWarehousePage), typeof(AddWarehousePage));
            Routing.RegisterRoute(nameof(EditWarehousePage), typeof(EditWarehousePage));
            Routing.RegisterRoute(nameof(DetailWarehousePage), typeof(DetailWarehousePage));
            Routing.RegisterRoute(nameof(AddUserPage), typeof(AddUserPage));
            Routing.RegisterRoute(nameof(EditUserPage), typeof(EditUserPage));
            Routing.RegisterRoute(nameof(DetailUserPage), typeof(DetailUserPage));
            Routing.RegisterRoute(nameof(AddSupplierPage), typeof(AddSupplierPage));
            Routing.RegisterRoute(nameof(EditSupplierPage), typeof(EditSupplierPage));
            Routing.RegisterRoute(nameof(DetailsSupplierPage), typeof(DetailsSupplierPage));
            Routing.RegisterRoute(nameof(AddShipmentPage), typeof(AddShipmentPage));
            Routing.RegisterRoute(nameof(EditShipmentPage), typeof(EditShipmentPage));
            Routing.RegisterRoute(nameof(DetailsShipmentPage), typeof(DetailsShipmentPage));
            Routing.RegisterRoute(nameof(AddCategoryPage), typeof(AddCategoryPage));
            Routing.RegisterRoute(nameof(EditCategoryPage), typeof(EditCategoryPage));
            Routing.RegisterRoute(nameof(DetailsCategoryPage), typeof(DetailsCategoryPage));
            Routing.RegisterRoute(nameof(AddOrderPage), typeof(AddOrderPage));
            Routing.RegisterRoute(nameof(DetailsOrderPage), typeof(DetailsOrderPage));
            Routing.RegisterRoute(nameof(EditOrderPage), typeof(EditOrderPage));

        }

        private string GetDebuggerDisplay()
        {
            return ToString();
        }
        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            
            Shell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
