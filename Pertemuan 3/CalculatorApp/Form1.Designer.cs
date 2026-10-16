#nullable enable

namespace CalculatorApp;

partial class Form1
{
    private System.ComponentModel.IContainer? components = null;
    private Label lblTitle = null!;
    private TextBox txtDisplay = null!;
    private TableLayoutPanel buttonGrid = null!;
    private GroupBox grpHistory = null!;
    private ListBox lstHistory = null!;
    private Button btnClearHistory = null!;
    private Button btn0 = null!;
    private Button btn1 = null!;
    private Button btn2 = null!;
    private Button btn3 = null!;
    private Button btn4 = null!;
    private Button btn5 = null!;
    private Button btn6 = null!;
    private Button btn7 = null!;
    private Button btn8 = null!;
    private Button btn9 = null!;
    private Button btnDecimal = null!;
    private Button btnClear = null!;
    private Button btnPlus = null!;
    private Button btnMinus = null!;
    private Button btnMultiply = null!;
    private Button btnDivide = null!;
    private Button btnEquals = null!;

    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            components?.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblTitle = new Label();
        txtDisplay = new TextBox();
        buttonGrid = new TableLayoutPanel();
        grpHistory = new GroupBox();
        lstHistory = new ListBox();
        btnClearHistory = new Button();
        btn7 = CreateButton("btn7", "7", NumberButton_Click);
        btn8 = CreateButton("btn8", "8", NumberButton_Click);
        btn9 = CreateButton("btn9", "9", NumberButton_Click);
        btnDivide = CreateButton("btnDivide", "÷", OperatorButton_Click);
        btn4 = CreateButton("btn4", "4", NumberButton_Click);
        btn5 = CreateButton("btn5", "5", NumberButton_Click);
        btn6 = CreateButton("btn6", "6", NumberButton_Click);
        btnMultiply = CreateButton("btnMultiply", "×", OperatorButton_Click);
        btn1 = CreateButton("btn1", "1", NumberButton_Click);
        btn2 = CreateButton("btn2", "2", NumberButton_Click);
        btn3 = CreateButton("btn3", "3", NumberButton_Click);
        btnMinus = CreateButton("btnMinus", "−", OperatorButton_Click);
        btn0 = CreateButton("btn0", "0", NumberButton_Click);
        btnDecimal = CreateButton("btnDecimal", ".", BtnDecimal_Click);
        btnClear = CreateButton("btnClear", "C", BtnClear_Click);
        btnPlus = CreateButton("btnPlus", "+", OperatorButton_Click);
        btnEquals = CreateButton("btnEquals", "=", BtnEquals_Click);
        SuspendLayout();

        lblTitle.AutoSize = true;
        lblTitle.Font = new Font("Segoe UI", 18F, FontStyle.Bold);
        lblTitle.Location = new Point(18, 16);
        lblTitle.Name = "lblTitle";
        lblTitle.Text = "Calculator";

        txtDisplay.BackColor = Color.White;
        txtDisplay.Font = new Font("Segoe UI", 22F);
        txtDisplay.Location = new Point(20, 60);
        txtDisplay.Name = "txtDisplay";
        txtDisplay.ReadOnly = true;
        txtDisplay.Size = new Size(332, 47);
        txtDisplay.TabStop = false;
        txtDisplay.Text = "0";
        txtDisplay.TextAlign = HorizontalAlignment.Right;

        buttonGrid.ColumnCount = 4;
        buttonGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        buttonGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        buttonGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        buttonGrid.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25F));
        buttonGrid.Location = new Point(20, 125);
        buttonGrid.Name = "buttonGrid";
        buttonGrid.RowCount = 5;
        buttonGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
        buttonGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
        buttonGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
        buttonGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
        buttonGrid.RowStyles.Add(new RowStyle(SizeType.Percent, 20F));
        buttonGrid.Size = new Size(332, 325);
        buttonGrid.Controls.Add(btn7, 0, 0);
        buttonGrid.Controls.Add(btn8, 1, 0);
        buttonGrid.Controls.Add(btn9, 2, 0);
        buttonGrid.Controls.Add(btnDivide, 3, 0);
        buttonGrid.Controls.Add(btn4, 0, 1);
        buttonGrid.Controls.Add(btn5, 1, 1);
        buttonGrid.Controls.Add(btn6, 2, 1);
        buttonGrid.Controls.Add(btnMultiply, 3, 1);
        buttonGrid.Controls.Add(btn1, 0, 2);
        buttonGrid.Controls.Add(btn2, 1, 2);
        buttonGrid.Controls.Add(btn3, 2, 2);
        buttonGrid.Controls.Add(btnMinus, 3, 2);
        buttonGrid.Controls.Add(btn0, 0, 3);
        buttonGrid.Controls.Add(btnDecimal, 1, 3);
        buttonGrid.Controls.Add(btnClear, 2, 3);
        buttonGrid.Controls.Add(btnPlus, 3, 3);
        buttonGrid.Controls.Add(btnEquals, 0, 4);
        buttonGrid.SetColumnSpan(btnEquals, 4);

        grpHistory.Controls.Add(lstHistory);
        grpHistory.Controls.Add(btnClearHistory);
        grpHistory.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        grpHistory.Location = new Point(370, 60);
        grpHistory.Name = "grpHistory";
        grpHistory.Size = new Size(264, 390);
        grpHistory.TabStop = false;
        grpHistory.Text = "Riwayat Perhitungan";

        lstHistory.Font = new Font("Segoe UI", 10F);
        lstHistory.FormattingEnabled = true;
        lstHistory.HorizontalScrollbar = true;
        lstHistory.ItemHeight = 17;
        lstHistory.Location = new Point(12, 28);
        lstHistory.Name = "lstHistory";
        lstHistory.Size = new Size(240, 293);
        lstHistory.TabIndex = 0;

        btnClearHistory.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnClearHistory.Location = new Point(12, 333);
        btnClearHistory.Name = "btnClearHistory";
        btnClearHistory.Size = new Size(240, 42);
        btnClearHistory.TabIndex = 1;
        btnClearHistory.Text = "Hapus Riwayat";
        btnClearHistory.UseVisualStyleBackColor = true;
        btnClearHistory.Click += BtnClearHistory_Click;

        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(245, 247, 250);
        ClientSize = new Size(654, 472);
        Controls.Add(lblTitle);
        Controls.Add(txtDisplay);
        Controls.Add(buttonGrid);
        Controls.Add(grpHistory);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        MaximizeBox = false;
        Name = "Form1";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Calculator App";
        ResumeLayout(false);
        PerformLayout();
    }

    private static Button CreateButton(string name, string text, EventHandler clickHandler)
    {
        var button = new Button
        {
            Dock = DockStyle.Fill,
            Font = new Font("Segoe UI", 15F, FontStyle.Bold),
            Margin = new Padding(4),
            Name = name,
            Text = text,
            UseVisualStyleBackColor = true
        };
        button.Click += clickHandler;
        return button;
    }
}
