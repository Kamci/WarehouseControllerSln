using System.Collections.ObjectModel;
using WarehouseController.Model;
using WarehouseController.ViewModel.Abstract;

namespace WarehouseController.ViewModel.OrderVM
{
    public partial class AddOrderViewModel : ANewItemViewModel<Order>
    {
        private int id;
        private DateTime orderDate = DateTime.Now;
        private string status = string.Empty;
        private int userId;
        private ObservableCollection<OrderItem> orderItems = new();
        private int productId;
        private int quantity;
        public AddOrderViewModel() : base("Add Order")
        {
            OrderDate = DateTime.Now;
            Status = "New";
            AddOrderItemCommand = new Command(OnAddOrderItem);
            RemoveOrderItemCommand = new Command<OrderItem>(RemoveOrderItem);

        }
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
        public void RemoveOrderItem(OrderItem item)
        {
            if (item != null && OrderItems.Contains(item))
                orderItems.Remove(item);
        }
        public int Id { get => id; set => SetProperty(ref id, value); }
        public DateTime OrderDate { get => orderDate; set => SetProperty(ref orderDate, value); }
        public string Status { get => status; set => SetProperty(ref status, value); }
        public int UserId { get => userId; set => SetProperty(ref userId, value); }
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
        public ObservableCollection<OrderItem> OrderItems
        {
            get => orderItems;
            set => SetProperty(ref orderItems, value);
        }
        public override bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(Status)
                     && OrderDate != default
                        && UserId > 0
                     && orderItems.Count > 0;
        }
        public override Order SetItem()
        {
            return new Order
            {
                OrderDate = OrderDate,
                Status = Status,
                UserId = UserId,
                OrderItems = [.. orderItems]

            };
        }
        public Command AddOrderItemCommand { get; }
        public Command<OrderItem> RemoveOrderItemCommand { get; }


    }
}
