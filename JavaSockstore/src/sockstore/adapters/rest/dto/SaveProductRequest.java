package be.itenium.sockstore.adapters.rest.dto;

import be.itenium.sockstore.application.api.models.Product;
import be.itenium.sockstore.vocabulary.model.ProductId;

import java.math.BigDecimal;

public record SaveProductRequest(String naam, String categorie, Double prijs, Integer voorraad) {
    public SaveProductRequest {
        if (naam == null || naam.isBlank()) {
            throw new IllegalArgumentException("Naam mag niet leeg zijn");
        }
        if (categorie == null || categorie.isBlank()) {
            throw new IllegalArgumentException("Categorie mag niet leeg zijn");
        }
        if (prijs == null || prijs <= 0) {
            throw new IllegalArgumentException("Prijs moet positief zijn");
        }
    }

    public Product fromRest() {
        Product product = new Product(
                null,
                naam,
                categorie,
                BigDecimal.valueOf(prijs),
                voorraad
        );
        return product;
    }

    public Product fromRestWithId(String id) {
        Product product = new Product(
                new ProductId(id),
                naam,
                categorie,
                BigDecimal.valueOf(prijs),
                voorraad
        );
        return product;
    }
}