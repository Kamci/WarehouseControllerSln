using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.DTO;
using WarehouseController.Model;
using WarehouseController.Services;
using WarehouseController.Services.Implementations;
using WarehouseController.Services.Interfaces;
using WarehouseController.ViewModel.Abstract;

namespace WarehouseController.ViewModel.ProductVM
{
    public partial class AddProductViewModel : ANewItemViewModel<ProductDto>
    {
        private readonly ReferenceDataHelper _refHelper = new();
        private int id;
        private string name = string.Empty;
        private decimal price;
        private int stockQuantity;
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
        public int StockQuantity { get => stockQuantity; set => SetProperty(ref stockQuantity, value); }
        public int CategoryId { get => categoryId; set => SetProperty(ref categoryId, value); }
        public int WarehouseId { get => warehouseId; set => SetProperty(ref warehouseId, value); }
        public int SupplierId { get => supplierId; set => SetProperty(ref supplierId, value); }



        public override bool ValidateSave()
        {
            bool isValid = !string.IsNullOrWhiteSpace(Name)
                   && Price > 0
                   && StockQuantity > 0
                   && SelectedCategory != null
                   && SelectedSupplier != null
                   && SelectedWarehouse != null;

            Debug.WriteLine($"[ValidateSave] Name: {Name}, Price: {Price}, Quantity: {StockQuantity}, " +
                            $"Category: {SelectedCategory?.Name}, Supplier: {SelectedSupplier?.Name}, " +
                            $"Warehouse: {SelectedWarehouse?.Name}, IsValid: {isValid}");

            return isValid;
        }

        public override ProductDto SetItem()
        {
            if (!ValidateSave())
                throw new InvalidOperationException("Invalid product data");
            var product = new ProductDto
            {
                Name = Name,
                Price = Price,
                StockQuantity = StockQuantity,
                CategoryId = CategoryId,
                WarehouseId = WarehouseId,
                SupplierId = SupplierId
            };

            Debug.WriteLine("[SetItem] Creating Product:");
            Debug.WriteLine($"Name: {product.Name}");
            Debug.WriteLine($"Price: {product.Price}");
            Debug.WriteLine($"Quantity: {product.StockQuantity}");

            return product;
        }

        #region Klucze obce

   
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
                SetProperty(ref selectedCategory, value);
                CategoryId = selectedCategory?.Id ?? 0;
            }
        }

        private Warehouse selectedWarehouse;
        public Warehouse SelectedWarehouse
        {
            get => selectedWarehouse;
            set
            {
                SetProperty(ref selectedWarehouse, value);
                WarehouseId = selectedWarehouse?.Id ?? 0;
            }
        }

        private Supplier selectedSupplier;
        public Supplier SelectedSupplier
        {
            get => selectedSupplier;
            set
            {
                SetProperty(ref selectedSupplier, value);
                SupplierId = selectedSupplier?.Id ?? 0;
            }
        }



        //metoda do inizjalizacji danych

        public async Task LoadDataAsync()
        {
            ResetFields();
            await _refHelper.LoadCategoriesAsync(Category);
            await _refHelper.LoadWarehousesAsync(Warehouses);
            await _refHelper.LoadSuppliersAsync(Suppliers);
        }

        public void ResetFields()
        {
            Name = string.Empty;
            Price = 0;
            StockQuantity = 0;
            SelectedCategory = null;
            SelectedWarehouse = null;
            SelectedSupplier = null;
        }
        #endregion
    }
}
