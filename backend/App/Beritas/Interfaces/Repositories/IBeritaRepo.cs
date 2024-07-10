using backend.Beritas.Models.Request;
using backend.Entities;

namespace backend.Beritas.Interfaces.Repositories;

public interface IBeritaRepo
{
    Task<(List<Berita>, int, int)> GetPaginatedBeritas(BeritaFilter filter, int pageIndex, int pageSize);

}
