using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.Model;
using WarehouseController.ViewModel.Abstract;

namespace WarehouseController.ViewModel.SupplierVM
{
    public class AddSupplierViewModel : ANewItemViewModel<Supplier>
    {
        private int id;
        private string name = string.Empty;
        private string contact = string.Empty;
        public AddSupplierViewModel() : base("Add Supplier")
        { }

        public int Id { get => id; set => SetProperty(ref id, value); }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Contact { get => contact; set => SetProperty(ref contact, value); }
        public override bool ValidateSave()
        {
            return  !string.IsNullOrWhiteSpace(name)
                   && !string.IsNullOrWhiteSpace(contact);
        }
        public override Supplier SetItem()
            => new Supplier()
            {
                Name = Name,
                Contact = Contact
            };

       
    }
}
