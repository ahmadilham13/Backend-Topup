using AutoMapper;
using backend.BaseModule.Models.Base;
using backend.Entities;
using backend.Helpers;
using backend.Products.Interfaces.Repositories;
using backend.Products.Interfaces.Shared;
using backend.Products.Models.Product;
using Microsoft.Extensions.Localization;

namespace backend.Products.Services;

public class ProductService : IProductService
{
    private Account _account;
    private readonly IMapper _mapper;
    private readonly IProductRepo _productRepo;
    private readonly ICategoryRepo _categorytRepo;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IStringLocalizer<ProductService> _localizer;

    public ProductService(
        IMapper mapper,
        IProductRepo productRepo,
        ICategoryRepo categoryRepo,
        IHttpContextAccessor httpContextAccessor,
        IStringLocalizer<ProductService> localizer
    )
    {
         _mapper = mapper;
        _productRepo = productRepo;
        _categorytRepo = categoryRepo;
        _httpContextAccessor = httpContextAccessor;
        _localizer = localizer;
        _account = (Account)_httpContextAccessor.HttpContext.Items["Account"];
    }

        public async Task<PaginatedResponse<ProductResponse>> GetPaginatedProduct(ProductFilter filter)
    {
        int pageSize = SiteHelper.ValidatePageSize(filter.pageSize);
        int pageIndex = filter.page > 1 ? filter.page : 1;

        (List<Product> result, int count, int totalPages) = await _productRepo.GetPaginatedProducts(filter, pageIndex, pageSize);

        return mappingProductPaginatedResponse(result, pageIndex, totalPages, count);
    }

    public async Task<ProductSingleResponse> GetProductById(Guid id)
    {
        Product product = await getProduct(id);
        return _mapper.Map<ProductSingleResponse>(product);
    }

    public async Task<ProductResponse> CreateProduct(CreateProductRequest model, string ipAddress)
    {
        // Check if category Name already exists on database
        if (await _productRepo.CheckProductNameExist(model.Name))
        {
            throw new AppException(_localizer["duplicate_product_name"], "duplicate_product_name");
        }

        if (!await _categorytRepo.CheckCategoryExist(model.CategoryId))
        {
            throw new AppException(_localizer["category_not_found"], "category_not_found");
        }

        Product product = _mapper.Map<Product>(model);
        product.AuthorId = _account.Id;

        await _productRepo.CreateProduct(product);
        return _mapper.Map<ProductResponse>(product);
    }

    public async Task<ProductResponse> UpdateProduct(Guid id, UpdateProductRequest model, string ipAddress)
    {
        Product product = await getProduct(id);

        if (!string.IsNullOrEmpty(model.Name))
        {
            product.Name = model.Name;
        }

        if (!string.IsNullOrEmpty(model.Description))
        {
            product.Description = model.Description;
        }

        if (model.CategoryId != null)
        {
            if (! await _categorytRepo.CheckCategoryExist((Guid)model.CategoryId))
            {
                throw new AppException(_localizer["category_not_found"], "category_not_found");
            }

            product.CategoryId = (Guid) model.CategoryId;
        }

        product.Updated = DateTime.UtcNow;

        await _productRepo.UpdateProduct(product);

        return _mapper.Map<ProductResponse>(product);
    }

    public async Task DeleteProduct(Guid id, string ipAddress)
    {
        Product product = await getProduct(id);

        product.Name = "[DELETED]_" + product.Name;
        product.Updated = DateTime.UtcNow;
        product.DeletedAt = DateTime.UtcNow;

        await _productRepo.UpdateProduct(product);
    }

    private PaginatedResponse<ProductResponse> mappingProductPaginatedResponse(IEnumerable<Product> products, int page, int totalPage, int totalCount)
    {
        return new PaginatedResponse<ProductResponse>()
        {
            Message = "success",
            Data = _mapper.Map<IEnumerable<ProductResponse>>(products),
            Page = page,
            TotalPage = totalPage,
            TotalCount = totalCount
        };
    }

    private async Task<Product> getProduct(Guid id)
    {
        Product product = await _productRepo.GetProduct(id) ?? throw new KeyNotFoundException(_localizer["product_not_found"]);
        return product;
    }
}