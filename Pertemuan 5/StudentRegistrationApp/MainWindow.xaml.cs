using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using StudentRegistrationApp.Models;
using StudentRegistrationApp.Services;

namespace StudentRegistrationApp;

public partial class MainWindow : Window
{
    private readonly ObservableCollection<StudentRecord> students = [];
    private readonly ICollectionView studentView;
    private StudentRecord? editingStudent;

    public MainWindow()
    {
        InitializeComponent();
        StudentDataGrid.ItemsSource = students;
        lstMahasiswa.ItemsSource = students;
        studentView = CollectionViewSource.GetDefaultView(students);
        studentView.Filter = FilterStudent;
        UpdateStudentCount();
    }

    private void BtnSave_Click(object sender, RoutedEventArgs e)
    {
        StudentRecord candidate = ReadForm();
        string? validationMessage = StudentValidator.Validate(candidate);
        if (validationMessage is not null)
        {
            ShowValidation(validationMessage);
            return;
        }

        if (students.Any(student => student.Nim.Equals(candidate.Nim, StringComparison.OrdinalIgnoreCase)))
        {
            ShowValidation("NIM sudah terdaftar.");
            txtNim.Focus();
            return;
        }

        students.Add(candidate);
        UpdateStudentCount();
        ResetForm();
        MessageBox.Show("Data mahasiswa berhasil disimpan!", "Informasi",
            MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void BtnEdit_Click(object sender, RoutedEventArgs e)
    {
        if (StudentDataGrid.SelectedItem is not StudentRecord selected)
        {
            ShowValidation("Pilih data yang ingin diedit.");
            return;
        }

        editingStudent = selected;
        txtNim.Text = selected.Nim;
        txtName.Text = selected.Name;
        SelectProgramStudy(selected.ProgramStudy);
        rbMale.IsChecked = selected.Gender == "Laki-laki";
        rbFemale.IsChecked = selected.Gender == "Perempuan";
        dpBirthDate.SelectedDate = selected.BirthDate;
        txtAddress.Text = selected.Address;
        txtPhoneNumber.Text = selected.PhoneNumber;
        btnSave.IsEnabled = false;
        btnUpdate.IsEnabled = true;
        txtNim.Focus();
    }

    private void BtnUpdate_Click(object sender, RoutedEventArgs e)
    {
        if (editingStudent is null)
        {
            ShowValidation("Pilih data melalui tombol Edit Pilihan terlebih dahulu.");
            return;
        }

        StudentRecord candidate = ReadForm();
        string? validationMessage = StudentValidator.Validate(candidate);
        if (validationMessage is not null)
        {
            ShowValidation(validationMessage);
            return;
        }

        bool duplicateNim = students.Any(student =>
            student.Id != editingStudent.Id &&
            student.Nim.Equals(candidate.Nim, StringComparison.OrdinalIgnoreCase));
        if (duplicateNim)
        {
            ShowValidation("NIM sudah digunakan mahasiswa lain.");
            return;
        }

        editingStudent.Nim = candidate.Nim;
        editingStudent.Name = candidate.Name;
        editingStudent.ProgramStudy = candidate.ProgramStudy;
        editingStudent.Gender = candidate.Gender;
        editingStudent.BirthDate = candidate.BirthDate;
        editingStudent.Address = candidate.Address;
        editingStudent.PhoneNumber = candidate.PhoneNumber;
        studentView.Refresh();
        ResetForm();
        MessageBox.Show("Data mahasiswa berhasil diperbarui!", "Informasi",
            MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void BtnDelete_Click(object sender, RoutedEventArgs e)
    {
        if (StudentDataGrid.SelectedItem is not StudentRecord selected)
        {
            ShowValidation("Pilih data yang ingin dihapus.");
            return;
        }

        MessageBoxResult confirmation = MessageBox.Show(
            $"Hapus data {selected.Nim} - {selected.Name}?",
            "Konfirmasi Hapus", MessageBoxButton.YesNo, MessageBoxImage.Warning);
        if (confirmation != MessageBoxResult.Yes)
        {
            return;
        }

        students.Remove(selected);
        UpdateStudentCount();
        ResetForm();
    }

    private void BtnReset_Click(object sender, RoutedEventArgs e) => ResetForm();

    private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
    {
        studentView.Refresh();
    }

    private void StudentDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // Selection dipakai oleh aksi Edit dan Hapus. Event ini sengaja tersedia
        // agar status bar dapat dikembangkan tanpa mengubah struktur DataGrid.
    }

    private StudentRecord ReadForm()
    {
        return new StudentRecord
        {
            Nim = txtNim.Text.Trim(),
            Name = txtName.Text.Trim(),
            ProgramStudy = cmbProgramStudy.SelectedValue?.ToString() ?? string.Empty,
            Gender = rbMale.IsChecked == true
                ? "Laki-laki"
                : rbFemale.IsChecked == true ? "Perempuan" : string.Empty,
            BirthDate = dpBirthDate.SelectedDate,
            Address = txtAddress.Text.Trim(),
            PhoneNumber = txtPhoneNumber.Text.Trim()
        };
    }

    private bool FilterStudent(object item)
    {
        if (item is not StudentRecord student)
        {
            return false;
        }

        string keyword = txtSearch.Text.Trim();
        return string.IsNullOrWhiteSpace(keyword)
            || student.Nim.Contains(keyword, StringComparison.OrdinalIgnoreCase)
            || student.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase)
            || student.ProgramStudy.Contains(keyword, StringComparison.OrdinalIgnoreCase);
    }

    private void SelectProgramStudy(string programStudy)
    {
        cmbProgramStudy.SelectedItem = cmbProgramStudy.Items
            .OfType<ComboBoxItem>()
            .FirstOrDefault(item => string.Equals(
                item.Content?.ToString(), programStudy, StringComparison.Ordinal));
    }

    private void ResetForm()
    {
        txtNim.Clear();
        txtName.Clear();
        cmbProgramStudy.SelectedIndex = -1;
        rbMale.IsChecked = false;
        rbFemale.IsChecked = false;
        dpBirthDate.SelectedDate = null;
        txtAddress.Clear();
        txtPhoneNumber.Clear();
        StudentDataGrid.SelectedItem = null;
        editingStudent = null;
        btnSave.IsEnabled = true;
        btnUpdate.IsEnabled = false;
        txtNim.Focus();
    }

    private void UpdateStudentCount()
    {
        txtStudentCount.Text = $"Jumlah Mahasiswa: {students.Count}";
    }

    private static void ShowValidation(string message)
    {
        MessageBox.Show(message, "Validasi", MessageBoxButton.OK, MessageBoxImage.Warning);
    }
}
