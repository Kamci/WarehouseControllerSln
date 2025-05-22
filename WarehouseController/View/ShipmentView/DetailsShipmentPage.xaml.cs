using WarehouseController.ViewModel.ShipmentVM;

namespace WarehouseController.View.ShipmentView;

public partial class DetailsShipmentPage : ContentPage
{
	public DetailsShipmentPage()
	{
		BindingContext = new DetailsShipmentViewModel();
        InitializeComponent();
	}
}