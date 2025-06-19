using Application.Domain;
using Vocabulary;

namespace Application.Ports;

public interface IProductPort {
    Task Save(ProductAggregate product);
    Task<ProductAggregate?> FindById(ProductId id);
    Task<IEnumerable<ProductAggregate>> FindAll();
    Task Update(ProductAggregate product);
}
