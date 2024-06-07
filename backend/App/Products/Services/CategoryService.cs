using AutoMapper;
using backend.BaseModule.Models.Base;
using backend.Entities;
using backend.Helpers;
using backend.Products.Interfaces.Repositories;
using backend.Products.Interfaces.Shared;
using backend.Products.Models.Category;
using Microsoft.Extensions.Localization;

namespace backend.Products.Services;

public class CategoryService : ICategoryService
{
    private Account _account;
    private readonly IMapper _mapper;
    private readonly ICategoryRepo _categoryRepo;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IStringLocalizer<CategoryService> _localizer;

    public CategoryService(
        IMapper mapper,
        ICategoryRepo categoryRepo,
        IHttpContextAccessor httpContextAccessor,
        IStringLocalizer<CategoryService> localizer
    )
    {
        _mapper = mapper;
        _categoryRepo = categoryRepo;
        _httpContextAccessor = httpContextAccessor;
        _localizer = localizer;
        _account = (Account)_httpContextAccessor.HttpContext.Items["Account"];
    }

    public async Task<PaginatedResponse<CategoryResponse>> GetPaginatedCategory(CategoryFilter filter)
    {
        int pageSize = SiteHelper.ValidatePageSize(filter.pageSize);
        int pageIndex = filter.page > 1 ? filter.page : 1;

        (List<Category> result, int count, int totalPages) = await _categoryRepo.GetPaginatedCategories(filter, pageIndex, pageSize);

        return mappingBrandPaginatedResponse(result, pageIndex, totalPages, count);
    }

        private PaginatedResponse<CategoryResponse> mappingBrandPaginatedResponse(IEnumerable<Category> categories, int page, int totalPage, int totalCount)
    {
        return new PaginatedResponse<CategoryResponse>()
        {
            Message = "success",
            Data = _mapper.Map<IEnumerable<CategoryResponse>>(categories),
            Page = page,
            TotalPage = totalPage,
            TotalCount = totalCount
        };
    }
}