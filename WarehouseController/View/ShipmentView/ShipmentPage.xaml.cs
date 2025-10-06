using WarehouseController.ViewModel.ProductVM;
using WarehouseController.Model;
using WarehouseController.ViewModel.ShipmentVM;
using WarehouseController.DTO;
namespace WarehouseController;


public partial class ShipmentPage : ContentPage
{
	ShipmentViewModel _viewModel;
    public ShipmentPage()
	{
		BindingContext = _viewModel = new ShipmentViewModel();
        InitializeComponent();
	}
    private async void ShipmentsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (ShipmentsList.SelectedItem is ShipmentDTO selectedShipment)
        {
            bool isConfirmed = await DisplayAlert("Product Selected", $"You selected: {selectedShipment.Id} - {selectedShipment.ShipmentDate}", "OK", "Cancel");

            if (isConfirmed)
            {
                _viewModel.SelectedShipment = selectedShipment;
            }
            else
            {
                ShipmentsList.SelectedItem = null;
            }

        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ShipmentViewModel vm)
            vm.LoadItemsCommand.Execute(null);
        _viewModel.OnAppearing();
    }

}