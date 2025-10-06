using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.DTO;
using WarehouseController.Model;

namespace WarehouseController.Services.Interfaces
{
    public interface IShipmentService
    {
        Task<IEnumerable<ShipmentDTO>> GetAllAsync();
        Task<ShipmentDTO> GetByIdAsync(int id);
        Task<List<RecentShipmentDto>> GetRecentShipmentsViewModelAsync(int warehouseId);
    }
}
