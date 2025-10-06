namespace RestApiWarehouseController.DTO
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }

        public string WarehouseName { get; set; } = "";
        public string SupplierName { get; set; } = "";
        public string CategoryName { get; set; } = "";

        public int? WarehouseId { get; set; }
        public int? SupplierId { get; set; }
        public int? CategoryId { get; set; }
    }
}
