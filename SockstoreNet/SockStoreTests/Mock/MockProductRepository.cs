using Application.Domain;
using Application.Ports;
using Vocabulary;

namespace SockStoreTests.Mock;

public class MockProductRepository : IProductPort {
    private readonly Dictionary<string, ProductAggregate> _store = new();

    public Task SaveAsync(ProductAggregate product)
    {
        _store[product.Id.Value] = product;
        return Task.CompletedTask;
    }
    public void Save(ProductAggregate product) {
        _store[product.Id.Value] = product;
    }
    public ProductAggregate? FindById(ProductId id) => _store.GetValueOrDefault(id.Value);
    public IEnumerable<ProductAggregate> FindAll() => _store.Values.ToList();
    public void Update(ProductAggregate product) {
        _store[product.Id.Value] = product;
    }
}