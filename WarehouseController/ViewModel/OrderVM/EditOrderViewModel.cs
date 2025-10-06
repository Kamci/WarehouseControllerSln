using System.Collections.ObjectModel;
using System.Diagnostics;
using WarehouseController.DTO;
using WarehouseController.Model;
using WarehouseController.Services.Implementations;
using WarehouseController.ViewModel.Abstract;

namespace WarehouseController.ViewModel.OrderVM;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class EditOrderViewModel : AItemUpdateViewModel<OrderDto>
{
  
    private int id;
    private DateTime orderDate;
    private string status = string.Empty;
    private int userId;

    private ObservableCollection<OrderItemDto> orderItems = new();
    private int productId;
    private int quantity;

    public EditOrderViewModel() : base("Edit Order")
    {
        AddOrderItemCommand = new Command(OnAddOrderItem);
        RemoveOrderItemCommand = new Command<OrderItemDto>(RemoveOrderItem);
    }


    public int Id { get => id; set => SetProperty(ref id, value); }
    public DateTime OrderDate { get => orderDate; set => SetProperty(ref orderDate, value); }
    public string Status { get => status; set => SetProperty(ref status, value); }
    public int UserId { get => userId; set => SetProperty(ref userId, value); }

    public ObservableCollection<OrderItemDto> OrderItems
    {
        get => orderItems;
        set => SetProperty(ref orderItems, value);
    }


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
 

    public Command AddOrderItemCommand { get; }
    public Command<OrderItemDto> RemoveOrderItemCommand { get; }

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

    private void RemoveOrderItem(OrderItemDto item)
    {
        if (item != null)
            OrderItems.Remove(item);
    }

 
    public override bool ValidateSave() =>
        !string.IsNullOrWhiteSpace(Status)
        && OrderDate != default
        && UserId > 0
        && OrderItems.Count > 0;

    public override OrderDto SetItem()
    {

        return new OrderDto
        {
            Id = Id,
            OrderDate = OrderDate,
            Status = Status,
            UserId = UserId,
            OrderItems = OrderItems.Select(oi => new OrderItemDto
            {
                Id = oi.Id, 
                ProductId = oi.ProductId,
                Quantity = oi.Quantity
            }).ToList()
        };
    }
   
    public override async Task LoadItem(int id)
    {
       
        var order = await DataStore.GetItemAsync(id);
        if (order != null)
        {
            Id = order.Id ?? 0;
            OrderDate = order.OrderDate;
            Status = order.Status;
            UserId = order.UserId;
            await LoadUsersAsync();
            SelectedStatus = Statuses.FirstOrDefault(s => s == order.Status);
            SelectedUser = Users.FirstOrDefault(u => u.Id == UserId);
            await LoadProductsAsync();

            OrderItems = new ObservableCollection<OrderItemDto>(
              order.OrderItems.Select(oi =>
              {
                  var product = Products.FirstOrDefault(p => p.Id == oi.ProductId);
                  return new OrderItemDto
                  {
                      Id = oi.Id,
                      ProductId = oi.ProductId,
                      Quantity = oi.Quantity,
                      ProductName = product?.Name ?? $"Product #{oi.ProductId}"
                  };
              }));
        }


    }

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
            SetProperty(ref selectedStatus, value);
            Status = value; 
        }
    }
    public ObservableCollection<User> Users { get; set; } = new();

    private User selectedUser;
    public User SelectedUser
    {
        get => selectedUser;
        set
        {
            if (SetProperty(ref selectedUser, value) && value != null)
                UserId = value.Id;
        }
    }

    public async Task LoadUsersAsync()
    {
        await _referenceHelper.LoadUsersAsync(Users);
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
