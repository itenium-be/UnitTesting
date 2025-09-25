using Application.Domain;
using Vocabulary;

namespace Application.Ports;

public interface IProductPort
{
    Task Save(ProductAggregate product);
    Task<ProductAggregate?> FindById(ProductId id, CancellationToken cancellationToken);
    Task<IEnumerable<ProductAggregate>> FindAll(CancellationToken cancellationToken);
    Task Update(ProductAggregate product);
    Task<decimal> GetGlobalDiscount();
}
