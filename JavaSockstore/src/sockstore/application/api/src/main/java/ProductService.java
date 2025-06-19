package be.itenium.sockstore.application.api;

import be.itenium.sockstore.application.api.models.Product;
import be.itenium.sockstore.vocabulary.model.ProductId;

import java.util.List;
import java.util.Optional;

public interface ProductService {
    Product save(Product product);
    Product updateVoorraad(ProductId id, int voorraad);
    Optional<Product> findById(ProductId id);
    List<Product> findAll();
}