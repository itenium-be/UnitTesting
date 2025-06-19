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
        // BUG: We vergeten te valideren of de nieuwe voorraad negatief is!
        // Dit zou moeten worden opgevangen door business logic
        ProductAggregate updated = aggregate.get().withVoorraad(command.nieuweVoorraad());

        // BUG: Laten we ook een subtle business logic fout introduceren
        // Als de voorraad onder de 10 komt, zouden we een warning moeten loggen
        // maar we doen dit verkeerd - we checken de oude voorraad ipv de nieuwe
        if (aggregate.get().getVoorraad() < 10) {
            System.out.println("WARNING: Lage voorraad voor product " + command.id().value());
        }

        return Optional.of(productPort.save(updated).toProduct());
    }
}