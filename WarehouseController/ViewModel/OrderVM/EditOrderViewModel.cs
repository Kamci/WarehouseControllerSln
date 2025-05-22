using System.Collections.ObjectModel;
using System.Diagnostics;
using WarehouseController.DTO;
using WarehouseController.ViewModel.Abstract;

namespace WarehouseController.ViewModel.OrderVM;

[QueryProperty(nameof(ItemId), nameof(ItemId))]
public partial class EditOrderViewModel : AItemUpdateViewModel<OrderDto>
{
    // ────────── pola edycyjne ──────────
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

    // ────────── wlasciwosci ──────────
    public int Id { get => id; set => SetProperty(ref id, value); }
    public DateTime OrderDate { get => orderDate; set => SetProperty(ref orderDate, value); }
    public string Status { get => status; set => SetProperty(ref status, value); }
    public int UserId { get => userId; set => SetProperty(ref userId, value); }

    public ObservableCollection<OrderItemDto> OrderItems
    {
        get => orderItems;
        set => SetProperty(ref orderItems, value);
    }

    //  pola pomocnicze (entry w UI)
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

    // ────────── dodawanie / usuwanie pozycji ──────────
    public Command AddOrderItemCommand { get; }
    public Command<OrderItemDto> RemoveOrderItemCommand { get; }

    private void OnAddOrderItem()
    {
        Debug.WriteLine("Zapisuję zamówienie:");
        Debug.WriteLine($"Order ID: {Id}, Items count: {OrderItems.Count}");

        foreach (var item in OrderItems)
        {
            Debug.WriteLine($"Item ID: {item.Id}, ProductId: {item.ProductId}, Quantity: {item.Quantity}");
        }
        var existing = OrderItems.FirstOrDefault(i => i.ProductId == ProductId);
        if (existing != null)
            existing.Quantity += Quantity;
        else
            OrderItems.Add(new OrderItemDto { ProductId = ProductId, Quantity = Quantity });

        //  reset pól w formularzu
        ProductId = 0;
        Quantity = 0;
    }

    private void RemoveOrderItem(OrderItemDto item)
    {
        if (item != null)
            OrderItems.Remove(item);
    }

    // ────────── walidacja i mapowanie ──────────
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
                Id = oi.Id, // ← BEZ TEGO API myśli, że to nowe itemy
                ProductId = oi.ProductId,
                Quantity = oi.Quantity
            }).ToList()
        };
    }
    // ────────── wczytanie rekordu ──────────
    public override async Task LoadItem(int id)
    {
        foreach (var oi in OrderItems)
            Debug.WriteLine($"[DEBUG] DTO: ID={oi.Id}, ProductId={oi.ProductId}, Quantity={oi.Quantity}");
        var order = await DataStore.GetItemAsync(id);
        if (order != null)
        {
            Id = order.Id ?? 0;
            OrderDate = order.OrderDate;
            Status = order.Status;
            UserId = order.UserId;

            OrderItems = new ObservableCollection<OrderItemDto>(
                order.OrderItems.Select(oi => new OrderItemDto
                {
                    Id = oi.Id, // ← TO JEST KLUCZOWE!
                    ProductId = oi.ProductId,
                    Quantity = oi.Quantity
                }));
        }
    }
}
