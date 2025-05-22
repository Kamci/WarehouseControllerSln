using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using WarehouseController.Model;
using WarehouseController.View.CategoryView;
using WarehouseController.ViewModel.Abstract;

namespace WarehouseController.ViewModel.CategoryVM
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class EditCategoryViewModel : AItemUpdateViewModel<Category>
    {
        private int id;
        private string name = string.Empty;

        public EditCategoryViewModel() : base("Edit Category")
        {
            try
            {
                // inicjalizacja
                Debug.WriteLine("EditCategoryViewModel loaded");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ViewModel init error: {ex}");
                throw; // Możesz to potem usunąć
            }
        }

        public int Id
        {
            get => id;
            set
            {
                Debug.WriteLine($"[EditCategoryViewModel] Setting Id: {value}");
                SetProperty(ref id, value);
            }
        }
        public string Name
        {
            get => name;
            set
            {
                Debug.WriteLine($"[EditCategoryViewModel] Setting Name: {value}");
                SetProperty(ref name, value);
            }
        }

        public override async Task LoadItem(int id)
        {
            try
            {
                Debug.WriteLine($"[EditCategoryViewModel] Loading item with id: {id}");
                var item = await DataStore.GetItemAsync(id);
                if (item != null)
                {
                    Debug.WriteLine($"[EditCategoryViewModel] Loaded category: {item.Name} (Id: {item.Id})");
                    Id = item.Id;
                    Name = item.Name;
                }
                else
                {
                    Debug.WriteLine($"[EditCategoryViewModel] No item found with id: {id}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ViewModel load error: {ex}");
                throw; // Możesz to potem usunąć
            }
        }

        public override bool ValidateSave()
        {
            bool result = !string.IsNullOrWhiteSpace(name) && id > 0;
            Debug.WriteLine($"[EditCategoryViewModel] ValidateSave: {result} (Id: {id}, Name: {name})");
            return result;
        }

        public override Category SetItem()
        {
            Debug.WriteLine($"[EditCategoryViewModel] SetItem called. Id: {Id}, Name: {Name}");
            return new Category
            {
                Id = Id,
                Name = Name
            };
        }
   
    }
}
