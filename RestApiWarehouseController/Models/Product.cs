using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RestApiWarehouseController.Models;

public partial class Product
{
    [Key]
    public int Id { get; set; }

    [StringLength(200)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "decimal(10, 2)")]
    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public int? WarehouseId { get; set; }

    public int? CategoryId { get; set; }

    public int? SupplierId { get; set; }

    [ForeignKey("CategoryId")]
    [InverseProperty("Products")]
    public virtual Category? Category { get; set; }

    [InverseProperty("Product")]
    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    [ForeignKey("SupplierId")]
    [InverseProperty("Products")]
    public virtual Supplier? Supplier { get; set; }

    [ForeignKey("WarehouseId")]
    [InverseProperty("Products")]
    public virtual Warehouse? Warehouse { get; set; }
}
