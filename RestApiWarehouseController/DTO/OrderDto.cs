namespace RestApiWarehouseController.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public string UserLogin { get; set; }
        public int? UserId { get; set; }
        public List<OrderItemDto> OrderItems { get; set; }
    }
}
