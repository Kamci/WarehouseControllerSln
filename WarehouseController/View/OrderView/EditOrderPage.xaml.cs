using WarehouseController.ViewModel.OrderVM;

namespace WarehouseController.View.OrderView;

public partial class EditOrderPage : ContentPage
{
	public EditOrderPage()
	{
		BindingContext = new EditOrderViewModel();
		InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is EditOrderViewModel vm)
        {
            await vm.LoadUsersAsync();
            await vm.LoadProductsAsync();
        }
    }

}