package be.itenium.sockstore.rdbms.entities;

import be.itenium.sockstore.domain.ProductAggregate;
import be.itenium.sockstore.vocabulary.Categorie;
import be.itenium.sockstore.vocabulary.Naam;
import be.itenium.sockstore.vocabulary.Prijs;
import be.itenium.sockstore.vocabulary.ProductId;
import jakarta.persistence.Entity;
import jakarta.persistence.Id;
import lombok.*;

import java.math.BigDecimal;

@Entity
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class ProductEntity {
    @Id
    private String id;
    private String naam;
    private String categorie;
    private Double prijs;
    private int voorraad;

    public static ProductEntity fromAggregate(ProductAggregate aggregate) {
        return ProductEntity.builder()
                .id(aggregate.getId().value())
                .naam(aggregate.getNaam().value())
                .categorie(aggregate.getCategorie().value())
                .prijs(aggregate.getPrijs().value().doubleValue())
                .voorraad(aggregate.getVoorraad())
                .build();
    }

    public ProductAggregate toAggregate() {
        return new ProductAggregate(new ProductId(id), new Naam(naam), new Categorie(categorie), new Prijs(BigDecimal.valueOf(prijs)), voorraad);
    }
}