using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RestApiWarehouseController.Models;

[Index("OrderId", "ProductId", Name = "UQ_OrderItem", IsUnique = true)]
public partial class OrderItem
{
    [Key]
    public int Id { get; set; }

    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    [ForeignKey("OrderId")]
    [InverseProperty("OrderItems")]
    public virtual Order Order { get; set; } = null!;

    [ForeignKey("ProductId")]
    [InverseProperty("OrderItems")]
    public virtual Product Product { get; set; } = null!;
}
