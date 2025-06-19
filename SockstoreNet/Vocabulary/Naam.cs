namespace Vocabulary;

public record Naam(string Value) {
    public string Value { get; } = Value ?? throw new ArgumentException("Naam mag niet leeg zijn.", nameof(Value));
}
