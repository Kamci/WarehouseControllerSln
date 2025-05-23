using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WarehouseController.Model;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.Services.Abstract;
using WarehouseController.DTO;

namespace WarehouseController.Services.DataStores
{
    public class ShipmentDataStore : AListDataStore<ShipmentDTO>
    {
        public ShipmentDataStore() : base() { }

        protected override string ControllerName => "Shipment";
        protected override int GetId(ShipmentDTO item) => item.Id;


    }
}