using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pandu_Gogi_Backend.Data;
using Pandu_Gogi_Backend.Models.Dtos.Category;
using Pandu_Gogi_Backend.Models.Entites;

namespace Pandu_Gogi_Backend.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/category")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        AppDbContext db;

        public CategoryController(AppDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult GetAllCategory()
        {
            var category = db.categories;

            return Ok(category);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = db.categories.FirstOrDefault(x => x.id == id);

            if (category == null) return BadRequest(new { message = "Category not found" });

            return Ok(category);
        }

        [HttpPost]
        public IActionResult CreateCategory(CategoryDto categoryDto)
        {
            try
            {
                var category = new Category
                {
                    name = categoryDto.name
                };

                db.categories.Add(category);
                db.SaveChanges();

                return StatusCode(201, new { category = category });

            } catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateCategory(CategoryDto categoryDto, int id)
        {
            try
            {
                var category = db.categories.FirstOrDefault(x => x.id == id);

                if (category == null) return BadRequest(new { message = "Category not found" });

                category.name = categoryDto.name;
                db.SaveChanges();

                return Ok(new { category = category });

            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteCategory(int id)
        {
            try
            {
                var category = db.categories.FirstOrDefault(x => x.id == id);

                if (category == null) return BadRequest(new { message = "Category not found" });

                db.categories.Remove(category);
                db.SaveChanges();

                return StatusCode(204, new {message = "Delete data success"});

            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

    }
}
