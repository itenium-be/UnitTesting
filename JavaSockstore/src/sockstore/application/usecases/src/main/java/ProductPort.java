package be.itenium.sockstore.application.usecases;

import be.itenium.sockstore.application.api.models.Product;
import be.itenium.sockstore.vocabulary.model.ProductId;

import java.util.List;
import java.util.Optional;

public interface ProductPort {
    Product save(Product product);

    Product updateVoorraad(ProductId id, int nieuweVoorraad);

    Optional<Product> findById(ProductId id);

    List<Product> findAll();
}
