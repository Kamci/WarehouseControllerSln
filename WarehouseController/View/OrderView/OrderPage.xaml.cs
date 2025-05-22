using WarehouseController.Model;
using WarehouseController.ViewModel.OrderVM;

namespace WarehouseController;

public partial class OrderPage : ContentPage
{
	OrderViewModel viewModel;
    public OrderPage()
	{
		InitializeComponent();
		BindingContext = viewModel = new OrderViewModel();
    }

    private async void OrdersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (OrdersList.SelectedItem is Order selectedOrder)
        {
            bool isConfirmed = await DisplayAlert("Order Selected", $"You selected: {selectedOrder.Id} - {selectedOrder.OrderDate} ", "OK", "Cancel");

            if (isConfirmed)
            {
                viewModel.SelectedOrder = selectedOrder;
            }
            else
            {
                // użytkownik anulował — nie robimy nic
                // można ewentualnie wyczyścić zaznaczenie:
                OrdersList.SelectedItem = null;
            }

        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is OrderViewModel vm)
            vm.LoadItemsCommand.Execute(null);
        viewModel.OnAppearing();
    }

}