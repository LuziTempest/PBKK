namespace StudentRegistration.Models;

public sealed class Program
{
    public int ProgramId { get; set; }
    public string ProgramCode { get; set; } = string.Empty;
    public string ProgramName { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
