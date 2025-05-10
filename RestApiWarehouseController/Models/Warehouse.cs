using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RestApiWarehouseController.Models;

public partial class Warehouse
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Name { get; set; } = null!;

    [StringLength(200)]
    public string Location { get; set; } = null!;

    [InverseProperty("Warehouse")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    [InverseProperty("Warehouse")]
    public virtual ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();
}
