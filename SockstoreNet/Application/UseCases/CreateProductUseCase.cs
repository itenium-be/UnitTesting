using Application.Commands;
using Application.Domain;
using Application.Interfaces;
using Application.Ports;
using Vocabulary;

namespace Application.UseCases;

public class CreateProductUseCase(IProductPort port) : ICreateProduct {
    public Product Create(CreateProductCommand cmd) {
        var product = new ProductAggregate(
            new ProductId(Guid.NewGuid().ToString()),
            cmd.Naam,
            cmd.Categorie,
            cmd.Prijs,
            cmd.Voorraad);

        port.Save(product);
        return product.ToProduct();
    }
}
