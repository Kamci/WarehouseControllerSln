using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.DTO;
using WarehouseController.Model;

namespace WarehouseController.Services.Implementations
{
    public class ReferenceDataHelper
    {
        public readonly WarehouseService _warehouseService = new();
        private readonly SupplierService _supplierService = new();
        private readonly CategoryService _categoryService = new();
        private readonly OrderService _orderService = new();
        private readonly ProductService _productService = new();
        private readonly ShipmentService _shipmentService = new();
        private readonly UserService _userService = new();

        public async Task LoadWarehousesAsync(ObservableCollection<Warehouse> target)
        {
            var list = await _warehouseService.GetAllAsync();
            target.Clear();
            foreach (var item in list)
                target.Add(item);
        }

        public async Task LoadSuppliersAsync(ObservableCollection<Supplier> target)
        {
            var list = await _supplierService.GetAllAsync();
            target.Clear();
            foreach (var item in list)
                target.Add(item);
        }

        public async Task LoadCategoriesAsync(ObservableCollection<Category> target)
        {
            var list = await _categoryService.GetAllAsync();
            target.Clear();
            foreach (var item in list)
                target.Add(item);
        }

        public async Task LoadOrdersAsync(ObservableCollection<OrderDto> target)
        {
            var list = await _orderService.GetAllAsync();
            target.Clear();
            foreach (var item in list)
                target.Add(item);
        }

        public async Task LoadProductsAsync(ObservableCollection<ProductDto> target)
        {
            var list = await _productService.GetAllAsync();
            target.Clear();
            foreach (var item in list)
                target.Add(item);
        }

        public async Task LoadShipmentsAsync(ObservableCollection<ShipmentDTO> target)
        {
            var list = await _shipmentService.GetAllAsync();
            target.Clear();
            foreach (var item in list)
                target.Add(item);
        }

        public async Task LoadUsersAsync(ObservableCollection<User> target)
        {
            var list = await _userService.GetAllAsync();
            target.Clear();
            foreach (var item in list)
                target.Add(item);
        }

       
    }
}
