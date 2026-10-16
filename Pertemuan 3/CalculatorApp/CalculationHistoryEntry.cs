using System.Globalization;

namespace CalculatorApp;

public sealed record CalculationHistoryEntry(
    double FirstNumber,
    double SecondNumber,
    string Operation,
    double Result)
{
    public override string ToString()
    {
        return $"{FormatNumber(FirstNumber)} {Operation} {FormatNumber(SecondNumber)} = {FormatNumber(Result)}";
    }

    private static string FormatNumber(double value)
    {
        return value.ToString("G15", CultureInfo.InvariantCulture);
    }
}
