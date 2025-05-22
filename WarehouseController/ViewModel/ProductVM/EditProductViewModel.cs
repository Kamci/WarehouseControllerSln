using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.Model;
using WarehouseController.ViewModel.Abstract;

namespace WarehouseController.ViewModel.ProductVM
{
    public partial class EditProductViewModel : AItemUpdateViewModel<Product>
    {
        private int id;
        private string name = string.Empty;
        private decimal price;
        private int quantity;
        private int categoryId;
        private int warehouseId;
        private int supplierId;

        public EditProductViewModel() : base("Edit Product")
        {
            try
            {
                // inicjalizacja
                Debug.WriteLine("EditProductViewModel loaded");
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
                    Quantity = item.Quantity;
                    CategoryId = item.CategoryId;
                    WarehouseId = item.WarehouseId;
                    SupplierId = item.SupplierId;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ViewModel load error: {ex}");
                throw; // Możesz to potem usunąć
            }
        }

        public override Product SetItem()
        => new()
        {
            Id = Id,
            Name = Name,
            Price = Price,
            Quantity = Quantity,
            CategoryId = CategoryId,
            WarehouseId = WarehouseId,
            SupplierId = SupplierId

        };

        public override bool ValidateSave()
        {
            return Id > 0
                   && !string.IsNullOrWhiteSpace(name)
                   && price > 0
                   && quantity > 0;
        }

    }

}
