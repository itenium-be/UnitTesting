using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class Product
{
    public int Id { get; set; }
    [StringLength(100)]
    public string Name { get; set; } = "";
    [StringLength(100)]
    public string Category { get; set; } = "";
    public decimal Price { get; set; }
    public int Stock { get; set; }
}
