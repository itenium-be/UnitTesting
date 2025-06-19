using Application.Domain;
using Vocabulary;

namespace Application.Ports;

public interface IProductPort {
    void Save(ProductAggregate product);
    ProductAggregate? FindById(ProductId id);
    IEnumerable<ProductAggregate> FindAll();
    void Update(ProductAggregate product);
}
