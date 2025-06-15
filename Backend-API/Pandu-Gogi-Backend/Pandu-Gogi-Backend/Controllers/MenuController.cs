using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pandu_Gogi_Backend.Data;
using Pandu_Gogi_Backend.Models.Dtos.Menu;
using Pandu_Gogi_Backend.Models.Entites;

namespace Pandu_Gogi_Backend.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/menu")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        AppDbContext db;

        public MenuController(AppDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult GetAllMenu()
        {
            var menu = db.menus.Include(x => x.category).ToList();
            return Ok(menu);
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetMenuById(int id)
        {
            var menu = db.menus.FirstOrDefault(x => x.id == id);

            if (menu == null) return NotFound(new { message = "Menu not found" });

            return Ok(menu);
        }

        [HttpPost]
        public IActionResult CreateMenu(MenuDto menuDto)
        {
            try
            {
                var menu = new Menu
                {
                    category_id = menuDto.category_id,
                    name = menuDto.name,
                    description = menuDto.description,
                    price = menuDto.price,
                    image_url = menuDto.image_url,
                };

                db.menus.Add(menu);
                db.SaveChanges();

                return StatusCode(201, new {menu = menu});

            } catch (Exception err)
            {
                return BadRequest(err.InnerException.Message);
            }
        }

        [HttpPut]
        [Route("{id:int}")]
        public IActionResult UpdateMenu(MenuDto menuDto, int id)
        {
            try
            {
                var menu = db.menus.FirstOrDefault(x => x.id == id);

                if (menu == null) return NotFound(new { message = "Menu not found" });

                menu.category_id = menuDto.category_id;
                menu.name = menuDto.name;
                menu.description = menuDto.description;
                menu.price = menuDto.price;
                menu.image_url = menuDto.image_url;

                db.SaveChanges();

                return Ok(new { menu = menu });
            } catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteMenu(int id)
        {
            try
            {
                var menu = db.menus.FirstOrDefault(x => x.id == id);

                if (menu == null) return NotFound(new { message = "Menu not found" });

                db.menus.Remove(menu);
                db.SaveChanges();

                return StatusCode(204, new { message = "Delete data success" });

            } catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }


    }
}
