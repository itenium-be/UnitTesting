package be.itenium.sockstore.domain;


import be.itenium.sockstore.vocabulary.*;
import lombok.Getter;

import java.math.BigDecimal;

@Getter
public class ProductAggregate {

    private final ProductId id;
    private final Naam naam;
    private final Categorie categorie;
    private final Prijs prijs;
    private final int voorraad;

    public ProductAggregate(Naam naam, Categorie categorie, Prijs prijs, int voorraad) {
        this(ProductId.create(), naam, categorie, prijs, voorraad);
    }

    public ProductAggregate(ProductId id, Naam naam, Categorie categorie, Prijs prijs, int voorraad) {
        if (id == null || naam == null || categorie == null || prijs == null) {
            throw new IllegalArgumentException("Geen enkel veld mag null zijn");
        }
        this.id = id;
        this.naam = naam;
        this.categorie = categorie;
        this.prijs = prijs;
        this.voorraad = voorraad;
    }

    public ProductAggregate withVoorraad(int nieuweVoorraad) {
        return new ProductAggregate(this.id, this.naam, this.categorie, this.prijs, nieuweVoorraad);
    }

    public ProductAggregate update(Naam naam, Categorie categorie, Prijs prijs, int voorraad) {
        return new ProductAggregate(this.id, naam, categorie, prijs, voorraad);
    }

    public Product toProduct() {
        return new Product(id, naam, categorie, prijs, voorraad);
    }

    public static ProductAggregate from(Product product) {
        return new ProductAggregate(product.id(), product.naam(), product.categorie(), product.prijs(), product.voorraad());
    }
}
