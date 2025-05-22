using WarehouseController.ViewModel.OrderVM;

namespace WarehouseController.View.OrderView;

public partial class DetailsOrderPage : ContentPage
{
	public DetailsOrderPage()
	{
		BindingContext = new DetailsOrderViewModel();
        InitializeComponent();
	}
}