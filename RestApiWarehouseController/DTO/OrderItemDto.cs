﻿namespace RestApiWarehouseController.DTO
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; internal set; }
    }
}
