using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.Model;
using WarehouseController.ViewModel.Abstract;

namespace WarehouseController.ViewModel.OrderVM
{
    public partial class EditOrderViewModel : AItemUpdateViewModel<Order>
    {
        private int id;
        private DateTime orderDate;
        private string status = string.Empty;
        private int userId;
        private ObservableCollection<OrderItem> orderItems = new();

        // Pola do dodawania nowego itemu
        private int productId;
        private int quantity;

        public EditOrderViewModel() : base("Edit Order")
        {
            AddOrderItemCommand = new Command(OnAddOrderItem);
            RemoveOrderItemCommand = new Command<OrderItem>(RemoveOrderItem);
        }

        public int Id { get => id; set => SetProperty(ref id, value); }
        public DateTime OrderDate { get => orderDate; set => SetProperty(ref orderDate, value); }
        public string Status { get => status; set => SetProperty(ref status, value); }
        public int UserId { get => userId; set => SetProperty(ref userId, value); }
        public ObservableCollection<OrderItem> OrderItems
        {
            get => orderItems;
            set => SetProperty(ref orderItems, value);
        }

        // Pola do edycji/wyboru nowych itemów (do UI, np. ComboBox na Product i Entry na Quantity)
        public int ProductId
        {
            get => productId;
            set => SetProperty(ref productId, value);
        }
        public int Quantity
        {
            get => quantity;
            set => SetProperty(ref quantity, value);
        }

        // Dodajemy nowe itemy do zamówienia
        public Command AddOrderItemCommand { get; }
        private async void OnAddOrderItem()
        {
            // Możesz np. otworzyć modal do wyboru produktu i ilości,
            // a po potwierdzeniu dodać do OrderItems:
            var newOrderItem = new OrderItem
            {
                ProductId = ProductId,
                Quantity = Quantity
            };
            OrderItems.Add(newOrderItem);
        }

        // Usuwanie itemów z zamówienia
        public Command<OrderItem> RemoveOrderItemCommand { get; }
        public void RemoveOrderItem(OrderItem item)
        {
            if (item != null && OrderItems.Contains(item))
                OrderItems.Remove(item);
        }

        public override bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(Status)
                && OrderDate != default
                && UserId > 0
                && OrderItems.Count > 0;
        }

        public override Order SetItem()
        {
            return new Order
            {
                Id = Id,
                OrderDate = OrderDate,
                Status = Status,
                UserId = UserId,
                OrderItems = [.. OrderItems]
            };
        }

        public override async Task LoadItem(int id)
        {
            var order = await DataStore.GetItemAsync(id);
            if (order != null)
            {
                Id = order.Id;
                OrderDate = order.OrderDate;
                Status = order.Status;
                UserId = order.UserId;
                OrderItems = new ObservableCollection<OrderItem>(order.OrderItems);
            }
        }

   
    }
}
