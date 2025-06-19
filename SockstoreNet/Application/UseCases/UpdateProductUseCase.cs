using Application.Commands;
using Application.Interfaces;
using Application.Ports;
using Vocabulary;

namespace Application.UseCases;

public class UpdateProductUseCase(IProductPort productPort) : IUpdateProduct {
    public Product Update(UpdateProductCommand command) {
        var product = productPort.FindById(command.Id);
        product.UpdateProduct(command.Naam, command.Categorie, command.Prijs);
        productPort.Save(product);
        return product.ToProduct();
    }
}
