using WarehouseController.ViewModel.ShipmentVM;

namespace WarehouseController.View.ShipmentView;

public partial class EditShipmentPage : ContentPage
{
	public EditShipmentPage()
	{
		InitializeComponent();
		BindingContext = new EditShipmentViewModel();
    }
}