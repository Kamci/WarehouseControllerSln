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

    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class EditSupplierViewModel : AItemUpdateViewModel<Supplier>

    {
        private int id;
        private string name = string.Empty;
        private string contact = string.Empty;
        public EditSupplierViewModel() : base("Edit Supplier")
        { }

        public int Id { get => id; set => SetProperty(ref id, value); }
        public string Name { get => name; set => SetProperty(ref name, value); }
        public string Contact { get => contact; set => SetProperty(ref contact, value); }
        public override async Task LoadItem(int id)
        {
            try
            {
                var item = await DataStore.GetItemAsync(id);
                if (item != null)
                {
                    Id = item.Id;
                    Name = item.Name;
                    Contact = item.Contact;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ViewModel load error: {ex}");
            }
        }


        public override Supplier SetItem() =>
           new()
           {
               Id = Id,
               Name = Name,
               Contact = Contact
           };

        public override bool ValidateSave()
        {
            return  !string.IsNullOrWhiteSpace(name)
                    && !string.IsNullOrWhiteSpace(contact);
        }

     
    }
}
