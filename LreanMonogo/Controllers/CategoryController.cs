using Microsoft.AspNetCore.Mvc;
using MyFinancePal.Models;
using MyFinancePal.Services;

namespace MyFinancePal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;  
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<List<Category>> GetAllAsync(string userId) =>
           await _categoryService.GetAllAsync(userId);

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Category>> Get(string id)
        {
            var category = await _categoryService.GetAsync(id);

            if (category is null)
            {
                return NotFound();
            }

            return category;
        }


        [HttpPost]
        public async Task<IActionResult> Post(Category newsT)
        {
            await _categoryService.Create(newsT);

            return Ok(await _categoryService.GetAllAsync(newsT.UserId));
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Category updateT)
        {
            var category = await _categoryService.GetAsync(id);

            if (category is null)
            {
                return NotFound();
            }

            updateT.Id = category.Id;

            await _categoryService.Update(id, updateT);

            return Ok(await _categoryService.GetAllAsync(updateT.UserId));
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Remove(string id, string userId)
        {
            var category = await _categoryService.GetAsync(id);

            if (category is null)
            {
                return NotFound();
            }

            await _categoryService.Remove(id);

            return Ok(await _categoryService.GetAllAsync(userId));
        }
    }
}
