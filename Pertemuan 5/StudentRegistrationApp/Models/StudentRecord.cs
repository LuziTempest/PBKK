namespace StudentRegistrationApp.Models;

public sealed class StudentRecord
{
    public Guid Id { get; init; } = Guid.NewGuid();
    public string Nim { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string ProgramStudy { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }
    public string Address { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
}
