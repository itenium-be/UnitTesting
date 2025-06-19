package be.itenium.sockstore.domain;

import be.itenium.sockstore.vocabulary.ProductId;

import java.util.List;
import java.util.Optional;

public interface ProductPort {
    ProductAggregate save(ProductAggregate product);

    Optional<ProductAggregate> findById(ProductId id);

    List<ProductAggregate> findAll();
}
