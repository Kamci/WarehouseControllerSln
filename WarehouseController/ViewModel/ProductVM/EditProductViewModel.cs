using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.DTO;
using WarehouseController.Model;
using WarehouseController.Services.Implementations;
using WarehouseController.Services.Interfaces;
using WarehouseController.ViewModel.Abstract;

namespace WarehouseController.ViewModel.ProductVM
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class EditProductViewModel : AItemUpdateViewModel<ProductDto>
    {
        private int id;
        private string name = string.Empty;
        private decimal price;
        private int stockQuantity;
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
        public int StockQuantity { get => stockQuantity; set => SetProperty(ref stockQuantity, value); }
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
                    StockQuantity = item.StockQuantity;
                    CategoryId = (int)item.CategoryId;
                    WarehouseId = (int)item.WarehouseId;
                    SupplierId = (int)item.SupplierId;

                    // Załaduj dane powiązane
                    await LoadDataAsync();

                    // Ustaw Selected... po załadowaniu list
                    SelectedCategory = Category.FirstOrDefault(c => c.Id == CategoryId);
                    SelectedWarehouse = Warehouses.FirstOrDefault(w => w.Id == WarehouseId);
                    SelectedSupplier = Suppliers.FirstOrDefault(s => s.Id == SupplierId);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ViewModel load error: {ex}");
                throw; // Możesz to potem usunąć
            }
        }

        public override ProductDto SetItem()
        => new()
        {
            Id = Id,
            Name = Name,
            Price = Price,
            StockQuantity = StockQuantity,
            CategoryId = CategoryId,
            WarehouseId = WarehouseId,
            SupplierId = SupplierId

        };

        public override bool ValidateSave()
        {
            return  !string.IsNullOrWhiteSpace(name)
                   && price > 0
                   && stockQuantity > 0;
        }

        #region Klucze obce
        private readonly ReferenceDataHelper _refHelper = new();
        public ObservableCollection<Category> Category { get; set; } = new();
        public ObservableCollection<Warehouse> Warehouses { get; set; } = new();
        public ObservableCollection<Supplier> Suppliers { get; set; } = new();
        ///-----------------------------wlasciwosci----------------------------------------------
        private Category selectedCategory;
        public Category SelectedCategory
        {
            get => selectedCategory;
            set
            {
                if(SetProperty(ref selectedCategory, value) && value != null)
                CategoryId = value.Id;
            }
        }

        private Warehouse selectedWarehouse;
        public Warehouse SelectedWarehouse
        {
            get => selectedWarehouse;
            set
            {
                if (SetProperty(ref selectedWarehouse, value) && value != null)
                    WarehouseId = value.Id;
            }
        }

        private Supplier selectedSupplier;
        public Supplier SelectedSupplier
        {
            get => selectedSupplier;
            set
            {
                if (SetProperty(ref selectedSupplier, value) && value != null)
                    SupplierId = value.Id;
            }
        }



        //metoda do inizjalizacji danych

        public async Task LoadDataAsync()
        {
            await _refHelper.LoadCategoriesAsync(Category);
            await _refHelper.LoadWarehousesAsync(Warehouses);
            await _refHelper.LoadSuppliersAsync(Suppliers);
        }
        #endregion

    }

}
