using backend.BaseModule.Controllers;
using backend.BaseModule.Models.Base;
using backend.BaseModule.Models.Media;
using backend.Beritas.Models.Request;
using backend.Beritas.Models.Response;
using backend.Helpers;
using backend.Products.Interfaces.Shared;
using Microsoft.AspNetCore.Mvc;

namespace backend.Products.Controllers;

[Authorize("dashboard-berita")]
[ApiController]
[Route("api/dashboard/berita")]
public class DashboardBeritaController(
    IBeritaService beritaService
    ) : BaseController
{
    private readonly IBeritaService _beritaService = beritaService;

    [HttpGet]
    [Produces("application/json")]
    public async Task<ActionResult<PaginatedResponse<BeritaResponse>>> GetPaginatedBerita([FromQuery] BeritaFilter filter)
    {
        PaginatedResponse<BeritaResponse> beritas = await _beritaService.GetPaginatedBerita(filter);
        return Ok(beritas);
    }

    [HttpPost("path")]
    [RequestSizeLimit(2542880)]
    [Produces("application/json")]
        public async Task<ActionResult<ImageFullUploadResponse>> UploadPath([FromForm] ImageUploadRequest model)
    {
        ImageFullUploadResponse media = await _beritaService.UploadPath(model);
        return Ok(new { message = "success", data = media });
    }
}