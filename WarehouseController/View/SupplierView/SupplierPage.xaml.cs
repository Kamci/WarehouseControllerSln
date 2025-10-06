using WarehouseController.Model;
using WarehouseController.ViewModel.SupplierVM;

namespace WarehouseController;
[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class SupplierPage : ContentPage
{
    public SupplierViewModel _viewModel;
    public SupplierPage()
    {
        InitializeComponent();
        BindingContext = _viewModel = new SupplierViewModel();
    }

    private async void SuppliersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (SuppliersList.SelectedItem is Supplier selectedSupplier)
        {
            bool isConfirmed = await DisplayAlert("Supplier Selected", $"You selected: {selectedSupplier.Name}", "OK", "Cancel");
            if (isConfirmed)
            {
                _viewModel.SelectedSupplier = selectedSupplier;
            }
            else
            {
                SuppliersList.SelectedItem = null;
            }
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is SupplierViewModel vm)
            vm.LoadItemsCommand.Execute(null);
        _viewModel.OnAppearing();
    }

}