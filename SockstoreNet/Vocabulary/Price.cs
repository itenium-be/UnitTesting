namespace Vocabulary;

public class Price
{
    private const decimal WeekendDiscount = 0.9m;

    public decimal Value { get; }

    public Price(decimal value)
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
    }

    public override string ToString() => $"{Value:0.00} EUR";
}
