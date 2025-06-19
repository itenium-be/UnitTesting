namespace Vocabulary;

public record Voorraad(int Value) {
    public int Value { get; } = Value < 0 ? throw new ArgumentException("Vooraad mag niet negatief zijn"): Value;
};
