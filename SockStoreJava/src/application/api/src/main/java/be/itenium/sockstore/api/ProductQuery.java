package be.itenium.sockstore.api;

import be.itenium.sockstore.vocabulary.Product;
import be.itenium.sockstore.vocabulary.ProductId;

import java.util.List;
import java.util.Optional;

public interface ProductQuery {
    Optional<Product> findById(ProductId id);
    List<Product> findAll();
}