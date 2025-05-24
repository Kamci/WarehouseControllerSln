using WarehouseController.ViewModel.OrderVM;

namespace WarehouseController.View.OrderView;

public partial class DetailsOrderPage : ContentPage
{
	public DetailsOrderPage()
	{
		BindingContext = new DetailsOrderViewModel();
        InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is DetailsOrderViewModel vm)
            await vm.LoadItem(vm.ItemId); 
    }
}