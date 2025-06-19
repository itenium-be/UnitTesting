using Application.Commands;
using Application.Interfaces;
using Application.Ports;
using Vocabulary;

namespace Application.UseCases;

public class UpdateProductUseCase(IProductPort productPort) : IUpdateProduct
{
    public async Task<Product> Update(UpdateProductCommand command)
    {
        var product = await productPort.FindById(command.Id, CancellationToken.None);
        if (product == null)
            throw new ArgumentException($"Product with id {command.Id} was not found");

        product.UpdateProduct(command.Naam, command.Categorie, command.Prijs);
        await productPort.Save(product);
        return product.ToProduct();
    }
}
