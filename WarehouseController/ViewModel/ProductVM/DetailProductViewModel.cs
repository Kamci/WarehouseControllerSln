using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WarehouseController.DTO;
using WarehouseController.Model;
using WarehouseController.View.ProductView;
using WarehouseController.ViewModel.Abstract;

namespace WarehouseController.ViewModel.ProductVM
{
    public class DetailProductViewModel : AItemDetailsViewModel<ProductDto>

    {
        public DetailProductViewModel() : base("Product Details")
        {   }

        private int id;
        private string name = string.Empty;
        private decimal price;
        private int stockQuantity;
        private string categoryName = string.Empty;
        private string warehouseName = string.Empty;
        private string supplierName = string.Empty;
        private int? categoryId;
        private int? warehouseId;
        private int? supplierId;

        public int Id { get => id; set => SetProperty(ref id, value); }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public decimal Price { get => price; set => SetProperty(ref price, value); }
        public int StockQuantity { get => stockQuantity; set => SetProperty(ref stockQuantity, value); }

        public int? CategoryId { get => categoryId; set => SetProperty(ref categoryId, value); }
        public int? WarehouseId { get => warehouseId; set => SetProperty(ref warehouseId, value); }
        public int? SupplierId { get => supplierId; set => SetProperty(ref supplierId, value); }
         
        public string CategoryName { get => categoryName; set => SetProperty(ref categoryName, value); }
        public string WarehouseName { get => warehouseName; set => SetProperty(ref warehouseName, value); }
        public string SupplierName { get => supplierName; set => SetProperty(ref supplierName, value); }
        public override async Task LoadItem(int id)
        {
            try
            {
                var item = await DataStore.GetItemAsync(id);
                if (item != null)
                {
                    Id = item.Id;
                    Name = item.Name;
                    Price = item.Price;
                    StockQuantity = item.StockQuantity;
                    CategoryId = item.CategoryId;
                    WarehouseId = item.WarehouseId;
                    SupplierId = item.SupplierId;

                    CategoryName = item.CategoryName;
                    WarehouseName = item.WarehouseName;
                    SupplierName = item.SupplierName;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ViewModel load error: {ex}");
                throw;
            }
        }

        protected override async Task GoToUpdatePage()
        {
             await Shell.Current.GoToAsync($"{nameof(EditProductPage)}?{nameof(EditProductViewModel.ItemId)}={Id}");
        }

    }
}
