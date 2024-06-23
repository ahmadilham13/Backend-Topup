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

    [HttpGet("{id}")]
    [Produces("application/json")]
    public async Task<ActionResult<CategorySingleResponse>> GetCategoryById(Guid id)
    {
        CategorySingleResponse category = await _categoryService.GetCategoryById(id);
        return Ok(new { message = "success", data = category });
    }

    [HttpGet("select-data")]
    [Produces("application/json")]
    public async Task<ActionResult<List<SelectDataResponse>>> GetListCategory([FromQuery] ListFilter filter)
    {
        List<SelectDataResponse> category = await _categoryService.GetListCategory(filter);
        return Ok(new { message = "success", data = category });
    }

    [HttpPost]
    [Produces("application/json")]
    public async Task<ActionResult<CategoryResponse>> CreateCategory(CreateCategoryRequest model)
    {
        CategoryResponse category = await _categoryService.CreateCategory(model, ipAddress());
        return Ok(new { message = "success", data = category });
    }

    [HttpPut("{id}")]
    [Produces("application/json")]
    public async Task<ActionResult<CategoryResponse>> UpdateCategory(Guid id, UpdateCategoryRequest model)
    {
        CategoryResponse category = await _categoryService.UpdateCategory(id, model, ipAddress());
        return Ok(new { message = "success", data = category });
    }

    [HttpDelete("{id}")]
    [Produces("application/json")]
    public async Task<ActionResult<CategoryResponse>> DeleteCategory(Guid id)
    {
        await _categoryService.DeleteCategory(id, ipAddress());
        return Ok(new { message = "success" });
    }
}