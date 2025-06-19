package be.itenium.sockstore.model;

import be.itenium.sockstore.model.rest.ProductDto;
import jakarta.persistence.Entity;
import jakarta.persistence.GeneratedValue;
import jakarta.persistence.Id;

import lombok.Data;

@Data
@Entity
public class Product {
    @Id
    @GeneratedValue(generator = "uuid")
    private String id;
    private String titel;
    private String omschrijving;
    private Double prijs;
    private Integer voorraad;

    public static ProductDto toProductDto(Product product) {
        return new ProductDto(
                product.getId(),
                product.getTitel(),
                product.getOmschrijving(),
                product.getPrijs(),
                product.getVoorraad()
        );
    }
}