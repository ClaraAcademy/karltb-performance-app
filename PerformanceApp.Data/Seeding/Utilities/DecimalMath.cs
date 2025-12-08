namespace PerformanceApp.Data.Seeding.Utilities;

public static class DecimalMath
{
    public static decimal SquareRoot(decimal value)
    {
        var x = (double)value;
        var result = Math.Sqrt(x);
        return (decimal)result;
    }

    public static decimal Power(decimal x, decimal y)
    {
        var dX = (double)x;
        var dY = (double)y;
        var result = Math.Pow(dX, dY);
        return (decimal)result;
    }

    public static decimal StandardDeviation(IEnumerable<decimal> values)
    {
        var dValues = values.Select(ToDouble);
        return (decimal)StandardDeviation(dValues);
    }

    public static decimal Product(IEnumerable<decimal> values)
    {
        var product = 1M;
        foreach (var value in values)
        {
            product *= value;
        }
        return product;
    }

    static double ToDouble(decimal v) => (double)v;
    static double StandardDeviation(IEnumerable<double> values)
    {
        var mean = values.Average();
        var n = values.Count();

        if (n < 2)
        {
            return 0.0;
        }

        var sum = values
            .Select(v => v - mean)
            .Select(v => v * v)
            .Sum();

        return Math.Sqrt(sum / (n - 1.0));
    }

}