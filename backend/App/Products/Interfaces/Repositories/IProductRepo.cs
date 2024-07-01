using backend.Entities;
using backend.Products.Models.Product;

namespace backend.Products.Interfaces.Repositories;

public interface IProductRepo
{
    Task<(List<Product>, int, int)> GetPaginatedProducts(ProductFilter filter, int pageIndex, int pageSize);
    Task<Product> GetProduct(Guid id);
    Task<ProductItem> GetProductItem(Guid id);
    Task<List<ProductItem>> GetProductItemByProductExcept(Guid productId, List<Guid> productItemIds);
    Task<bool> CheckProductNameExist(string name);
    Task CreateProduct(Product product);
    Task UpdateProduct(Product product);
    Task CreateProductItem(ProductItem productItem);
    Task UpdateProductItem(ProductItem productItem);
}