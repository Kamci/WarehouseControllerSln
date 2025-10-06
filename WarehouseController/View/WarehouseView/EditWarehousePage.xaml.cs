using WarehouseController.ViewModel.WarehouseVM;

namespace WarehouseController.View.WarehouseView;

public partial class EditWarehousePage : ContentPage
{
	public EditWarehousePage()
	{
		InitializeComponent();
        BindingContext = new EditWarehouseViewModel();
    }
}