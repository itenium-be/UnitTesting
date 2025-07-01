namespace Vocabulary;

public record ProductId(int Value = 0)
{
    public int Value { get; } = Value;

    public override string ToString() => Value.ToString();
}
