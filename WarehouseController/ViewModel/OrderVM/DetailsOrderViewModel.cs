using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using WarehouseController.DTO;
using WarehouseController.Model;
using WarehouseController.Services.Implementations;
using WarehouseController.View.OrderView;
using WarehouseController.ViewModel.Abstract;

namespace WarehouseController.ViewModel.OrderVM
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class DetailsOrderViewModel : AItemDetailsViewModel<OrderDto>

    {
        public DetailsOrderViewModel() : base("Order Details")
        { }

        private int id;
        private DateTime orderDate;
        private string status = string.Empty;
        private int userId;
        private ObservableCollection<OrderItemDto> orderItems = new();


        public int Id { get => id; set => SetProperty(ref id, value); }
        public DateTime OrderDate { get => orderDate; set => SetProperty(ref orderDate, value); }
        public string Status { get => status; set => SetProperty(ref status, value); }
        public int UserId { get => userId; set => SetProperty(ref userId, value); }
        public ObservableCollection<OrderItemDto> OrderItems
        {
            get => orderItems;
            set => SetProperty(ref orderItems, value);
        }

        public override async Task LoadItem(int id)
        {
            try
            {
                var item = await DataStore.GetItemAsync(id);
                if (item != null)
                {
                    Id = item.Id ?? 0;
                    OrderDate = item.OrderDate;
                    Status = item.Status;
                    UserId = item.UserId;

                    var enrichedItems = new ObservableCollection<OrderItemDto>();

                    foreach (var orderItem in item.OrderItems)
                    {
                        orderItem.ProductName = await _refProvider.GetProductNameByIdAsync(orderItem.ProductId);
                        enrichedItems.Add(orderItem);
                    }

                    OrderItems = enrichedItems;
                    UserDisplay = await _refProvider.GetUserLoginByIdAsync(UserId);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ViewModel load error: {ex}");
                throw;
            }
        }


        protected override async Task GoToUpdatePage()
        {
            await Shell.Current.GoToAsync($"{nameof(EditOrderPage)}?{nameof(EditOrderViewModel.ItemId)}={Id}");
        }


        #region DisplayName

        private readonly ReferenceNameProvider _refProvider = new();
        private string userDisplay;
        public string UserDisplay
        {
            get => userDisplay;
            set => SetProperty(ref userDisplay, value);
        }
        private string productDisplay;
        public string ProductDisplay
        {
            get => productDisplay;
            set => SetProperty(ref productDisplay, value);
        }

        
        #endregion
    }
}
