using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.Model;
using WarehouseController.Services.Implementations;
using WarehouseController.Services.Interfaces;
using WarehouseController.ViewModel.Abstract;

namespace WarehouseController.ViewModel.ShipmentVM
{
    public partial class AddShipmentViewModel : ANewItemViewModel<Shipment>
    {
        private int id;
        private int supplierId;
        private int warehouseId;
        private DateTime shipmentDate = DateTime.Now;
        private string status = string.Empty;
        public AddShipmentViewModel() : base("Add Shipment")
        {
            try
            {
                // inicjalizacja
                Debug.WriteLine("AddShipmentViewModel loaded");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ViewModel init error: {ex}");
                throw; // Możesz to potem usunąć
            }
        }


        public int Id { get => id; set => SetProperty(ref id, value); }
        public int SupplierId { get => supplierId; set => SetProperty(ref supplierId, value); }
        public int WarehouseId { get => warehouseId; set => SetProperty(ref warehouseId, value); }
        public DateTime ShipmentDate { get => shipmentDate; set => SetProperty(ref shipmentDate, value); }
        public string Status { get => status; set => SetProperty(ref status, value); }



        public override bool ValidateSave()
        {
            return !string.IsNullOrWhiteSpace(Status)
                 && SupplierId > 0
                 && WarehouseId > 0
                 && ShipmentDate != default
                 && ShipmentDate >= DateTime.Now.AddDays(-30)
                 && ShipmentDate <= DateTime.Now.AddDays(7);

        }

        public override Shipment SetItem()
            => new Shipment()
            {
                SupplierId = SupplierId,
                WarehouseId = WarehouseId,
                ShipmentDate = ShipmentDate,
                Status = Status

            };

        #region Comboboxy

        private readonly ReferenceDataHelper _refHelper = new();
        public ObservableCollection<string> Statuses { get; set; } =
                new ObservableCollection<string> { "Pending", "Delivered", "Cancelled" };

        private string selectedStatus;
        public string SelectedStatus
        {
            get => selectedStatus;
            set
            {
                SetProperty(ref selectedStatus, value);
                Status = value; // zakładam, że masz już właściwość Status
            }
        }

        public ObservableCollection<Warehouse> Warehouses { get; set; } = new();

        public ObservableCollection<Supplier> Suppliers { get; set; } = new();



        private Warehouse selectedWarehouse;
        public Warehouse SelectedWarehouse
        {
            get => selectedWarehouse;
            set
            {
                if (SetProperty(ref selectedWarehouse, value) && value != null)
                    WarehouseId = value.Id;
            }
        }

        private Supplier selectedSupplier;
        public Supplier SelectedSupplier
        {
            get => selectedSupplier;
            set
            {
                if (SetProperty(ref selectedSupplier, value) && value != null)
                    SupplierId = value.Id;
            }
        }

        public async Task LoadDataAsync()
        {
            await _refHelper.LoadWarehousesAsync(Warehouses);
            await _refHelper.LoadSuppliersAsync(Suppliers);
        }
        #endregion
    }
}
