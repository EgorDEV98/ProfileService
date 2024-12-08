namespace ProfileService.Contracts.Models.Responces;

public class GetProfileResponse
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Имя пользователя
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Пароль
    /// </summary>
    public string HashedPassword { get; set; }
    
    /// <summary>
    /// Апи ключ
    /// </summary>
    public string HashedTinkoffApiKey { get; set; }
    
    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTimeOffset CreatedDate { get; set; }
    
    /// <summary>
    /// Последнее обновление
    /// </summary>
    public DateTimeOffset LastUpdate { get; set; }
}