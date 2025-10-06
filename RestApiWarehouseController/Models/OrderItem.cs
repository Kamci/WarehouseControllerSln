using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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

    [JsonIgnore]               
    [ValidateNever]           
    [ForeignKey("OrderId")]
    public virtual Order? Order { get; set; }   

    [JsonIgnore]
    [ValidateNever]
    [ForeignKey("ProductId")]
    public virtual Product? Product { get; set; }
}
