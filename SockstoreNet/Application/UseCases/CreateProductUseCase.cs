using Application.Commands;
using Application.Domain;
using Application.Interfaces;
using Application.Ports;
using Vocabulary;

namespace Application.UseCases;

public class CreateProductUseCase(IProductPort port) : ICreateProduct
{
    public async Task<Product> Create(CreateProductCommand cmd)
    {
        var product = new ProductAggregate(
            new ProductId(),
            cmd.Name,
            cmd.Category,
            cmd.Price,
            cmd.Stock);

        await port.Save(product);
        return product.ToProduct();
    }
}
