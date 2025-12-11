namespace Fleet.API.Models;

public class AlertMessage
{
    public string Type { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow.AddYears(-2).AddHours(3);
}
