using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.ViewModel.Abstract;
using WarehouseController.Model;

namespace WarehouseController.ViewModel.WarehouseVM
{
    class EditWarehouseViewModel : AItemUpdateViewModel<Warehouse>
    {
        private int id;
        private string name = string.Empty;
        private string location = string.Empty;

        public EditWarehouseViewModel() : base("Edit Warehouse")
        {
            try
            {
                // inicjalizacja
                Debug.WriteLine("EditWarehouseViewModel loaded");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ViewModel init error: {ex}");
                throw; // Możesz to potem usunąć
            }
        }
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
                throw; // Możesz to potem usunąć
            }
        }

        public override Warehouse SetItem()
        => new()
        {
            Id = Id,
            Name = Name,
            Location = Location

        };

        public override bool ValidateSave()
        {
            return Id > 0
                   && !string.IsNullOrWhiteSpace(name)
                   && !string.IsNullOrWhiteSpace(location);
        }

    }

}