namespace Hng13Stage0;

/// <summary>
/// Represents the response for the /me endpoint.
/// </summary>
public class UserResponse
{
    public string Status { get; set; } = "success";
    public UserInfo User { get; set; } = new();
    public string Timestamp { get; set; } = DateTime.UtcNow.ToString("O");
    public string? Fact { get; set; }
}

public class UserInfo
{
    public string Email { get; set; } = "amaechijude178@gmail.com";
    public string Name { get; set; } = "Amaechi Ugwu";
    public string Stack { get; set; } = ".NET 9 / C#";
}