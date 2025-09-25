using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Entities;

public class ParameterEntity
{
    public int Id { get; set; }
    [StringLength(100)]
    public string Key { get; set; } = "";
    [StringLength(100)]
    public string Value { get; set; } = "";

    public override string ToString() => $"{Key}={Value}";
}
