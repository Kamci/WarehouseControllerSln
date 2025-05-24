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
        { }

        public int Id
        {
            get => id;
            set
            {
                SetProperty(ref id, value);
            }
        }
        public string Name
        {
            get => name;
            set
            {
                SetProperty(ref name, value);
            }
        }

        public override async Task LoadItem(int id)
        {
            try
            {
                var item = await DataStore.GetItemAsync(id);
                if (item != null)
                {
                    Id = item.Id;
                    Name = item.Name;
                }
                else
                {
                    Debug.WriteLine($"No item found with id: {id}");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ViewModel load error: {ex}");
                throw; 
            }
        }

        public override bool ValidateSave()
        {
            bool result = !string.IsNullOrWhiteSpace(name) && id > 0;
            return result;
        }

        public override Category SetItem()
        {
            return new Category
            {
                Id = Id,
                Name = Name
            };
        }
   
    }
}
