using AppResponseExtension.Exceptions;
using Microsoft.EntityFrameworkCore;
using ProfileService.Application.Common;
using ProfileService.Application.Models.DTOs;
using ProfileService.Contracts.Models.Responces;
using ProfileService.Data;
using ProfileService.Data.Entities;

namespace ProfileService.Application.Services;

public class ProfilesService
{
    private readonly ProfileServiceDbContext _context;
    private readonly IEncryptor _encryptor;
    private readonly IDateTimeProvider _dateTimeProvider;

    public ProfilesService(ProfileServiceDbContext context, IEncryptor encryptor, IDateTimeProvider dateTimeProvider)
    {
        _context = context;
        _encryptor = encryptor;
        _dateTimeProvider = dateTimeProvider;
    }
    
    /// <summary>
    /// Получить профиль пользователя
    /// </summary>
    /// <returns></returns>
    public async Task<GetProfileResponse> GetProfileAsync()
    {
        var profile = await _context.Profiles.FirstOrDefaultAsync();
        return new GetProfileResponse()
        {
            Id = profile!.Id,
            Name = profile.Name,
            LastUpdate = profile.LastUpdate,
            CreatedDate = profile.CreatedDate
        };
    }

    /// <summary>
    /// Создать профиль
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<GetProfileResponse> CreateProfileAsync(CreateProfileDto dto, CancellationToken ct)
    {
        var isCreated = await _context.Profiles.AnyAsync(ct);
        if (isCreated)
        {
            ConflictException.Throw("Profile already exists");
        }

        var profile = new Profile()
        {
            Name = dto.Name,
            Password = _encryptor.Encrypt(dto.Password)!,
            TinkoffApiKey = _encryptor.Encrypt(dto.TinkoffApiKey)!,
            CreatedDate = _dateTimeProvider.GetDateTimeNow(),
            LastUpdate = _dateTimeProvider.GetDateTimeNow(),
        };
        
        await _context.Profiles.AddAsync(profile, ct);
        await _context.SaveChangesAsync(ct);

        return new GetProfileResponse()
        {
            Id = profile.Id,
            Name = profile.Name,
            LastUpdate = profile.LastUpdate,
            CreatedDate = profile.CreatedDate
        };
    }

    /// <summary>
    /// Удалить профиль
    /// </summary>
    /// <param name="ct"></param>
    public async Task<bool> DeleteProfileAsync(CancellationToken ct)
    {
        var profiles = await _context.Profiles.ToArrayAsync(ct);
        
        // Range на случае если профилей создалось несколько
        _context.Profiles.RemoveRange(profiles);
        await _context.SaveChangesAsync(ct);

        return true;
    }

    /// <summary>
    /// Обновить профиль
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    public async Task<GetProfileResponse> UpdateProfileAsync(UpdateProfileDto dto, CancellationToken ct)
    {
        var profile = await _context.Profiles.FirstOrDefaultAsync(ct);
        
        profile!.Name = dto.Name ?? profile.Name;
        profile.Password = _encryptor.Encrypt(dto.Password) ?? profile.Password;
        
        await _context.SaveChangesAsync(ct);

        return new GetProfileResponse()
        {
            Id = profile.Id,
            Name = profile.Name,
            CreatedDate = profile.CreatedDate,
            LastUpdate = profile.LastUpdate,
        };
    }
}