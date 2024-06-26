using backend.BaseModule.Models.Media;
using backend.BaseModule.Controllers;
using backend.BaseModule.Models.Base;
using backend.Helpers;
using backend.Products.Interfaces.Shared;
using backend.Products.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace backend.Products.Controllers;

[Authorize("dashboard-product")]
[ApiController]
[Route("api/dashboard/product")]
public class DashboardProductController(
    IProductService productService
) : BaseController
{
    private readonly IProductService _productService = productService;

    [HttpGet]
    [Produces("application/json")]
    public async Task<ActionResult<PaginatedResponse<ProductResponse>>> GetPaginatedProduct([FromQuery] ProductFilter filter )
    {
        PaginatedResponse<ProductResponse> products = await _productService.GetPaginatedProduct(filter);
        return Ok(products);
    }

    [HttpGet("{id}")]
    [Produces("application/json")]
    public async Task<ActionResult<ProductSingleResponse>> GetProductById(Guid id)
    {
        ProductSingleResponse product = await _productService.GetProductById(id);
        return Ok(new { message = "success", data = product });
    }

    [HttpPost]
    [Produces("application/json")]
    public async Task<ActionResult<ProductSingleResponse>> CreateProduct(CreateProductRequest model)
    {
        ProductSingleResponse product = await _productService.CreateProduct(model, ipAddress());
        return Ok(new { message = "success", data = product });
    }

    [HttpPut("{id}")]
    [Produces("application/json")]
    public async Task<ActionResult<ProductSingleResponse>> UpdateProduct(Guid id, UpdateProductRequest model)
    {
        ProductSingleResponse product = await _productService.UpdateProduct(id, model, ipAddress());
        return Ok(new { message = "success", data = product });
    }

    [HttpDelete("{id}")]
    [Produces("application/json")]
    public async Task<ActionResult<ProductResponse>> DeleteProduct(Guid id)
    {
        await _productService.DeleteProduct(id, ipAddress());
        return Ok(new { message = "success" });
    }

    [HttpPost("thumbnail")]
    [RequestSizeLimit(2542880)]
    [Produces("application/json")]
        public async Task<ActionResult<ImageFullUploadResponse>> UploadThumbnail([FromForm] ImageUploadRequest model)
    {
        ImageFullUploadResponse media = await _productService.UploadThumbnail(model);
        return Ok(new { message = "success", data = media });
    }
    
    [HttpPost("icon")]
    [RequestSizeLimit(2542880)]
    [Produces("application/json")]
        public async Task<ActionResult<ImageFullUploadResponse>> UploadIcon([FromForm] ImageUploadRequest model)
    {
        ImageFullUploadResponse media = await _productService.UploadIcon(model);
        return Ok(new { message = "success", data = media });
    }
}