using Vocabulary;

namespace Application.Domain;

public class ProductAggregate(ProductId id, Name name, Category category, Price price, Stock stock)
{
    public ProductId Id { get; } = id;
    public Name Name { get; private set; } = name;
    public Category Category { get; private set; } = category;
    public Price Price { get; private set; } = price;
    public Stock Stock { get; private set; } = stock;

    public void UpdateStock(Stock newStock)
    {
        Stock = newStock;
    }

    public void UpdateProduct(Name name, Category category, Price price)
    {
        Name = name;
        Category = category;
        Price = price;
    }

    public Product ToProduct()
    {
        return new Product(
            Id,
            Name,
            Category,
            Price,
            Stock
        );
    }
}
