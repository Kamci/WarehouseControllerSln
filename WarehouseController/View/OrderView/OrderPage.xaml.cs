using WarehouseController.DTO;
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
        if (OrdersList.SelectedItem is OrderDto selectedOrder)
        {
            bool isConfirmed = await DisplayAlert("Order Selected", $"You selected: {selectedOrder.Id} - {selectedOrder.OrderDate} ", "OK", "Cancel");

            if (isConfirmed)
            {
                viewModel.SelectedOrder = selectedOrder;
            }
            else
            {
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