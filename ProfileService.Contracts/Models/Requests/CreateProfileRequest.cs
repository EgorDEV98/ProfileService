using System.ComponentModel.DataAnnotations;

namespace ProfileService.Contracts.Models.Requests;

public class CreateProfileRequest
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    [Required]
    public required string Name { get; set; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    [Required]
    public required string Password { get; set; }
    
    /// <summary>
    /// Апи ключ
    /// </summary>
    [Required]
    public required string TinkoffApiKey { get; set; }
}