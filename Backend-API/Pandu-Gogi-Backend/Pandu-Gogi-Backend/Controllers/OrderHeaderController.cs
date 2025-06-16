using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pandu_Gogi_Backend.Data;
using Pandu_Gogi_Backend.Models.Dtos.Category;
using Pandu_Gogi_Backend.Models.Dtos.Menu;
using Pandu_Gogi_Backend.Models.Dtos.OrderDetail;
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

                var response = new CreateOrderResponse
                {
                    OrderHeader = orderHeaderDto,
                    user = userDto
                };

                return StatusCode(201, new { oder = response });

            }
            catch (Exception err)
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

            var order = db.orderHeaders
                .Include(x => x.OrderDetails)
                .Where(x => x.user_id == user.id)
                .Select(x => new GetOrderByUserrResponse
                {
                    id = x.id,
                    date = x.date,
                    total_price = x.total_price,
                    status = x.status,
                    order = x.OrderDetails
                        .Where(y => y.order_header_id == x.id)
                        .Select(y => new OrderDetailMenuDto
                        {
                            menu_id = y.menu_id,
                            qty = y.qty,
                            menu = db.menus
                                .Where(z => z.id == y.menu_id)
                                .Select(z => new OrderDetailMenuResponse
                                {
                                    id = z.id,
                                    category_id = z.category_id,
                                    name = z.name,
                                    description = z.description,
                                    price = z.price,
                                    image_url = z.image_url,
                                    category = db.categories
                                        .Where(a => a.id == z.category_id)
                                        .Select(a => new OrderDetailMenuCategoryResponse
                                        {
                                            id = a.id,
                                            name = a.name,
                                        })
                                        .FirstOrDefault() // ambil 1 kategori
                                })
                                .FirstOrDefault() // ambil 1 menu
                        }).ToList()
                }).ToList();

            return Ok(order);
        }


        [HttpPut("status")]
        public IActionResult UpdateStatus(OrderStatusDto orderStatusDto)
        {
            var order = db.orderHeaders.FirstOrDefault(x => x.id == orderStatusDto.order_id);
            if (order == null) return BadRequest(new { message = "Order not found" });

            order.status = orderStatusDto.status;
            db.SaveChanges();

            return Ok(new { order = order });
        }
    }
}
