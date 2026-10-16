using Microsoft.Data.SqlClient;
using StudentRegistration.Models;

namespace StudentRegistration.Repositories;

public sealed class StudentRepository
{
    private readonly string connectionString;

    public StudentRepository(string connectionString)
    {
        this.connectionString = !string.IsNullOrWhiteSpace(connectionString)
            ? connectionString
            : throw new ArgumentException("Connection string tidak boleh kosong.", nameof(connectionString));
    }

    public List<Student> GetAll()
    {
        const string sql = """
            SELECT
                student.StudentId,
                student.NIM,
                student.Name,
                student.ProgramId,
                program.ProgramName,
                student.BirthDate,
                student.Address,
                student.PhoneNumber,
                student.CreatedAt,
                student.UpdatedAt
            FROM dbo.Students student
            INNER JOIN dbo.Programs program
                ON student.ProgramId = program.ProgramId
            WHERE program.IsActive = @IsActive
            ORDER BY student.StudentId;
            """;

        var students = new List<Student>();
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@IsActive", true);

        connection.Open();
        using SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
            students.Add(MapStudent(reader));
        }

        return students;
    }

    public int GetTotalCount()
    {
        const string sql = "SELECT COUNT(*) FROM dbo.Students;";
        using var connection = new SqlConnection(connectionString);
        using var command = new SqlCommand(sql, connection);
        connection.Open();
        return Convert.ToInt32(command.ExecuteScalar());
    }

    private static Student MapStudent(SqlDataReader reader)
    {
        return new Student
        {
            StudentId = reader.GetInt32(reader.GetOrdinal("StudentId")),
            NIM = reader.GetString(reader.GetOrdinal("NIM")),
            Name = reader.GetString(reader.GetOrdinal("Name")),
            ProgramId = reader.GetInt32(reader.GetOrdinal("ProgramId")),
            ProgramName = reader.GetString(reader.GetOrdinal("ProgramName")),
            BirthDate = reader.IsDBNull(reader.GetOrdinal("BirthDate"))
                ? null
                : reader.GetDateTime(reader.GetOrdinal("BirthDate")),
            Address = reader.IsDBNull(reader.GetOrdinal("Address"))
                ? string.Empty
                : reader.GetString(reader.GetOrdinal("Address")),
            PhoneNumber = reader.IsDBNull(reader.GetOrdinal("PhoneNumber"))
                ? string.Empty
                : reader.GetString(reader.GetOrdinal("PhoneNumber")),
            CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
            UpdatedAt = reader.IsDBNull(reader.GetOrdinal("UpdatedAt"))
                ? null
                : reader.GetDateTime(reader.GetOrdinal("UpdatedAt"))
        };
    }
}
