namespace Vocabulary;

public record Categorie(string Value) {
    public string Value { get; } = Value ?? throw new ArgumentException("Categorie mag niet leeg zijn.", nameof(Value));
}