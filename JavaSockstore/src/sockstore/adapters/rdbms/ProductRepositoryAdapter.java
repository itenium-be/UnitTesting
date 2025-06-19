package be.itenium.sockstore.adapters.rdbms;

import be.itenium.sockstore.adapters.rdbms.entities.ProductEntity;
import be.itenium.sockstore.application.api.models.Product;
import be.itenium.sockstore.application.usecases.ProductPort;
import be.itenium.sockstore.vocabulary.model.ProductId;

import java.math.BigDecimal;
import java.util.List;
import java.util.Optional;

public class ProductRepositoryAdapter implements ProductPort {
    private final ProductRepository repository;

    public ProductRepositoryAdapter(ProductRepository repository) {
        this.repository = repository;
    }

    @Override
    public Product save(Product product) {
        ProductEntity entity = ProductEntity.builder().id(product.id().value()).naam(product.naam()).categorie(product.categorie()).prijs(product.prijs().doubleValue()).voorraad(product.voorraad()).build();

        ProductEntity opgeslagen = repository.save(entity);
        return new Product(new ProductId(opgeslagen.getId()), opgeslagen.getNaam(), opgeslagen.getCategorie(), BigDecimal.valueOf(opgeslagen.getPrijs()), opgeslagen.getVoorraad());
    }

    @Override
    public Product updateVoorraad(ProductId id, int nieuweVoorraad) {
        return null;
    }

    @Override
    public Optional<Product> findById(ProductId id) {
        return Optional.empty();
    }

    @Override
    public List<Product> findAll() {
        return List.of();
    }
}
