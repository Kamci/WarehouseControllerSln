using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using WarehouseController.Model;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.Services.Abstract;

namespace WarehouseController.Services.DataStores
{
    public class ShipmentDataStore : AListDataStore<Shipment>
    {
        public ShipmentDataStore() : base() { }

        protected override string ControllerName => "Shipment";
        protected override int GetId(Shipment item) => item.Id;
    }
}