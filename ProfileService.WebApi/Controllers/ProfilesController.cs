using AppResponseExtension.Response;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Application.Models.DTOs;
using ProfileService.Application.Services;
using ProfileService.Contracts.Models.Requests;
using ProfileService.Contracts.Models.Responces;

namespace ProfileService.WebApi.Controllers;

/// <inheritdoc />
[ApiController]
[Route("[controller]")]
public class ProfilesController : ControllerBase
{
    private readonly ProfilesService _profilesService;

    /// <inheritdoc />
    public ProfilesController(ProfilesService profilesService)
    {
        _profilesService = profilesService;
    }

    /// <summary>
    /// Получить профиль
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<BaseResponse<GetProfileResponse>> GetProfileAsync(CancellationToken ct)
    {
        var result = await _profilesService.GetProfileAsync();
        return AppResponse.Create(result);
    }

    /// <summary>
    /// Создание пользователя
    /// </summary>
    /// <param name="request">Параметры</param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<BaseResponse<GetProfileResponse>> CreateProfileAsync([FromBody] CreateProfileRequest request,
        CancellationToken ct)
    {
        var result = await _profilesService.CreateProfileAsync(new CreateProfileDto
        {
            Name = request.Name,
            Password = request.Password,
            TinkoffApiKey = request.TinkoffApiKey
        }, ct);
        return AppResponse.Create(result);
    }

    /// <summary>
    /// Удалить профиль
    /// </summary>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpDelete]
    public async Task<BaseResponse> DeleteProfileAsync(CancellationToken ct)
    {
        await _profilesService.DeleteProfileAsync(ct);
        return AppResponse.Create();
    }

    /// <summary>
    /// Обновить профиль пользователя
    /// </summary>
    /// <param name="request"></param>
    /// <param name="ct"></param>
    /// <returns></returns>
    [HttpPatch]
    public async Task<BaseResponse<GetProfileResponse>> UpdateProfileAsync([FromBody] UpdateProfileRequest request, CancellationToken ct)
    {
        var result = await _profilesService.UpdateProfileAsync(new UpdateProfileDto()
        {
            Name = request.Name,
            Password = request.Password,
        }, ct);
        return AppResponse.Create(result);
    }
}