package be.itenium.sockstore.rest.dto;

import be.itenium.sockstore.vocabulary.Product;
import be.itenium.sockstore.vocabulary.ProductId;

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
}