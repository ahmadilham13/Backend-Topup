using backend.BaseModule.Models.Base;
using backend.Beritas.Models.Request;
using backend.Beritas.Models.Response;

namespace backend.Products.Interfaces.Shared;

public interface IBeritaService
{
    Task<PaginatedResponse<BeritaResponse>> GetPaginatedBerita(BeritaFilter filter);

}