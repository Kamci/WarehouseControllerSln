using WarehouseController.ViewModel.OrderVM;

namespace WarehouseController.View.OrderView;

public partial class EditOrderPage : ContentPage
{
	public EditOrderPage()
	{
		BindingContext = new EditOrderViewModel();
		InitializeComponent();
	}
}