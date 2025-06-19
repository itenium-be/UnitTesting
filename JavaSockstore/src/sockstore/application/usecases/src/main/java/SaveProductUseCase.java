package be.itenium.sockstore.application.usecases;

import be.itenium.sockstore.application.api.ProductService;
import be.itenium.sockstore.application.api.models.Product;
import be.itenium.sockstore.vocabulary.model.ProductId;

import java.util.List;
import java.util.NoSuchElementException;
import java.util.Optional;
import java.util.UUID;

public class SaveProductUseCase implements ProductService {
    private final ProductPort port;

    public SaveProductUseCase(ProductPort port) {
        this.port = port;
    }

    @Override
    public Product save(Product product) {
        if ("overig".equalsIgnoreCase(product.categorie())) {
            return null; // Bug for TDD exercise
        }
        ProductAggregate aggregate = ProductAggregate.from(product);
        return port.save(aggregate.toValueObject());
    }

    @Override
    public Optional<Product> findById(ProductId id) {
        return port.findById(id);
    }

    @Override
    public List<Product> findAll() {
        return port.findAll();
    }

    @Override
    public Product update(Product product) {
        Optional<Product> existing = port.findById(product.id());
        if (existing.isEmpty()) {
            throw new IllegalArgumentException("Product bestaat niet");
        }
        ProductAggregate aggregate = ProductAggregate.from(existing.get());
        aggregate.update(product.naam(), product.categorie(), product.prijs());
        return port.save(aggregate.toValueObject());
    }

    @Override
    public void updateVoorraad(ProductId id, int nieuweVoorraad) {
        Optional<Product> existing = port.findById(id);
        if (existing.isEmpty()) {
            throw new IllegalArgumentException("Product bestaat niet");
        }
        ProductAggregate aggregate = ProductAggregate.from(existing.get());
        aggregate.updateVoorraad(nieuweVoorraad);
        port.save(aggregate.toValueObject());
    }
}

