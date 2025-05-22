using WarehouseController.ViewModel.ShipmentVM;

namespace WarehouseController.View.ShipmentView;

public partial class EditShipmentPage : ContentPage
{
	public EditShipmentPage()
	{
		InitializeComponent();
		BindingContext = new EditShipmentViewModel();
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is EditShipmentViewModel vm)
        {
            await vm.LoadDataAsync();
        }
    }
}