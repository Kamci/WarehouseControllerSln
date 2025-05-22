using System.Diagnostics;
using WarehouseController.Model;
using WarehouseController.ViewModel.Abstract;

namespace WarehouseController.ViewModel.ShipmentVM
{
    public partial class EditShipmentViewModel : AItemUpdateViewModel<Shipment>
    {
        private int id;
        private int supplierId;
        private int warehouseId;
        private DateTime shipmentDate = DateTime.Now;
        private string status = string.Empty;
        public EditShipmentViewModel() : base("Add Shipment")
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
                && Id > 0
                   && SupplierId > 0
                   && WarehouseId > 0
                     && ShipmentDate != default(DateTime)
                     && ShipmentDate <= DateTime.Now
                     && ShipmentDate >= DateTime.Now.AddDays(-30)
                        && ShipmentDate <= DateTime.Now.AddDays(30);

        }

        public override Shipment SetItem()
            => new Shipment()
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


    }
}
