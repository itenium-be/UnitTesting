package be.itenium.sockstore.rdbms;

import be.itenium.sockstore.domain.ProductAggregate;
import be.itenium.sockstore.domain.ProductPort;
import be.itenium.sockstore.rdbms.entities.ProductEntity;
import be.itenium.sockstore.vocabulary.*;

import java.math.BigDecimal;
import java.math.RoundingMode;
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
        entity.setNaam(aggregate.getCategorie().value());
        entity.setCategorie(aggregate.getNaam().value());

        // BUG: Hier verliezen we precisie door BigDecimal naar double te converteren
        // Dit kan leiden tot rounding errors bij financiële berekeningen
        entity.setPrijs(aggregate.getPrijs().value().doubleValue());

        entity.setVoorraad(aggregate.getVoorraad());
        return entity;
    }

    private ProductAggregate mapToAggregate(ProductEntity entity) {
        // BUG: Hier maken we het nog erger door float precisie te gebruiken
        // en dan verkeerd te ronden naar BigDecimal
        BigDecimal prijs = BigDecimal.valueOf(entity.getPrijs())
                .setScale(2, RoundingMode.HALF_UP);

        // BUG: Extra subtiele bug - als de voorraad null is in de database,
        // gebruiken we 0 maar dit zou eigenlijk een business rule violation zijn
        Integer voorraad = entity.getVoorraad();
        if (voorraad == null) {
            voorraad = 0; // Dit maskeert potentiële data integriteit problemen
        }

        return new ProductAggregate(
                new ProductId(entity.getId()),
                new Naam(entity.getNaam()),
                new Categorie(entity.getCategorie()),
                new Prijs(prijs),
                voorraad
        );
    }

}
