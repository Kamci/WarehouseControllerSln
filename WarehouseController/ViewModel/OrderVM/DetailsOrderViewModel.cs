using System;
using System.Collections.Generic;
using System.Diagnostics;
using WarehouseController.DTO;
using WarehouseController.View.OrderView;
using WarehouseController.ViewModel.Abstract;

namespace WarehouseController.ViewModel.OrderVM
{
    public partial class DetailsOrderViewModel : AItemDetailsViewModel<OrderDto>

    {
        public DetailsOrderViewModel() : base("Order Details")
        {
            try
            {
                // inicjalizacja
                Debug.WriteLine("DetailOrderViewModel loaded");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ViewModel init error: {ex}");
                throw; // Możesz to potem usunąć
            }
        }

        private int id;
        private DateTime orderDate;
        private string status = string.Empty;
        private int userId;
        private ICollection<OrderItemDto> orderItems = new List<OrderItemDto>();

        public int Id { get => id; set => SetProperty(ref id, value); }
        public DateTime OrderDate { get => orderDate; set => SetProperty(ref orderDate, value); }
        public string Status { get => status; set => SetProperty(ref status, value); }
        public int UserId { get => userId; set => SetProperty(ref userId, value); }
        public ICollection<OrderItemDto> OrderItems { get => orderItems; set => SetProperty(ref orderItems, value); }
        public override async Task LoadItem(int id)
        {
            try
            {
                var item = await DataStore.GetItemAsync(id);
                if (item != null)
                {
                    Id = (int)item.Id;
                    OrderDate = item.OrderDate;
                    Status = item.Status;
                    UserId = item.UserId;
                    OrderItems = item.OrderItems;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ViewModel load error: {ex}");
                throw; // Możesz to potem usunąć
            }
        }


        protected override async Task GoToUpdatePage()
        {
            Debug.WriteLine($"[NAVIGATE] Going to EditOrderPage with ID: {Id}");

            await Shell.Current.GoToAsync($"{nameof(EditOrderPage)}?{nameof(EditOrderViewModel.ItemId)}={Id}");
        }

 
    }
}
