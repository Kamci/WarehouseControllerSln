﻿using System.Collections.ObjectModel;
using System.Diagnostics;
using WarehouseController.DTO;
using WarehouseController.Model;
using WarehouseController.Services.Implementations;
using WarehouseController.Services.Interfaces;
using WarehouseController.ViewModel.Abstract;

namespace WarehouseController.ViewModel.ShipmentVM
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public partial class EditShipmentViewModel : AItemUpdateViewModel<ShipmentDTO>
    {
        private int id;
        private int supplierId;
        private int warehouseId;
        private DateTime shipmentDate = DateTime.Now;
        private string status = string.Empty;
        public EditShipmentViewModel() : base("Add Shipment")
        { }


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

        public override ShipmentDTO SetItem()
            => new ShipmentDTO()
            {
                Id = Id,
                SupplierId = SupplierId,
                WarehouseId = WarehouseId,
                ShipmentDate = ShipmentDate,
                Status = Status

            };

        public override async Task LoadItem(int id)
        {
            try
            {
                var item = await DataStore.GetItemAsync(id);
                if (item != null)
                {
                    
                    await LoadDataAsync();
                    Id = item.Id;
                    SupplierId = (int)item.SupplierId;
                    WarehouseId = (int)item.WarehouseId;
                    ShipmentDate = (DateTime)item.ShipmentDate;
                    Status = item.Status;
                    SelectedStatus = Statuses.FirstOrDefault(s => s == item.Status) ?? Statuses.FirstOrDefault();
                    SelectedWarehouse = Warehouses.FirstOrDefault(w => w.Id == WarehouseId);
                    SelectedSupplier = Suppliers.FirstOrDefault(s => s.Id == SupplierId);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"ViewModel load error: {ex}");
                throw;
            }
        }


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
                Status = value; 
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
                {
                    SupplierId = value.Id;
                }
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
