using Application.Domain;
using Application.Ports;
using Vocabulary;

namespace SockStoreTests.Mock;

public class MockProductRepository : IProductPort
{
    private readonly Dictionary<string, ProductAggregate> _store = new();

    public Task SaveAsync(ProductAggregate product)
    {
        _store[product.Id.Value] = product;
        return Task.CompletedTask;
    }

    public Task Save(ProductAggregate product)
    {
        _store[product.Id.Value] = product;
        return Task.CompletedTask;
    }

    public Task<ProductAggregate?> FindById(ProductId id, CancellationToken cancellationToken) => Task.FromResult(_store.GetValueOrDefault(id.Value));

    public Task<IEnumerable<ProductAggregate>> FindAll(CancellationToken cancellationToken) => Task.FromResult(_store.Values.AsEnumerable());

    public Task Update(ProductAggregate product)
    {
        _store[product.Id.Value] = product;
        return Task.CompletedTask;
    }
}