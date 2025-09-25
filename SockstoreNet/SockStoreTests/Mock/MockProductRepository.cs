using Application.Domain;
using Application.Ports;
using Vocabulary;

namespace SockStoreTests.Mock;

public class MockProductRepository : IProductPort
{
    private readonly Dictionary<int, ProductAggregate> _store = [];

    public MockProductRepository(IEnumerable<ProductAggregate> products)
    {
        foreach (var product in products)
        {
            _store.Add(product.Id.Value, product);
        }
    }

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

    public Task<decimal> GetGlobalDiscount()
    {
        return Task.FromResult(0m);
    }
}