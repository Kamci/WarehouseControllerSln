using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.DTO;
using WarehouseController.Model;
using WarehouseController.View.OrderView;
using WarehouseController.ViewModel.Abstract;

namespace WarehouseController.ViewModel.OrderVM
{
    public partial class OrderViewModel : AItemListViewModel<OrderDto>
    {
        private OrderDto? _selectedOrder;
        public Command RefreshCommand { get; }
        public Command<OrderDto> AboutCommand { get; }
        public OrderViewModel() : base("Order")
        {
            RefreshCommand = LoadItemsCommand;
            AboutCommand = new Command<OrderDto>(OnOrderSelected);
        }



        public OrderDto? SelectedOrder
        {
            get => _selectedOrder;
            set => SetProperty(ref _selectedOrder, value);
        }
        async void OnOrderSelected(OrderDto order)
        {
            await GoToDetailsPage(order);
        }

        public override async Task GoToAddPage()
        {
          
            await Shell.Current.GoToAsync(nameof(AddOrderPage));
        }

        public override async Task GoToDetailsPage(OrderDto order)
        {
            if (order == null)
                return;
            SelectedOrder = order;
          
            await Shell.Current.GoToAsync($"{nameof(DetailsOrderPage)}?{nameof(DetailsOrderViewModel.ItemId)}={order.Id}");
        }
    }
}
