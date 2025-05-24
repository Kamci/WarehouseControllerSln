using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

namespace RestApiWarehouseController.Models;

[Index("Login", Name = "UQ__Users__5E55825BA770CAEF", IsUnique = true)]
public partial class User
{
    [Key]
    public int Id { get; set; }

    [StringLength(100)]
    public string Login { get; set; } = null!;

    [MaxLength(256)]
    [JsonIgnore]
    public byte[]? PasswordHash { get; set; } = null!;

    [StringLength(50)]
    public string Role { get; set; } = null!;

    [InverseProperty("User")]
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();


    [NotMapped]
    public string? Password { get; set; }
}
