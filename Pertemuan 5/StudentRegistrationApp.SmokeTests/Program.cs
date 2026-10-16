using StudentRegistrationApp.Models;
using StudentRegistrationApp.Services;

var validStudent = new StudentRecord
{
    Nim = "20260001",
    Name = "Budi Santoso",
    ProgramStudy = "Teknik Informatika",
    Gender = "Laki-laki",
    BirthDate = new DateTime(2005, 1, 15),
    Address = "Surabaya",
    PhoneNumber = "0812-3456-7890"
};

if (StudentValidator.Validate(validStudent) is not null)
{
    throw new InvalidOperationException("Data valid ditolak validator.");
}

validStudent.Nim = string.Empty;
if (StudentValidator.Validate(validStudent) != "NIM harus diisi.")
{
    throw new InvalidOperationException("NIM kosong tidak terdeteksi.");
}

validStudent.Nim = "20260001";
validStudent.PhoneNumber = "telepon";
if (StudentValidator.Validate(validStudent) is null)
{
    throw new InvalidOperationException("Nomor telepon tidak valid tidak terdeteksi.");
}

Console.WriteLine("3 skenario validasi mahasiswa lulus.");
