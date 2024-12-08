namespace ProfileService.Data.Entities;

public class Profile
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
    public string Password { get; set; }
    
    /// <summary>
    /// Апи ключ
    /// </summary>
    public string TinkoffApiKey { get; set; }
    
    /// <summary>
    /// Дата создания
    /// </summary>
    public DateTimeOffset CreatedDate { get; set; }
    
    /// <summary>
    /// Последнее обновление
    /// </summary>
    public DateTimeOffset LastUpdate { get; set; }
}