using System.Globalization;

namespace CalculatorApp;

public partial class Form1 : Form
{
    private const int MaxHistoryItems = 100;

    private double firstNumber;
    private double secondNumber;
    private double result;
    private string operation = string.Empty;
    private bool startNewEntry = true;

    public Form1()
    {
        InitializeComponent();
    }

    private void NumberButton_Click(object? sender, EventArgs e)
    {
        if (sender is not Button button)
        {
            return;
        }

        if (startNewEntry || txtDisplay.Text == "0")
        {
            txtDisplay.Text = button.Text;
            startNewEntry = false;
        }
        else
        {
            txtDisplay.Text += button.Text;
        }
    }

    private void OperatorButton_Click(object? sender, EventArgs e)
    {
        if (sender is not Button button || !TryReadDisplay(out firstNumber))
        {
            return;
        }

        operation = button.Text;
        txtDisplay.Clear();
        startNewEntry = true;
    }

    private void BtnEquals_Click(object? sender, EventArgs e)
    {
        try
        {
            if (!TryReadDisplay(out secondNumber))
            {
                return;
            }

            result = CalculatorEngine.Calculate(firstNumber, secondNumber, operation);
            txtDisplay.Text = result.ToString("G15", CultureInfo.InvariantCulture);
            AddToHistory(firstNumber, secondNumber, operation, result);
            operation = string.Empty;
            startNewEntry = true;
        }
        catch (Exception ex) when (ex is DivideByZeroException or InvalidOperationException)
        {
            MessageBox.Show(ex.Message, "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
            startNewEntry = true;
        }
    }

    private void BtnClear_Click(object? sender, EventArgs e)
    {
        firstNumber = 0;
        secondNumber = 0;
        result = 0;
        operation = string.Empty;
        startNewEntry = true;
        txtDisplay.Text = "0";
    }

    private void BtnDecimal_Click(object? sender, EventArgs e)
    {
        if (startNewEntry)
        {
            txtDisplay.Text = "0.";
            startNewEntry = false;
            return;
        }

        if (!txtDisplay.Text.Contains('.'))
        {
            txtDisplay.Text += ".";
        }
    }

    private void BtnClearHistory_Click(object? sender, EventArgs e)
    {
        lstHistory.Items.Clear();
    }

    private void AddToHistory(double left, double right, string selectedOperation, double calculationResult)
    {
        var entry = new CalculationHistoryEntry(left, right, selectedOperation, calculationResult);
        lstHistory.Items.Insert(0, entry);

        if (lstHistory.Items.Count > MaxHistoryItems)
        {
            lstHistory.Items.RemoveAt(lstHistory.Items.Count - 1);
        }
    }

    private bool TryReadDisplay(out double value)
    {
        if (double.TryParse(txtDisplay.Text, NumberStyles.Float, CultureInfo.InvariantCulture, out value))
        {
            return true;
        }

        MessageBox.Show("Nilai pada display tidak valid.", "Kesalahan", MessageBoxButtons.OK, MessageBoxIcon.Error);
        value = 0;
        return false;
    }
}
