using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarehouseController.DTO;
using WarehouseController.Model;
using WarehouseController.Services.DataStores;
using WarehouseController.Services.Interfaces;

namespace WarehouseController.Services.Implementations
{
    public class ShipmentService : IShipmentService
    {
        private readonly ShipmentDataStore _dataStore;

        public ShipmentService()
        {
            _dataStore = new ShipmentDataStore();
        }

        public Task<IEnumerable<ShipmentDTO>> GetAllAsync() => _dataStore.GetItemsAsync();
        public Task<ShipmentDTO> GetByIdAsync(int id) => _dataStore.GetItemAsync(id);

        public async Task<List<RecentShipmentDto>> GetRecentShipmentsViewModelAsync(int warehouseId)
        {
            var allShipments = await GetAllAsync();
            var supplierService = new SupplierService();
            var warehouseService = new WarehouseService();

            var allSuppliers = await supplierService.GetAllAsync();
            var allWarehouses = await warehouseService.GetAllAsync();

            var recentShipments = allShipments
                .Where(s => s.WarehouseId == warehouseId)
                .OrderByDescending(s => s.ShipmentDate)
                .Take(5)
                .Select(s =>
                {
                    var supplierName = allSuppliers.FirstOrDefault(x => x.Id == s.SupplierId)?.Name ?? "Unknown";
                    var warehouseName = allWarehouses.FirstOrDefault(x => x.Id == s.WarehouseId)?.Name ?? "Unknown";

                    return new RecentShipmentDto(s, supplierName, warehouseName);
                })
                .ToList();

            return recentShipments;
        }
    }
}
