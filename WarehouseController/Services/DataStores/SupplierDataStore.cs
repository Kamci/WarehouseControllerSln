using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.Model;
using WarehouseController.Services.Abstract;

namespace WarehouseController.Services.DataStores
{
    public class SupplierDataStore : AListDataStore<Supplier>
    {
        public SupplierDataStore() : base() { }

        protected override string ControllerName => "Supplier";
        protected override int GetId(Supplier item) => item.Id;
    }
}