namespace ProfileService.Application.Models.DTOs;

public class CreateProfileDto
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public required string Name { get; set; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    public required string Password { get; set; }
    
    /// <summary>
    /// Апи ключ
    /// </summary>
    public required string TinkoffApiKey { get; set; }
}