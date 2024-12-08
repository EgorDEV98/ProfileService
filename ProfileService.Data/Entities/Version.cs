namespace ProfileService.Data.Entities;

/// <summary>
/// Версия
/// </summary>
public class Version
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public Guid Id { get; set; }
    
    /// <summary>
    /// Номер версии
    /// </summary>
    public int VersionNumber { get; set; }
    
    /// <summary>
    /// Установлено
    /// </summary>
    public bool IsInstalled { get; set; }
    
    /// <summary>
    /// Последнее обновление
    /// </summary>
    public DateTimeOffset LastUpdate { get; set; }
}