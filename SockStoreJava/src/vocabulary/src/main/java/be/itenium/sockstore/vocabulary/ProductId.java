package be.itenium.sockstore.vocabulary;

import java.util.UUID;

public record ProductId(String value) {
    public ProductId {
        if (value == null || value.isBlank()) {
            throw new IllegalArgumentException("ProductId must not be null or blank");
        }
    }
    public static ProductId create() {
        return new ProductId(UUID.randomUUID().toString());
    }
}
