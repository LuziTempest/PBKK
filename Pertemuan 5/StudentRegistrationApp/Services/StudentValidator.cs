using StudentRegistrationApp.Models;

namespace StudentRegistrationApp.Services;

public static class StudentValidator
{
    public static string? Validate(StudentRecord student)
    {
        if (string.IsNullOrWhiteSpace(student.Nim))
        {
            return "NIM harus diisi.";
        }

        if (string.IsNullOrWhiteSpace(student.Name))
        {
            return "Nama mahasiswa harus diisi.";
        }

        if (string.IsNullOrWhiteSpace(student.ProgramStudy))
        {
            return "Program studi harus dipilih.";
        }

        if (string.IsNullOrWhiteSpace(student.Gender))
        {
            return "Jenis kelamin harus dipilih.";
        }

        if (student.BirthDate is null)
        {
            return "Tanggal lahir harus dipilih.";
        }

        if (student.BirthDate.Value.Date > DateTime.Today)
        {
            return "Tanggal lahir tidak boleh berada di masa depan.";
        }

        if (string.IsNullOrWhiteSpace(student.Address))
        {
            return "Alamat harus diisi.";
        }

        if (string.IsNullOrWhiteSpace(student.PhoneNumber))
        {
            return "Nomor telepon harus diisi.";
        }

        if (!student.PhoneNumber.All(character =>
                char.IsDigit(character) || character is '+' or '-' or ' '))
        {
            return "Nomor telepon hanya boleh berisi angka, spasi, tanda +, atau tanda -.";
        }

        return null;
    }
}
