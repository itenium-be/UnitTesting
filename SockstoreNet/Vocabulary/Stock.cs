namespace Vocabulary;

public record Stock(int Value)
{
    public int Value { get; } = Value < 0 ? throw new ArgumentException("Stock cannot be negative") : Value;

    public override string ToString() => Value.ToString();
};
