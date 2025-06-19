package be.itenium.sockstore.api;

import be.itenium.sockstore.api.commands.UpdateVoorraadCommand;
import be.itenium.sockstore.vocabulary.Product;

import java.util.Optional;

public interface UpdateVoorraad {
    Optional<Product> updateVoorraad(UpdateVoorraadCommand command);
}
