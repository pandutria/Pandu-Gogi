using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pandu_Gogi_Backend.Data;
using Pandu_Gogi_Backend.Models.Dtos.OrderHeader;
using Pandu_Gogi_Backend.Models.Dtos.User;
using Pandu_Gogi_Backend.Models.Entites;

namespace Pandu_Gogi_Backend.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/orderHeader")]
    [ApiController]
    public class OrderHeaderController : ControllerBase
    {
        AppDbContext db;

        public OrderHeaderController(AppDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult GetAllOrder()
        {
            var order = db.orderHeaders.Include(x => x.user);
            return Ok(order);
        }

        [Authorize]
        [HttpPost]
        public IActionResult CreateOrder(OrderHeaderDto orderHeaderDto)
        {
            try
            {
                var user_id = User.FindFirst("id").Value;
                var user = db.users.FirstOrDefault(x => x.id.ToString() == user_id);

                var order = new OrderHeader
                {
                    user_id = user.id,
                    date = DateTime.Now,
                    total_price = orderHeaderDto.total_price,
                    status = orderHeaderDto.status,
                };

                db.orderHeaders.Add(order);
                db.SaveChanges();

                var orderUser = db.orderHeaders.Include(x => x.user).FirstOrDefault(x => x.id == order.id);
                var userDto = new UserDto
                {
                    username = orderUser.user.username,
                    fullname = orderUser.user.fullname,
                    password = orderUser.user.password,
                    image_url = orderUser.user.image_url,
                    isAdmin = orderUser.user.isAdmin,
                };

                var response = new CreateOrderResponse {
                    OrderHeader = orderHeaderDto,
                    user = userDto
                };

                return StatusCode(201, new { oder = response });

            } catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [Authorize]
        [HttpGet("history")]
        public IActionResult GetOrderByUser()
        {
            var user_id = User.FindFirst("id").Value;
            var user = db.users.FirstOrDefault(x => x.id.ToString() == user_id);
            var order = db.orderHeaders.Where(x => x.user_id == user.id)
                .Select(x => new GetOrderByUserrResponse
                {
                    date = x.date,
                    total_price = x.total_price,
                    status = x.status,
                });

            return Ok(order);
        }

    }
}
