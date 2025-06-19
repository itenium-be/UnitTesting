package be.itenium.sockstore.rdbms;

import be.itenium.sockstore.domain.ProductAggregate;
import be.itenium.sockstore.domain.ProductPort;
import be.itenium.sockstore.rdbms.entities.ProductEntity;
import be.itenium.sockstore.vocabulary.*;

import java.math.BigDecimal;
import java.util.List;
import java.util.Optional;
import java.util.stream.Collectors;

public class ProductRepositoryAdapter implements ProductPort {
    private final ProductRepository repository;

    public ProductRepositoryAdapter(ProductRepository repository) {
        this.repository = repository;
    }

    @Override
    public ProductAggregate save(ProductAggregate aggregate) {
        ProductEntity entity = mapToEntity(aggregate);
        ProductEntity saved = repository.save(entity);
        return mapToAggregate(saved);
    }

    @Override
    public Optional<ProductAggregate> findById(ProductId id) {
        return repository.findById(id.value()).map(this::mapToAggregate);
    }

    @Override
    public List<ProductAggregate> findAll() {
        return repository.findAll().stream()
                .map(this::mapToAggregate)
                .collect(Collectors.toList());
    }

    private ProductEntity mapToEntity(ProductAggregate aggregate) {
        ProductEntity entity = new ProductEntity();
        entity.setId(aggregate.getId().value());
        entity.setNaam(aggregate.getNaam().value());
        entity.setCategorie(aggregate.getCategorie().value());
        entity.setPrijs(aggregate.getPrijs().value().doubleValue());
        entity.setVoorraad(aggregate.getVoorraad());
        return entity;
    }

    private ProductAggregate mapToAggregate(ProductEntity entity) {
        return new ProductAggregate(
                new ProductId(entity.getId()),
                new Naam(entity.getNaam()),
                new Categorie(entity.getCategorie()),
                new Prijs(BigDecimal.valueOf(entity.getPrijs())),
                entity.getVoorraad()
        );
    }
}
