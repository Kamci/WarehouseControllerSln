using System.Collections.ObjectModel;
using WarehouseController.ViewModel.Abstract;
using WarehouseController.DTO;
using WarehouseController.Services.Implementations;
using WarehouseController.Model;
using WarehouseController.Services.Implementations.Authorization;

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
        private string loggedInLogin;
        public string LoggedInLogin
        {
            get => loggedInLogin;
            set => SetProperty(ref loggedInLogin, value);
        }
        public AddOrderViewModel() : base("Add Order")
        {
            UserId = SessionService.LoggedInUserId;
            OrderDate = DateTime.Now;
            AddOrderItemCommand = new Command(OnAddOrderItem);
            LoggedInLogin = SessionService.LoggedInUserLogin;
            RemoveOrderItemCommand = new Command<OrderItemDto>(RemoveOrderItem);

        }
        private void OnAddOrderItem()
        {
            var existing = OrderItems.FirstOrDefault(i => i.ProductId == ProductId);
            string productName = SelectedProduct?.Name ?? "Unknown";
            if (existing != null)
            {
                existing.Quantity += Quantity;
            }
            else
            {
              
                OrderItems.Add(new OrderItemDto
                {
                    ProductId = ProductId,
                    Quantity = Quantity,
                    ProductName = productName 
                });
            }

            Quantity = 0;
            SelectedProduct = null;
        }
        public void RemoveOrderItem(OrderItemDto item)
        {
            if (item != null && OrderItems.Contains(item))
                orderItems.Remove(item);
        }
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
        public ObservableCollection<string> Statuses { get; set; } =
               new ObservableCollection<string> { "Open", "Processing", "Shipped", "Delivered", "Cancelled", "Returned" };

        private string selectedStatus;
        public string SelectedStatus
        {
            get => selectedStatus;
            set
            {
                if (SetProperty(ref selectedStatus, value))
                {
                    Status = value; 
                }
            }
        }
     
        public ObservableCollection<ProductDto> Products { get; set; } = new();

        private ProductDto selectedProduct;
        public ProductDto SelectedProduct
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
