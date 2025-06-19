package be.itenium.sockstore.model.rest;

import be.itenium.sockstore.model.Product;

public record ProductDto(String id, String titel, String omschrijving, Double prijs, Integer voorraad) {
    public ProductDto {
        if (titel == null || titel.isBlank()) {
            throw new IllegalArgumentException("Titel mag niet leeg zijn");
        }
        if (prijs == null || prijs <= 0) {
            throw new IllegalArgumentException("Prijs moet positief zijn");
        }
    }
    public static Product toProduct(ProductDto dto) {
        Product product = new Product();
        product.setId(dto.id());
        product.setTitel(dto.titel());
        product.setOmschrijving(dto.omschrijving());
        product.setPrijs(dto.prijs());
        product.setVoorraad(dto.voorraad());
        return product;
    }
}