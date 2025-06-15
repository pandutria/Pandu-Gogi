using System.ComponentModel.DataAnnotations.Schema;

namespace Pandu_Gogi_Backend.Models.Dtos.OrderDetail
{
    public class OrderDetailDto
    {
        public int order_header_id { get; set; }
        public int menu_id { get; set; }
        public int qty { get; set; }
    }
}
