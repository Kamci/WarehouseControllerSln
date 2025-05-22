using System.Collections.ObjectModel;
using WarehouseController.Model;

namespace WarehouseController.Services
{
    public class OrderDataStore : IDataStore<Order>
    {
        // Prosta baza danych w pamięci (możesz podmienić na prawdziwą)
        private readonly ObservableCollection<Order> orders = new()
        {
            new Order
            {
                Id = 1,
                OrderDate = DateTime.Now.AddDays(-3),
                Status = "Completed",
                UserId = 1,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem { Id = 1, ProductId = 1, Quantity = 2, OrderId = 1 },
                    new OrderItem { Id = 2, ProductId = 2, Quantity = 1, OrderId = 1 }
                }
            },
            new Order
            {
                Id = 2,
                OrderDate = DateTime.Now.AddDays(-1),
                Status = "Pending",
                UserId = 2,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem { Id = 3, ProductId = 3, Quantity = 5, OrderId = 2 }
                }
            }
        };

        public async Task<bool> AddItemAsync(Order item)
        {
            // Auto-increment ID (jeśli 0)
            if (item.Id == 0)
                item.Id = orders.Any() ? orders.Max(x => x.Id) + 1 : 1;

            // Ustaw OrderId w OrderItemach i nadaj im ID
            int nextOrderItemId = orders.SelectMany(o => o.OrderItems).Any()
                ? orders.SelectMany(o => o.OrderItems).Max(oi => oi.Id) + 1
                : 1;
            foreach (var oi in item.OrderItems)
            {
                oi.OrderId = item.Id;
                if (oi.Id == 0)
                    oi.Id = nextOrderItemId++;
            }

            orders.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Order item)
        {
            var old = orders.FirstOrDefault(o => o.Id == item.Id);
            if (old != null)
                orders.Remove(old);

            // Podobna logika jak w AddItemAsync (OrderId i Id dla OrderItem)
            int nextOrderItemId = orders.SelectMany(o => o.OrderItems).Any()
                ? orders.SelectMany(o => o.OrderItems).Max(oi => oi.Id) + 1
                : 1;
            foreach (var oi in item.OrderItems)
            {
                oi.OrderId = item.Id;
                if (oi.Id == 0)
                    oi.Id = nextOrderItemId++;
            }

            orders.Add(item);
            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var old = orders.FirstOrDefault(o => o.Id == id);
            if (old != null)
                orders.Remove(old);
            return await Task.FromResult(true);
        }

        public async Task<Order> GetItemAsync(int id)
        {
            var order = orders.FirstOrDefault(o => o.Id == id);
            // Głęboka kopia (opcjonalnie), aby uniknąć efektów ubocznych
            return await Task.FromResult(order);
        }

        public async Task<IEnumerable<Order>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(orders);
        }
    }
}
