using backend.BaseModule.Controllers;
using backend.Products.Interfaces.Shared;
using backend.Products.Models.Category;
using backend.Products.Models.Product;
using Microsoft.AspNetCore.Mvc;

namespace backend.Products.Controllers;

[ApiController]
[Route("api/category")]
public class CategoryController(
    ICategoryService categoryService
    ) : BaseController
{
    private readonly ICategoryService _categoryService = categoryService;

    [HttpGet]
    [Produces("application/json")]
    public async Task<ActionResult<List<CategoryResponse>>> GetAllCategory()
    {
        List<CategoryResponse> categories = await _categoryService.GetAllCategory();
        return Ok(categories);
    }
    [HttpGet("{id}")]
    [Produces("application/json")]
    public async Task<ActionResult<CategoryProductPaginateResponse>> GetFullCategory(Guid id, [FromQuery] ProductFilter filter)
    {
        CategoryProductPaginateResponse categories = await _categoryService.GetFullCategory(id, filter);
        return Ok(new { message = "success", data = categories });
    }
}
