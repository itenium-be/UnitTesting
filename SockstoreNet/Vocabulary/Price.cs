namespace Vocabulary;

public class Price
{
    private const decimal WeekendDiscount = 0.9m;

    public decimal Value { get; }

    public Price(decimal value, decimal globalDiscount = 0)
    {
        if (value < 0)
        {
            throw new ArgumentException("Price cannot be negative");
        }

        if (DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
        {
            Value = value * WeekendDiscount;
        }
        else
        {
            Value = value;
        }

        if (globalDiscount != 0)
        {
            Value -= globalDiscount;
        }

        Value = Math.Round(Value, 2);
    }

    public override string ToString() => $"{Value:0.00} EUR";
}
