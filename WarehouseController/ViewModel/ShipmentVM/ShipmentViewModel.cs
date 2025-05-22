using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.Model;
using WarehouseController.View.ShipmentView;
using WarehouseController.ViewModel.Abstract;

namespace WarehouseController.ViewModel.ShipmentVM
{
    public partial class ShipmentViewModel : AItemListViewModel<Shipment>
    {
        private Shipment? _selectedShipment;
        public Command RefreshCommand { get; }
        public Command<Shipment> AboutCommand { get; }
        public ShipmentViewModel() : base("Shipment")
        {
            RefreshCommand = LoadItemsCommand;
            AboutCommand = new Command<Shipment>(OnShipmentSelected);
        }
        public Shipment? SelectedShipment
        {
            get => _selectedShipment;
            set => SetProperty(ref _selectedShipment, value);
        }
        async void OnShipmentSelected(Shipment shipment)
        {
            await GoToDetailsPage(shipment);
        }
        public override async Task GoToAddPage()
        {
            // Załaduj nową stronę przez Shell
            await Shell.Current.GoToAsync(nameof(AddShipmentPage));
        }
        public override async Task GoToDetailsPage(Shipment shipment)
        {
            if (shipment == null)
                return;
            SelectedShipment = shipment;
            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(DetailsShipmentPage)}?{nameof(DetailsShipmentViewModel.ItemId)}={shipment.Id}");
        }
    }
}
