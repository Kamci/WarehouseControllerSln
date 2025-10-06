using WarehouseController.Services.Implementations.Authorization;
using WarehouseController.ViewModel.OrderVM;

namespace WarehouseController.View.OrderView;

public partial class AddOrderPage : ContentPage
{
	public AddOrderPage()
	{
		BindingContext = new AddOrderViewModel();
        InitializeComponent();
	}


    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is AddOrderViewModel vm)
        {
            await vm.LoadProductsAsync();
        }
    }
}