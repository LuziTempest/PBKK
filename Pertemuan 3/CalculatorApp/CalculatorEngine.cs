namespace CalculatorApp;

public static class CalculatorEngine
{
    public static double Calculate(double firstNumber, double secondNumber, string operation)
    {
        return operation switch
        {
            "+" => firstNumber + secondNumber,
            "−" or "-" => firstNumber - secondNumber,
            "×" or "*" => firstNumber * secondNumber,
            "÷" or "/" when secondNumber == 0 => throw new DivideByZeroException(
                "Bilangan tidak dapat dibagi dengan nol."),
            "÷" or "/" => firstNumber / secondNumber,
            _ => throw new InvalidOperationException("Pilih operator sebelum menekan tombol sama dengan.")
        };
    }
}
