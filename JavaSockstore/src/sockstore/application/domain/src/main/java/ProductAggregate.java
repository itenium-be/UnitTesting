package be.itenium.sockstore.application.domain;

package application.domain.model;

import vocabulary.ProductId;
import lombok.Getter;

import java.math.BigDecimal;
import java.util.Objects;

@Getter
public class ProductAggregate {

    private final ProductId id;
    private String naam;
    private String categorie;
    private BigDecimal prijs;
    private int voorraad;

    public ProductAggregate(ProductId id, String naam, String categorie, BigDecimal prijs, int voorraad) {
        if (id == null || naam == null || categorie == null || prijs == null) {
            throw new IllegalArgumentException("Geen enkel veld mag null zijn.");
        }
        if (prijs.compareTo(BigDecimal.ZERO) < 0) {
            throw new IllegalArgumentException("Prijs mag niet negatief zijn.");
        }
        if (voorraad < 0) {
            throw new IllegalArgumentException("Voorraad mag niet negatief zijn.");
        }

        this.id = id;
        this.naam = naam;
        this.categorie = categorie;
        this.prijs = prijs;
        this.voorraad = voorraad;
    }

    public void update(String naam, String categorie, BigDecimal prijs) {
        if (naam != null) this.naam = naam;
        if (categorie != null) this.categorie = categorie;
        if (prijs != null && prijs.compareTo(BigDecimal.ZERO) >= 0) this.prijs = prijs;
    }

    public void updateVoorraad(int nieuweVoorraad) {
        if (nieuweVoorraad < 0) {
            throw new IllegalArgumentException("Voorraad mag niet negatief zijn.");
        }
        this.voorraad = nieuweVoorraad;
    }

    public Product toValueObject() {
        return new Product(id, naam, categorie, prijs, voorraad);
    }

    public static ProductAggregate from(Product product) {
        return new ProductAggregate(
                product.id(),
                product.naam(),
                product.categorie(),
                product.prijs(),
                product.voorraad()
        );
    }
}
