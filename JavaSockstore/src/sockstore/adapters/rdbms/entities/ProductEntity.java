package be.itenium.sockstore.adapters.rdbms.entities;

import jakarta.persistence.Entity;
import jakarta.persistence.Id;
import lombok.*;

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
    private BigDecimal prijs;
    private int voorraad;

    public static ProductEntity fromAggregate(ProductAggregate aggregate) {
        return ProductEntity.builder()
                .id(aggregate.getId().getValue())
                .naam(aggregate.getNaam())
                .categorie(aggregate.getCategorie())
                .prijs(aggregate.getPrijs())
                .voorraad(aggregate.getVoorraad())
                .build();
    }

    public ProductAggregate toAggregate() {
        return ProductAggregate.reconstruct(id, naam, categorie, prijs, voorraad);
    }
}