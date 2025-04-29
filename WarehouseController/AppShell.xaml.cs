using WarehouseController.View.ProductView;

namespace WarehouseController
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddProductPage), typeof(AddProductPage));

        }
    }
}
