using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.DTO;
using WarehouseController.Model;
using WarehouseController.View.ShipmentView;
using WarehouseController.ViewModel.Abstract;

namespace WarehouseController.ViewModel.ShipmentVM
{
    public partial class ShipmentViewModel : AItemListViewModel<ShipmentDTO>
    {
        private ShipmentDTO? _selectedShipment;
        public Command RefreshCommand { get; }
        public Command<ShipmentDTO> AboutCommand { get; }
        public ShipmentViewModel() : base("Shipment")
        {
            RefreshCommand = LoadItemsCommand;
            AboutCommand = new Command<ShipmentDTO>(OnShipmentSelected);
        }
        public ShipmentDTO? SelectedShipment
        {
            get => _selectedShipment;
            set => SetProperty(ref _selectedShipment, value);
        }
        async void OnShipmentSelected(ShipmentDTO shipment)
        {
            await GoToDetailsPage(shipment);
        }
        public override async Task GoToAddPage()
        {
          
            await Shell.Current.GoToAsync(nameof(AddShipmentPage));
        }
        public override async Task GoToDetailsPage(ShipmentDTO shipment)
        {
            if (shipment == null)
                return;
            SelectedShipment = shipment;
            await Shell.Current.GoToAsync($"{nameof(DetailsShipmentPage)}?{nameof(DetailsShipmentViewModel.ItemId)}={shipment.Id}");
        }
    }
}
