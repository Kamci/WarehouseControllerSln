using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.Model;
using WarehouseController.View.WarehouseView;
using WarehouseController.ViewModel.Abstract;

namespace WarehouseController.ViewModel.WarehouseVM
{
    class DetailWarehouseViewModel : AItemDetailsViewModel<Warehouse>

    {
        public DetailWarehouseViewModel() : base("Warehouse Details")
        {
           
        }

        private int id;
        private string name = string.Empty;
        private string location = string.Empty;

        public int Id { get => id; set => SetProperty(ref id, value); }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Location { get => location; set => SetProperty(ref location, value); }
        public override async Task LoadItem(int id)
        {
            try
            {
                var item = await DataStore.GetItemAsync(id);
                if (item != null)
                {
                    Id = item.Id;
                    Name = item.Name;
                    Location = item.Location;
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
            

            await Shell.Current.GoToAsync($"{nameof(EditWarehousePage)}?{nameof(EditWarehouseViewModel.ItemId)}={Id}");
        }
    }
}
