package be.itenium.sockstore.usecases;

import be.itenium.sockstore.api.UpdateProduct;
import be.itenium.sockstore.api.commands.UpdateProductCommand;
import be.itenium.sockstore.domain.ProductAggregate;
import be.itenium.sockstore.domain.ProductPort;
import be.itenium.sockstore.vocabulary.Product;

public class UpdateProductUseCase implements UpdateProduct {
    private final ProductPort productPort;

    public UpdateProductUseCase(ProductPort productPort) {
        this.productPort = productPort;
    }

    public Product update(UpdateProductCommand command) {
        ProductAggregate updated = new ProductAggregate(
                command.id(),
                command.naam(),
                command.categorie(),
                command.prijs(),
                command.voorraad()
        );
        return productPort.save(updated).toProduct();
    }
}