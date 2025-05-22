using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.Model;
using WarehouseController.View.ShipmentView;
using WarehouseController.ViewModel.Abstract;

namespace WarehouseController.ViewModel.ShipmentVM
{
    public partial class DetailsShipmentViewModel : AItemDetailsViewModel<Shipment>

    {
        public DetailsShipmentViewModel() : base("Shipment Details")
        {
            try
            {
                // inicjalizacja
                Debug.WriteLine("DetailShipmentViewModel loaded");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ViewModel init error: {ex}");
                throw; // Możesz to potem usunąć
            }
        }

        private int id;

        private int supplierId;
        private int warehouseId;
        private DateTime shipmentDate;
        private string status = string.Empty;

        public int Id { get => id; set => SetProperty(ref id, value); }
        public int SupplierId { get => supplierId; set => SetProperty(ref supplierId, value); }
        public int WarehouseId { get => warehouseId; set => SetProperty(ref warehouseId, value); }
        public DateTime ShipmentDate { get => shipmentDate; set => SetProperty(ref shipmentDate, value); }
        public string Status { get => status; set => SetProperty(ref status, value); }

        public override async Task LoadItem(int id)
        {
            try
            {
                var item = await DataStore.GetItemAsync(id);
                if (item != null)
                {
                    Id = item.Id;
                    SupplierId = item.SupplierId;
                    WarehouseId = item.WarehouseId;
                    ShipmentDate = item.ShipmentDate;
                    Status = item.Status;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ViewModel load error: {ex}");
                throw; // Możesz to potem usunąć
            }
        }

        protected override async Task GoToUpdatePage()
        {
            Debug.WriteLine($"[NAVIGATE] Going to EditShipmentPage with ID: {Id}");

            await Shell.Current.GoToAsync($"{nameof(EditShipmentPage)}?{nameof(EditShipmentViewModel.ItemId)}={Id}");
        }

      
    }
}
