using Application.Commands;
using Application.Interfaces;
using Application.Ports;
using Vocabulary;

namespace Application.UseCases;

public class UpdateVoorraadUseCase(IProductPort productPort) : IUpdateVoorraad
{
    public async Task<Product> UpdateVoorraad(UpdateVoorraadCommand command)
    {
        if (command.Voorraad.Value < 0)
        {
            throw new InvalidOperationException("Voorraad kan niet negatief zijn.");
        }
        var product = await productPort.FindById(command.Id, CancellationToken.None);
        if (product == null)
            throw new ArgumentException($"Product with id {command.Id} was not found");

        product.UpdateStock(command.Voorraad);
        await productPort.Save(product);
        return product.ToProduct();
    }
}
