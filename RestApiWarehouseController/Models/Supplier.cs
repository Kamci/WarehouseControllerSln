using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RestApiWarehouseController.Models;

public partial class Supplier
{
    [Key]
    public int Id { get; set; }

    [StringLength(200)]
    public string Name { get; set; } = null!;

    [StringLength(200)]
    public string Contact { get; set; } = null!;

    [InverseProperty("Supplier")]
    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    [InverseProperty("Supplier")]
    public virtual ICollection<Shipment> Shipments { get; set; } = new List<Shipment>();
}
