using WarehouseController.Model;
using WarehouseController.ViewModel.WarehouseVM;
namespace WarehouseController;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class WarehousePage : ContentPage
{
    WarehouseViewModel _viewModel;
    public WarehousePage()
    {
        InitializeComponent();
        BindingContext = _viewModel = new WarehouseViewModel();

    }

    private async void WarehousesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (WarehosuesList.SelectedItem is Warehouse selectedWarehouse)
        {
            bool isConfirmed = await DisplayAlert("Warehouse Selected", $"You selected: {selectedWarehouse.Name}", "OK", "Cancel");

            if (isConfirmed)
            {
                _viewModel.SelectedWarehouse = selectedWarehouse;
            }
            else
            {
                WarehosuesList.SelectedItem = null;
            }

        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is WarehouseViewModel vm)
            vm.LoadItemsCommand.Execute(null);
        _viewModel.OnAppearing();
    }

}