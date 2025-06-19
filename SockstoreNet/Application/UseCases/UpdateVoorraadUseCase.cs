using Application.Commands;
using Application.Interfaces;
using Application.Ports;
using Vocabulary;

namespace Application.UseCases;

public class UpdateVoorraadUseCase(IProductPort productPort) : IUpdateVoorraad {
    public Product UpdateVoorraad(UpdateVoorraadCommand command) {
        if (command.Voorraad.Value < 0)
        {
            throw new InvalidOperationException("Voorraad kan niet negatief zijn.");
        }
        var product = productPort.FindById(command.Id);
        product.UpdateStock(command.Voorraad);
        productPort.Save(product);
        return product.ToProduct();
    }
}
