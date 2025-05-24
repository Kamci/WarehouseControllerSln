using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.DTO;
using WarehouseController.Model;
using WarehouseController.Services.Implementations;
using WarehouseController.View.ShipmentView;
using WarehouseController.ViewModel.Abstract;

namespace WarehouseController.ViewModel.ShipmentVM
{
    public partial class DetailsShipmentViewModel : AItemDetailsViewModel<ShipmentDTO>

    {
        public DetailsShipmentViewModel() : base("Shipment Details")
        { }

        private int id;

        private int supplierId;
        private string suplierName;
        private int warehouseId;
        private string warehouseName;
        private DateTime shipmentDate;
        private string status = string.Empty;

        public int Id { get => id; set => SetProperty(ref id, value); }
        public int SupplierId { get => supplierId; set => SetProperty(ref supplierId, value); }
        public string SupplierName { get => suplierName; set => SetProperty(ref suplierName, value); }
        public int WarehouseId { get => warehouseId; set => SetProperty(ref warehouseId, value); }
        public string WarehouseName { get => warehouseName; set => SetProperty(ref warehouseName, value); }
        public DateTime ShipmentDate
        {
            get => shipmentDate;
            set
            {
                SetProperty(ref shipmentDate, value);
            }
        }
        public string Status { get => status; set => SetProperty(ref status, value); }

        public override async Task LoadItem(int id)
        {
            try
            {
                var item = await DataStore.GetItemAsync(id);
                if (item != null)
                {
                    Id = item.Id;
                    SupplierId = (int)item.SupplierId;
                    WarehouseId = (int)item.WarehouseId;
                    ShipmentDate = (DateTime)item.ShipmentDate;
                    Status = item.Status;
                    SupplierName = item.SupplierName;
                    WarehouseName = item.WarehouseName;
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
            await Shell.Current.GoToAsync($"{nameof(EditShipmentPage)}?{nameof(EditShipmentViewModel.ItemId)}={Id}");
        }

      
    }
}
