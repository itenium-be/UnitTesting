package be.itenium.sockstore.adapters.rest.dto;

import be.itenium.sockstore.application.api.models.Product;

public record ProductResponse(String id, String naam, String categorie, Double prijs, int voorraad) {
    public static ProductResponse toRest(Product product) {
        return new ProductResponse(
                product.id().toString(),
                product.naam(),
                product.categorie(),
                product.prijs().doubleValue(),
                product.voorraad()
        );
    }
}
