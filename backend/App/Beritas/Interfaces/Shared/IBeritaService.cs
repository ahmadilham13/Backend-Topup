using backend.BaseModule.Models.Base;
using backend.BaseModule.Models.Media;
using backend.Beritas.Models.Request;
using backend.Beritas.Models.Response;

namespace backend.Products.Interfaces.Shared;

public interface IBeritaService
{
    Task<PaginatedResponse<BeritaResponse>> GetPaginatedBerita(BeritaFilter filter);
    Task<ImageFullUploadResponse> UploadPath(ImageUploadRequest model);

}