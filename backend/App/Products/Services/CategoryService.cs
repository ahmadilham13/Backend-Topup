using AutoMapper;
using backend.BaseModule.Models.Base;
using backend.Entities;
using backend.Helpers;
using backend.Products.Interfaces.Repositories;
using backend.Products.Interfaces.Shared;
using backend.Products.Models.Category;
using backend.Products.Models.Product;
using Microsoft.Extensions.Localization;

namespace backend.Products.Services;

public class CategoryService : ICategoryService
{
    private Account _account;
    private readonly IMapper _mapper;
    private readonly ICategoryRepo _categoryRepo;
    private readonly IProductRepo _productRepo;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IStringLocalizer<CategoryService> _localizer;

    public CategoryService(
        IMapper mapper,
        ICategoryRepo categoryRepo,
        IProductRepo productRepo,
        IHttpContextAccessor httpContextAccessor,
        IStringLocalizer<CategoryService> localizer
    )
    {
        _mapper = mapper;
        _categoryRepo = categoryRepo;
        _productRepo = productRepo;
        _httpContextAccessor = httpContextAccessor;
        _localizer = localizer;
        _account = (Account)_httpContextAccessor.HttpContext.Items["Account"];
    }

    public async Task<List<SelectDataResponse>> GetListCategory(ListFilter filter)
    {
        List<Category> categories = await _categoryRepo.GetAllCategories(filter);
        return _mapper.Map<List<SelectDataResponse>>(categories);
    }

    public async Task<PaginatedResponse<CategoryResponse>> GetPaginatedCategory(CategoryFilter filter)
    {
        int pageSize = SiteHelper.ValidatePageSize(filter.pageSize);
        int pageIndex = filter.page > 1 ? filter.page : 1;

        (List<Category> result, int count, int totalPages) = await _categoryRepo.GetPaginatedCategories(filter, pageIndex, pageSize);

        return mappingCategoryPaginatedResponse(result, pageIndex, totalPages, count);
    }

    public async Task<CategorySingleResponse> GetCategoryById(Guid id)
    {
        Category category = await getCategory(id);
        return _mapper.Map<CategorySingleResponse>(category);
    }

    public async Task<CategoryResponse> CreateCategory(CreateCategoryRequest model, string ipAddress)
    {
        // Check if category Name already exists on database
        if (await _categoryRepo.CheckCategoryNameExist(model.Name))
        {
            throw new AppException(_localizer["duplicate_category_name"], "duplicate_category_name");
        }

        Category category = _mapper.Map<Category>(model);
        category.AuthorId = _account.Id;

        await _categoryRepo.CreateCategory(category);

        return _mapper.Map<CategoryResponse>(category);
    }

    public async Task<CategoryResponse> UpdateCategory(Guid id, UpdateCategoryRequest model, string ipAddress)
    {
        Category category = await getCategory(id);

        if (!string.IsNullOrEmpty(model.Name))
        {
            category.Name = model.Name;
        }

        category.Description = model.Description;
        category.Updated = DateTime.UtcNow;

        await _categoryRepo.UpdateCategory(category);

        return _mapper.Map<CategoryResponse>(category);
    }

    public async Task DeleteCategory(Guid id , string ipAddress)
    {
        Category category = await getCategory(id);

        category.Name = "[DELETED]_" + category.Name;
        category.Updated = DateTime.UtcNow;
        category.DeletedAt = DateTime.UtcNow;

        await _categoryRepo.UpdateCategory(category);
    }

    public async Task<List<CategoryResponse>> GetAllCategory()
    {
        List<Category> categories = await _categoryRepo.GetAllCategories();
        return _mapper.Map<List<CategoryResponse>>(categories);
    }

    public async Task<CategoryProductPaginateResponse> GetFullCategory(Guid id, ProductFilter filter)
    {
        Category categories = await _categoryRepo.GetFullCategory(id);
        CategoryProductPaginateResponse response = _mapper.Map<CategoryProductPaginateResponse>(categories);

        int pageSize = SiteHelper.ValidatePageSize(filter.pageSize);
        int pageIndex = filter.page > 1 ? filter.page : 1;

        (List<Product> result, int count, int totalPages) = await _productRepo.GetPaginatedProductsByCategory(id, filter, pageIndex, pageSize);

        response.Products = mappingProductPaginatedResponse(result, pageIndex, totalPages, count);
        
        return response;
    }

    private PaginatedResponse<CategoryResponse> mappingCategoryPaginatedResponse(IEnumerable<Category> categories, int page, int totalPage, int totalCount)
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

    private PaginatedResponse<CategoryProductResponse> mappingProductPaginatedResponse(IEnumerable<Product> products, int page, int totalPage, int totalCount)
    {
        return new PaginatedResponse<CategoryProductResponse>()
        {
            Message = "List Products",
            Data = _mapper.Map<IEnumerable<CategoryProductResponse>>(products),
            Page = page,
            TotalPage = totalPage,
            TotalCount = totalCount
        };
    }

    private async Task<Category> getCategory(Guid id)
    {
        Category category = await _categoryRepo.GetCategory(id) ?? throw new KeyNotFoundException(_localizer["category_not_found"]);
        return category;
    }
}