using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.Model;

namespace WarehouseController.Services.Interfaces
{
    public interface IShipmentService
    {
        Task<IEnumerable<Shipment>> GetAllAsync();
        Task<Shipment> GetByIdAsync(int id);
    }
}
