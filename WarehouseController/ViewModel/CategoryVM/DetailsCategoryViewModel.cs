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
        { }

        private int id;
        private string name = string.Empty;

        public int Id { get => id; set { SetProperty(ref id, value); } }
        public string Name { get => name; set {  SetProperty(ref name, value); } }

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
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ViewModel load error: {ex}");
                throw;
            }
        }

        protected override async Task GoToUpdatePage()
        {
            await Shell.Current.GoToAsync($"{nameof(EditCategoryPage)}?{nameof(EditCategoryViewModel.ItemId)}={Id}");
        }
     
    }
}