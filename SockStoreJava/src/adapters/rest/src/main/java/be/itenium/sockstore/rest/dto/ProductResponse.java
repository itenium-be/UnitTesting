package be.itenium.sockstore.rest.dto;

import be.itenium.sockstore.vocabulary.Product;

public record ProductResponse(String id, String naam, String categorie, Double prijs, int voorraad) {
    public static ProductResponse toRest(Product product) {
        return new ProductResponse(
                product.id().value(),
                product.naam().value(),
                product.categorie().value(),
                product.prijs().value().doubleValue(),
                product.voorraad()
        );
    }
}
