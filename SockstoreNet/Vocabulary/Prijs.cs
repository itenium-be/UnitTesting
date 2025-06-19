namespace Vocabulary;

public record Prijs(decimal Value)
{
    public decimal Value { get; } = Value < 0 ? throw new ArgumentException("Prijs mag niet negatief zijn") : Value;

    public override string ToString() => $"{Value:0.00} EUR";
}
