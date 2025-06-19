package be.itenium.sockstore.vocabulary;

import java.math.BigDecimal;

public record Product(ProductId id, Naam naam, Categorie categorie, Prijs prijs, Integer voorraad) {
    public Product {
        if (voorraad == null) {
            voorraad = 0;
        }
        if (id == null || naam == null || categorie == null || prijs == null) {
            throw new IllegalArgumentException("Product fields must not be null");
        }
        if (prijs.value().compareTo(BigDecimal.ZERO) < 0) {
            throw new IllegalArgumentException("Prijs mag niet negatief zijn.");
        }
        if (voorraad < 0) {
            throw new IllegalArgumentException("Voorraad mag niet negatief zijn.");
        }
    }
}