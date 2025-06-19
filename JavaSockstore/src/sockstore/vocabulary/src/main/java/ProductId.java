package be.itenium.sockstore.vocabulary.model;

public record ProductId(String value) {
    public ProductId {
        if (value == null || value.isBlank()) {
            throw new IllegalArgumentException("ProductId must not be null or blank");
        }
    }
}
