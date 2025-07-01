using Vocabulary;

namespace SockStoreApi.Response;

public record ProductResponse(int Id, string Name, string Category, decimal Price, int Stock)
{
    public static ProductResponse FromProduct(Product product)
    {
        return new ProductResponse(product.Id.Value, product.Name.Value, product.Category.Value, product.Price.Value, product.Stock.Value);
    }

    public override string ToString() => $"{Name} ({Category})";
}
