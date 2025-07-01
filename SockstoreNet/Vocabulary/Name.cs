namespace Vocabulary;

public record Name(string Value)
{
    public string Value { get; } = Value ?? throw new ArgumentException("Name cannot be empty.", nameof(Value));

    public override string ToString() => Value;
}
