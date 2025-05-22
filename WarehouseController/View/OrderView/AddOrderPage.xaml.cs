using WarehouseController.ViewModel.OrderVM;

namespace WarehouseController.View.OrderView;

public partial class AddOrderPage : ContentPage
{
	public AddOrderPage()
	{
		BindingContext = new AddOrderViewModel();
        InitializeComponent();
	}
}