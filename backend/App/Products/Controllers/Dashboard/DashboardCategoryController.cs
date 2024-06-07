using backend.BaseModule.Controllers;
using backend.BaseModule.Models.Base;
using backend.Helpers;
using backend.Products.Interfaces.Shared;
using backend.Products.Models.Category;
using Microsoft.AspNetCore.Mvc;

namespace backend.Products.Controllers;

[Authorize("dashboard-category")]
[ApiController]
[Route("api/dashboard/category")]
public class DashboardCategoryController(
    ICategoryService categoryService
    ) : BaseController
{
    private readonly ICategoryService _categoryService = categoryService;

    [HttpGet]
    [Produces("application/json")]
    public async Task<ActionResult<PaginatedResponse<CategoryResponse>>> GetPaginatedCategory([FromQuery] CategoryFilter filter )
    {
        PaginatedResponse<CategoryResponse> categories = await _categoryService.GetPaginatedCategory(filter);
        return Ok(categories);
    }
}