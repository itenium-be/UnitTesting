namespace Vocabulary;

public record Price(decimal Value)
{
    public decimal Value { get; } = Value < 0 ? throw new ArgumentException("Price cannot be negative") : Value;

    public override string ToString() => $"{Value:0.00} EUR";
}
