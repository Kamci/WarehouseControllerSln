using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.Model;
using WarehouseController.View.SupplierView;
using WarehouseController.ViewModel.Abstract;

namespace WarehouseController.ViewModel.SupplierVM
{
    public class DetailSupplierViewModel : AItemDetailsViewModel<Supplier>
    {
        public DetailSupplierViewModel() : base("Supplier Details")
        {  }

        private int id;
        private string name = string.Empty;
        private string contact = string.Empty;

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
                throw;
            }
        }

        protected override async Task GoToUpdatePage()
        {

            await Shell.Current.GoToAsync($"{nameof(EditSupplierPage)}?{nameof(EditSupplierViewModel.ItemId)}={Id}");
        }

  
    }
}
