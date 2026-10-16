using System.IO;
using System.Text.Json;

namespace StudentRegistration.Configuration;

public static class ConnectionStringProvider
{
    public const string EnvironmentVariableName = "STUDENT_REGISTRATION_CONNECTION_STRING";

    public static string GetConnectionString()
    {
        string? environmentValue = Environment.GetEnvironmentVariable(EnvironmentVariableName);
        if (!string.IsNullOrWhiteSpace(environmentValue))
        {
            return environmentValue;
        }

        string settingsPath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
        if (!File.Exists(settingsPath))
        {
            throw new FileNotFoundException(
                "File appsettings.json tidak ditemukan pada direktori output aplikasi.", settingsPath);
        }

        using JsonDocument document = JsonDocument.Parse(File.ReadAllText(settingsPath));
        if (document.RootElement.TryGetProperty("ConnectionStrings", out JsonElement connectionStrings)
            && connectionStrings.TryGetProperty("StudentRegistrationDb", out JsonElement value)
            && !string.IsNullOrWhiteSpace(value.GetString()))
        {
            return value.GetString()!;
        }

        throw new InvalidOperationException(
            "ConnectionStrings:StudentRegistrationDb belum dikonfigurasi pada appsettings.json.");
    }
}
