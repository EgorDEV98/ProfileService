using AppResponseExtension.Response;
using Microsoft.AspNetCore.Mvc;
using ProfileService.Application.Services;

namespace ProfileService.WebApi.Controllers;

public class SystemController
{
    private readonly SystemService _systemService;

    public SystemController(SystemService systemService)
    {
        _systemService = systemService;
    }

    [HttpGet("getVersion")]
    public async Task<BaseResponse<int>> GetVersionAsync(CancellationToken ct)
    {
        var result = await _systemService.GetVersionAsync(ct);
        return AppResponse.Create(result);
    }
}