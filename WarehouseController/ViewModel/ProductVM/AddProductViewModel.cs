using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.Model;
using WarehouseController.Services;
using WarehouseController.ViewModel.Abstract;

namespace WarehouseController.ViewModel.ProductVM
{
    public partial class AddProductViewModel : ANewItemViewModel<Product>
    {
        private int id;
        private string name = string.Empty;
        private decimal price;
        private int quantity;
        private int categoryId;
        private int warehouseId;
        private int supplierId;

        public AddProductViewModel() : base ("Add Product")
        {
            try
            {
                // inicjalizacja
                Debug.WriteLine("AddProductViewModel loaded");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ViewModel init error: {ex}");
                throw; // Możesz to potem usunąć
            }
        }

      
        public int Id { get => id; set => SetProperty(ref id, value); }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public decimal Price { get => price; set => SetProperty(ref price, value); }
        public int Quantity { get => quantity; set => SetProperty(ref quantity, value); }
        public int CategoryId { get => categoryId; set => SetProperty(ref categoryId, value); }
        public int WarehouseId { get => warehouseId; set => SetProperty(ref warehouseId, value); }
        public int SupplierId { get => supplierId; set => SetProperty(ref supplierId, value); }



        public override bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(name)
                   && price > 0
                   && quantity > 0;
        }

        public override Product SetItem()
            => new Product()
            {
                Id = Id,
                Name = Name,
                Price = Price,
                Quantity = Quantity,
                CategoryId = CategoryId,
                WarehouseId = WarehouseId,
                SupplierId = SupplierId
            };
    }
}
