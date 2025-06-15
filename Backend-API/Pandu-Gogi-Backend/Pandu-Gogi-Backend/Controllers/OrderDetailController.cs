using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pandu_Gogi_Backend.Data;
using Pandu_Gogi_Backend.Models.Dtos.OrderDetail;
using Pandu_Gogi_Backend.Models.Entites;

namespace Pandu_Gogi_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        AppDbContext db;

        public OrderDetailController(AppDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult GetAllOrder()
        {
            var orderDetail = db.orderDetails;
            return Ok(orderDetail);
        }

        [HttpPost]
        public IActionResult CreateOrderDetail (OrderDetailDto orderDetailDto)
        {
            try
            {
                var orderDetail = new OrderDetail
                {
                    order_header_id = orderDetailDto.order_header_id,
                    menu_id = orderDetailDto.menu_id,
                    qty = orderDetailDto.qty,
                };

                db.orderDetails.Add(orderDetail);
                db.SaveChanges();

                return StatusCode(201, new { OrderDetail = orderDetail });
            } catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [HttpGet("GetByHeader")]
        public IActionResult GetOrderByHeader(OrderDetailGetByHeaderDto orderDetailGetByHeaderDto)
        {
            var orderDetail = db.orderDetails.Include(x => x.menu).Where(x => x.order_header_id == orderDetailGetByHeaderDto.order_header_id);

            return Ok(orderDetail);
        }
    }
}
