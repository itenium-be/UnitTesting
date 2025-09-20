namespace Migration.Legacy;

public class LegacyProduct
{
    public Guid Id { get; set; }
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
    public string Color { get; set; } = "";
    public SockSize Size { get; set; }
    public bool Active { get; set; }
    public string CreatedOn { get; set; } = "";
    public string UpdatedOn { get; set; } = "";
    public string Material { get; set; } = "";
    public string Description { get; set; } = "";
    public string Brand { get; set; } = "";
}

public enum SockSize
{
    XSS,
    XS,
    S,
    M,
    L,
    XL,
    XLL
}