using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.Model;
using WarehouseController.View.OrderView;
using WarehouseController.ViewModel.Abstract;

namespace WarehouseController.ViewModel.OrderVM
{
    public partial class OrderViewModel : AItemListViewModel<Order>
    {
        private Order? _selectedOrder;
        public Command RefreshCommand { get; }
        public Command<Order> AboutCommand { get; }
        public OrderViewModel() : base("Order")
        {
            RefreshCommand = LoadItemsCommand;
            AboutCommand = new Command<Order>(OnOrderSelected);
        }



        public Order? SelectedOrder
        {
            get => _selectedOrder;
            set => SetProperty(ref _selectedOrder, value);
        }
        async void OnOrderSelected(Order order)
        {
            await GoToDetailsPage(order);
        }

        public override async Task GoToAddPage()
        {
            // Załaduj nową stronę przez Shell
            await Shell.Current.GoToAsync(nameof(AddOrderPage));
        }

        public override async Task GoToDetailsPage(Order order)
        {
            if (order == null)
                return;
            SelectedOrder = order;
            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(DetailsOrderPage)}?{nameof(DetailsOrderViewModel.ItemId)}={order.Id}");
        }
    }
}
