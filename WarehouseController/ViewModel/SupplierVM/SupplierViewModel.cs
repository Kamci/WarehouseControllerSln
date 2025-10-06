using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.Model;
using WarehouseController.View.SupplierView;
using WarehouseController.ViewModel.Abstract;

namespace WarehouseController.ViewModel.SupplierVM
{
    public class SupplierViewModel : AItemListViewModel<Supplier>
    {
        private Supplier? _selectedSupplier;
        public Command RefreshCommand { get; }
        public Command<Supplier> AboutCommand { get; }
        public SupplierViewModel() : base("Supplier")
        {
            RefreshCommand = LoadItemsCommand;
            AboutCommand = new Command<Supplier>(OnSupplierSelected);
        }



        public Supplier? SelectedSupplier
        {
            get => _selectedSupplier;
            set => SetProperty(ref _selectedSupplier, value);
        }
        async void OnSupplierSelected(Supplier supplier)
        {
            await GoToDetailsPage(supplier);
        }

        public override async Task GoToAddPage()
        {
          
            await Shell.Current.GoToAsync(nameof(AddSupplierPage));
        }

        public override async Task GoToDetailsPage(Supplier supplier)
        {
            if (supplier == null)
                return;
            SelectedSupplier = supplier;
           
            await Shell.Current.GoToAsync($"{nameof(DetailsSupplierPage)}?{nameof(DetailSupplierViewModel.ItemId)}={supplier.Id}");
        }
    }
}
