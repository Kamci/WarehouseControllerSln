using System.Collections.ObjectModel;
using WarehouseController.ViewModel.Abstract;
using WarehouseController.DTO;
using WarehouseController.Services.Implementations;
using WarehouseController.Model;

namespace WarehouseController.ViewModel.OrderVM
{
    public partial class AddOrderViewModel : ANewItemViewModel<OrderDto>
    {
        private int id;
        private DateTime orderDate = DateTime.Now;
        private string status = string.Empty;
        private int userId;
        private ObservableCollection<OrderItemDto> orderItems = new();
        private int productId;
        private int quantity;
        public AddOrderViewModel() : base("Add Order")
        {
            OrderDate = DateTime.Now;
            Status = "Pending";
            AddOrderItemCommand = new Command(OnAddOrderItem);
            RemoveOrderItemCommand = new Command<OrderItemDto>(RemoveOrderItem);

        }
        private async void OnAddOrderItem()
        {
            // Możesz np. otworzyć modal do wyboru produktu i ilości,
            // a po potwierdzeniu dodać do OrderItems:
            // sprawdź, czy dany produkt jest już na liście
            var existing = OrderItems.FirstOrDefault(i => i.ProductId == ProductId);
            string productName = SelectedProduct?.Name ?? "Unknown";
            if (existing != null)
            {
                // jeśli jest – tylko zwiększamy ilość
                existing.Quantity += Quantity;
            }
            else
            {
                // jeśli nie ma – dodajemy nowy wiersz
                OrderItems.Add(new OrderItemDto
                {
                    ProductId = ProductId,
                    Quantity = Quantity,
                      ProductName = productName // ← Ustaw nazwę
                });
            }

            // opcjonalnie wyczyść pola formularza
            Quantity = 0;
            SelectedProduct = null;
        }
        public void RemoveOrderItem(OrderItemDto item)
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
        public ObservableCollection<OrderItemDto> OrderItems
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
        public override OrderDto SetItem()
        {
            return new OrderDto
            {
                OrderDate = OrderDate,
                Status = Status,
                UserId = UserId,
                OrderItems = OrderItems.Select(oi => new OrderItemDto
                {
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity
                }).ToList()
            };
        }
        public Command AddOrderItemCommand { get; }
        public Command<OrderItemDto> RemoveOrderItemCommand { get; }

        #region Combobox
        private readonly ReferenceDataHelper _referenceHelper = new();

        public ObservableCollection<User> Users { get; set; } = new();

        private User selectedUser;
        public User SelectedUser
        {
            get => selectedUser;
            set
            {
                if (SetProperty(ref selectedUser, value) && value != null)
                    UserId = value.Id; // zakładam że masz już właściwość UserId
            }
        }

        public async Task LoadUsersAsync()
        {
            await _referenceHelper.LoadUsersAsync(Users);
        }

        public ObservableCollection<Product> Products { get; set; } = new();

        private Product selectedProduct;
        public Product SelectedProduct
        {
            get => selectedProduct;
            set
            {
                if (SetProperty(ref selectedProduct, value) && value != null)
                {
                    ProductId = selectedProduct.Id;
                }
            }
        }
        public async Task LoadProductsAsync()
        {
            await _referenceHelper.LoadProductsAsync(Products);
        }
            #endregion
        }
}
