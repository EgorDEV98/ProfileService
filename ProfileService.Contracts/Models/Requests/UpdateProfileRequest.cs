namespace ProfileService.Contracts.Models.Requests;

public class UpdateProfileRequest
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string? Name { get; set; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    public string? Password { get; set; }
}