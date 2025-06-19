package be.itenium.sockstore.usecases;

import be.itenium.sockstore.api.UpdateVoorraad;
import be.itenium.sockstore.domain.ProductAggregate;
import be.itenium.sockstore.api.commands.UpdateVoorraadCommand;
import be.itenium.sockstore.domain.ProductPort;
import be.itenium.sockstore.vocabulary.Product;

import java.util.Optional;

public class UpdateVoorraadUseCase implements UpdateVoorraad {
    private final ProductPort productPort;

    public UpdateVoorraadUseCase(ProductPort productPort) {
        this.productPort = productPort;
    }

    public Optional<Product> updateVoorraad(UpdateVoorraadCommand command) {
        Optional<ProductAggregate> aggregate = productPort.findById(command.id());
        if (aggregate.isEmpty()) {
            return Optional.empty();
        }
        return Optional.of(productPort.save(aggregate.get().withVoorraad(command.nieuweVoorraad())).toProduct());
    }
}