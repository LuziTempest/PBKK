using CalculatorApp;

var cases = new (double Left, double Right, string Operator, double Expected)[]
{
    (10, 20, "+", 30),
    (30, 12, "−", 18),
    (6, 7, "×", 42),
    (100, 4, "÷", 25),
    (2.5, 4, "×", 10)
};

foreach (var testCase in cases)
{
    double actual = CalculatorEngine.Calculate(testCase.Left, testCase.Right, testCase.Operator);
    if (Math.Abs(actual - testCase.Expected) > 0.0000001)
    {
        throw new InvalidOperationException(
            $"Gagal: {testCase.Left} {testCase.Operator} {testCase.Right} menghasilkan {actual}.");
    }
}

try
{
    CalculatorEngine.Calculate(10, 0, "÷");
    throw new InvalidOperationException("Gagal: pembagian nol tidak ditolak.");
}
catch (DivideByZeroException)
{
    // Hasil yang diharapkan.
}

var historyEntry = new CalculationHistoryEntry(12.5, 7.5, "+", 20);
if (historyEntry.ToString() != "12.5 + 7.5 = 20")
{
    throw new InvalidOperationException("Gagal: format riwayat perhitungan tidak sesuai.");
}

Console.WriteLine("7 skenario logika kalkulator dan riwayat lulus.");
