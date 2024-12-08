namespace ProfileService.Application.Models.DTOs;

public class UpdateProfileDto
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