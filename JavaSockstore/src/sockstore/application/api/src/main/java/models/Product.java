package be.itenium.sockstore.application.api.models;

import be.itenium.sockstore.vocabulary.model.ProductId;

import java.math.BigDecimal;

public record Product(ProductId id, String naam, String categorie, BigDecimal prijs, Integer voorraad) {
    public Product {
        if (voorraad == null) {
            voorraad = 0;
        }
        if (id == null || naam == null || categorie == null || prijs == null) {
            throw new IllegalArgumentException("Product fields must not be null");
        }
        if (prijs.compareTo(BigDecimal.ZERO) < 0) {
            throw new IllegalArgumentException("Prijs mag niet negatief zijn.");
        }
        if (voorraad < 0) {
            throw new IllegalArgumentException("Voorraad mag niet negatief zijn.");
        }
    }
}