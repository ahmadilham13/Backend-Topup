using AutoMapper;
using backend.BaseModule.Interfaces.Shared;
using backend.BaseModule.Models.Base;
using backend.BaseModule.Models.Media;
using backend.Beritas.Interfaces.Repositories;
using backend.Beritas.Models.Request;
using backend.Beritas.Models.Response;
using backend.Entities;
using backend.Helpers;
using backend.Products.Interfaces.Shared;
using Microsoft.Extensions.Localization;

namespace backend.Beritas.Services;

public class BeritaService : IBeritaService
{
    private Account _account;
    private readonly IMapper _mapper;
    private readonly IBeritaRepo _beritaRepo;
    private readonly IUploadService _uploadService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IStringLocalizer<BeritaService> _localizer;

    public BeritaService(
        IMapper mapper,
        IBeritaRepo beritaRepo,
        IUploadService uploadService,
        IHttpContextAccessor httpContextAccessor,
        IStringLocalizer<BeritaService> localizer
    )
    {
        _mapper = mapper;
        _beritaRepo = beritaRepo;
        _uploadService = uploadService;
        _httpContextAccessor = httpContextAccessor;
        _localizer = localizer;
        _account = (Account)_httpContextAccessor.HttpContext.Items["Account"];
    }

    public async Task<PaginatedResponse<BeritaResponse>> GetPaginatedBerita(BeritaFilter filter)
    {
        int pageSize = SiteHelper.ValidatePageSize(filter.pageSize);
        int pageIndex = filter.page > 1 ? filter.page : 1;

        (List<Berita> result, int count, int totalPages) = await _beritaRepo.GetPaginatedBeritas(filter, pageIndex, pageSize);

        return mappingBeritaPaginatedResponse(result, pageIndex, totalPages, count);
    }

    public async Task<ImageFullUploadResponse> UploadPath(ImageUploadRequest model)
    {
        Media uploadImage = await _uploadService.UploadImage(model.image, "beritas");
        return _mapper.Map<ImageFullUploadResponse>(uploadImage);
    }

    private PaginatedResponse<BeritaResponse> mappingBeritaPaginatedResponse(IEnumerable<Berita> beritas, int page, int totalPage, int totalCount)
    {
        return new PaginatedResponse<BeritaResponse>()
        {
            Message = "success",
            Data = _mapper.Map<IEnumerable<BeritaResponse>>(beritas),
            Page = page,
            TotalPage = totalPage,
            TotalCount = totalCount
        };
    }
}
