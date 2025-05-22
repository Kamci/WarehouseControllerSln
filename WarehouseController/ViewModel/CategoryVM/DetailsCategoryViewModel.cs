using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.Model;
using WarehouseController.View.CategoryView;
using WarehouseController.ViewModel.Abstract;

namespace WarehouseController.ViewModel.CategoryVM
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class DetailsCategoryViewModel : AItemDetailsViewModel<Category>

    {
        public DetailsCategoryViewModel() : base("Category Details")
        {
            try
            {
                // inicjalizacja
                Debug.WriteLine("DetailCategoryViewModel loaded");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ViewModel init error: {ex}");
                throw; // Możesz to potem usunąć
            }
        }

        private int id;
        private string name = string.Empty;

        public int Id { get => id; set { Debug.WriteLine($"Setting Id: {value}"); SetProperty(ref id, value); } }
        public string Name { get => name; set { Debug.WriteLine($"Setting Name: {value}"); SetProperty(ref name, value); } }

        public override async Task LoadItem(int id)
        {
            try
            {
                Debug.WriteLine($"LoadItem({id}) – start");
                var item = await DataStore.GetItemAsync(id);
                Debug.WriteLine($"LoadItem({id}) – result: {item?.Id} {item?.Name}");
                if (item != null)
                {
                    Id = item.Id;
                    Name = item.Name;
                    Debug.WriteLine("Nowa kategoria: " + item?.Name);
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
            Debug.WriteLine($"[NAVIGATE] Going to EditCategoryPage with ID: {Id}");

            await Shell.Current.GoToAsync($"{nameof(EditCategoryPage)}?{nameof(EditCategoryViewModel.ItemId)}={Id}");
        }
     
    }
}