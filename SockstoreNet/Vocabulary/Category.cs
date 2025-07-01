namespace Vocabulary;

public record Category(string Value)
{
    public string Value { get; } = Value ?? throw new ArgumentException("Category cannot be empty.", nameof(Value));

    public override string ToString() => Value;
}