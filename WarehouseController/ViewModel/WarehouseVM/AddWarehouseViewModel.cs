using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.Model;
using WarehouseController.ViewModel.Abstract;

namespace WarehouseController.ViewModel.WarehouseVM
{
    class AddWarehouseViewModel : ANewItemViewModel<Warehouse>
    {
        private int id;
        private string name = string.Empty;
        private string location = string.Empty;

        public AddWarehouseViewModel() : base("Add Warehouse")
        {
            
        }


        public int Id { get => id; set => SetProperty(ref id, value); }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Location { get => location; set => SetProperty(ref location, value); }



        public override bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(name)
                   && !string.IsNullOrWhiteSpace(location);
        }

        public override Warehouse SetItem()
            => new Warehouse()
            {
                Name = Name,
                Location = Location
            };
    }
}

