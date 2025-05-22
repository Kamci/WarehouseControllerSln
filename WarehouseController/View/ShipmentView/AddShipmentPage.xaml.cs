using System.Diagnostics;
using WarehouseController.Model;
using WarehouseController.ViewModel.ShipmentVM;

namespace WarehouseController.View.ShipmentView;

public partial class AddShipmentPage : ContentPage
{
	public Shipment Item { get; set; }
    public AddShipmentPage()
	{
		BindingContext = new AddShipmentViewModel();
        InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is AddShipmentViewModel vm)
        {
            await vm.LoadDataAsync();
        }
    }
}