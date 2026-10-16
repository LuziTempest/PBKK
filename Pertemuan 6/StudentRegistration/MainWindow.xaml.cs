using System.Windows;
using StudentRegistration.Configuration;
using StudentRegistration.Repositories;

namespace StudentRegistration;

public partial class MainWindow : Window
{
    private readonly StudentRepository studentRepository;

    public MainWindow()
    {
        InitializeComponent();
        studentRepository = new StudentRepository(ConnectionStringProvider.GetConnectionString());
        LoadStudents();
    }

    private void LoadStudents()
    {
        try
        {
            StatusTextBlock.Text = "Memuat data dari SQL Server...";
            var students = studentRepository.GetAll();
            StudentDataGrid.ItemsSource = students;
            TotalStudentsTextBlock.Text = $"Total Mahasiswa: {studentRepository.GetTotalCount()}";
            StatusTextBlock.Text = $"Data berhasil dimuat pada {DateTime.Now:dd/MM/yyyy HH:mm:ss}.";
        }
        catch (Exception ex)
        {
            StudentDataGrid.ItemsSource = null;
            TotalStudentsTextBlock.Text = "Total Mahasiswa: -";
            StatusTextBlock.Text = "Gagal memuat data. Periksa SQL Server dan connection string.";
            MessageBox.Show(ex.Message, "Database Error",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }

    private void RefreshButton_Click(object sender, RoutedEventArgs e)
    {
        LoadStudents();
    }
}
