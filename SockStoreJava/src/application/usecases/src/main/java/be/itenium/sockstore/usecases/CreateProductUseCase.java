package be.itenium.sockstore.usecases;

import be.itenium.sockstore.api.CreateProduct;
import be.itenium.sockstore.domain.ProductAggregate;
import be.itenium.sockstore.api.commands.CreateProductCommand;
import be.itenium.sockstore.domain.ProductPort;
import be.itenium.sockstore.vocabulary.Product;

public class CreateProductUseCase implements CreateProduct {
    private final ProductPort productPort;

    public CreateProductUseCase(ProductPort productPort) {
        this.productPort = productPort;
    }

    public Product create(CreateProductCommand command) {
        ProductAggregate nieuwProduct = new ProductAggregate(
                command.naam(),
                command.categorie(),
                command.prijs(),
                command.voorraad()
        );
        return productPort.save(nieuwProduct).toProduct();
    }
}