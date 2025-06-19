using Application.Ports;
using Vocabulary;

namespace Application.Query;

public interface IProductQuery {
    Product? FindById(ProductId id);
    IEnumerable<Product> FindAll();
}

public class ProductQuery(IProductPort productPort) : IProductQuery {
    public Product? FindById(ProductId id) =>
        productPort.FindById(id)?.ToProduct();

    public IEnumerable<Product> FindAll() =>
        productPort.FindAll().Select(aggregate => aggregate.ToProduct());
}
