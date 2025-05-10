using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RestApiWarehouseController.Models;

public partial class Shipment
{
    [Key]
    public int Id { get; set; }

    public int? SupplierId { get; set; }

    public int? WarehouseId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ShipmentDate { get; set; }

    [StringLength(50)]
    public string Status { get; set; } = null!;

    [ForeignKey("SupplierId")]
    [InverseProperty("Shipments")]
    public virtual Supplier? Supplier { get; set; }

    [ForeignKey("WarehouseId")]
    [InverseProperty("Shipments")]
    public virtual Warehouse? Warehouse { get; set; }
}
