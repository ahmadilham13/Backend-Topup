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

    public async Task<ProductSingleResponse> CreateProduct(CreateProductRequest model, string ipAddress)
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

        if (model.ProductItems != null)
        {
            foreach (var item in model.ProductItems)
            {
                ProductItem productItem = new()
                {
                    Id = Guid.NewGuid(),
                    Name = item.Name,
                    Provider = item.Provider,
                    Price = item.Price,
                    SalePrice = item.SalePrice,
                    Status = item.Status,
                    ProductId = product.Id,
                    Created = DateTime.UtcNow
                };

                await _productRepo.CreateProductItem(productItem);
            }
        }

        return _mapper.Map<ProductSingleResponse>(product);
    }

    public async Task<ProductSingleResponse> UpdateProduct(Guid id, UpdateProductRequest model, string ipAddress)
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

        product.Status = model.Status;
        product.Updated = DateTime.UtcNow;

        await _productRepo.UpdateProduct(product);

        List<ProductItem> productItems = [];

        if (model.ProductItems != null)
        {
            foreach (var item in model.ProductItems)
            {
                // Check if ProductItem object exists
                // or create a new one if it doesn't exist
                ProductItem productItem = await _productRepo.GetProductItem(item.Id ?? Guid.Empty) ?? new ProductItem
                {
                    Id = Guid.NewGuid(),
                    ProductId = product.Id,
                    Created = DateTime.UtcNow
                };

                productItem.Name = item.Name;
                productItem.Provider = item.Provider;
                productItem.Price = item.Price;
                productItem.SalePrice = item.SalePrice;
                productItem.Status = item.Status;
                productItem.Updated = DateTime.UtcNow;

                if (item.Id == null)
                {
                    await _productRepo.CreateProductItem(productItem);
                }
                else
                {
                    await _productRepo.UpdateProductItem(productItem);
                }

                productItems.Add(productItem);
            }
        }

        await removeProductItem(product.Id, productItems);

        product.ProductItems = productItems;

        return _mapper.Map<ProductSingleResponse>(product);
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

    private async Task removeProductItem(Guid productId, List<ProductItem> model)
    {
        List<Guid> productItemId = [];

        // Get list Product Item IDs from request
        if (model != null && model.Count > 0)
        {
            productItemId = model.Where( x => x.Id != Guid.Empty ).Select( x => x.Id ).ToList();   
        }
        
        // Get list Product Item with current ID from db that don't exist on list Product Item IDs
        var removedProductItem = await _productRepo.GetProductItemByProductExcept(productId, productItemId);
        
        // Remove Product Item with current ID from db that don't exist on list Product Item IDs
        if (removedProductItem != null)
        {
            foreach (var item in removedProductItem)
            {
                item.DeletedAt = DateTime.UtcNow;

                await _productRepo.UpdateProductItem(item);
            }
        }
    }
}