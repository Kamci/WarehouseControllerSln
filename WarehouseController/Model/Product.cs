namespace WarehouseController.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public int WarehouseId { get; set; }
        public int CategoryId { get; set; }

        // (opcjonalnie) nawigacyjne
        public Warehouse Warehouse { get; set; }
        public Category Category { get; set; }
    }

}
