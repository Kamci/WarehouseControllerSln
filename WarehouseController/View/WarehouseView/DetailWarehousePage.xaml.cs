using WarehouseController.ViewModel.WarehouseVM;

namespace WarehouseController.View.WarehouseView;

public partial class DetailWarehousePage : ContentPage
{
	public DetailWarehousePage()
	{
		InitializeComponent();
        BindingContext = new DetailWarehouseViewModel();
    }
}