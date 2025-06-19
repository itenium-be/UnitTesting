package be.itenium.sockstore.vocabulary;

public record Categorie(String value) {
    public Categorie {
        if (value == null || value.isBlank()) {
            throw new IllegalArgumentException("Categorie mag niet leeg zijn");
        }
    }
}