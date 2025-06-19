package be.itenium.sockstore.vocabulary;

public record Naam(String value) {
    public Naam {
        if (value == null || value.isBlank()) {
            throw new IllegalArgumentException("Naam mag niet leeg zijn");
        }
    }
}