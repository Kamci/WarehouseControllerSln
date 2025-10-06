using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WarehouseController.Model;

namespace WarehouseController.Services.Implementations
{
    public class ReferenceNameProvider
    {
        private readonly SupplierService _supplierService = new();
        private readonly WarehouseService _warehouseService = new();
        private readonly CategoryService _categoryService = new();
        private readonly UserService _userService = new();
        private readonly ShipmentService _shipmentService = new();
        private readonly ProductService _productService = new();

  
        public async Task<string> GetUserLoginByIdAsync(int id)
        {
            var list = await _userService.GetAllAsync();
            return list.FirstOrDefault(u => u.Id == id)?.Login ?? $"User #{id}";
        }

        public async Task<string> GetWarehouseNameByIdAsync(int id)
        {
            var list = await _warehouseService.GetAllAsync();
            return list.FirstOrDefault(w => w.Id == id)?.Name ?? $"Warehouse #{id}";
        }

        public async Task<string> GetCategoryNameByIdAsync(int id)
        {
            var list = await _categoryService.GetAllAsync();
            return list.FirstOrDefault(c => c.Id == id)?.Name ?? $"Category #{id}";
        }

        public async Task<string> GetSupplierNameByIdAsync(int id)
        {
            var list = await _supplierService.GetAllAsync();
            return list.FirstOrDefault(s => s.Id == id)?.Name ?? $"Supplier #{id}";
        }

        public async Task<string> GetProductNameByIdAsync(int id)
        {
            var list = await _productService.GetAllAsync();
            return list.FirstOrDefault(s => s.Id == id)?.Name ?? $"Product #{id}";
        }

        public async Task<string> GetShipmentDateByIdAsync(int id)
        {
            var list = await _shipmentService.GetAllAsync();
            var shipment = list.FirstOrDefault(s => s.Id == id);
            return shipment != null ? $"{shipment.ShipmentDate:yyyy-MM-dd} (ID: {shipment.Id})" : $"Shipment #{id}";
        }

    
        public static string FormatUserDisplay(User user) =>
            user == null ? "User not found" : $"User #{user.Id} | Login: {user.Login} | Role: {user.Role}";

        public static string FormatWarehouseDisplay(Warehouse warehouse) =>
            warehouse == null ? "Warehouse not found" : $"Warehouse #{warehouse.Id} | Name: {warehouse.Name} | Location: {warehouse.Location}";

        public static string FormatCategoryDisplay(Category category) =>
            category == null ? "Category not found" : $"Category #{category.Id} | Name: {category.Name}";

        public static string FormatSupplierDisplay(Supplier supplier) =>
            supplier == null ? "Supplier not found" : $"Supplier #{supplier.Id} | Name: {supplier.Name} | Contact: {supplier.Contact}";

        public static string FormatOrderDisplay(Order order) =>
            order == null ? "Order not found" : $"Order #{order.Id} | Date: {order.OrderDate:yyyy-MM-dd} | User ID: {order.UserId}";

        public static string FormatProductDisplay(Product product) =>
            product == null ? "Product not found" : $"Product #{product.Id} | {product.Name} | Price: {product.Price:C}";

        public static string FormatShipmentDisplay(Shipment shipment) =>
            shipment == null ? "Shipment not found" : $"Shipment #{shipment.Id} | Date: {shipment.ShipmentDate:yyyy-MM-dd} | Status: {shipment.Status}";
    }
}
