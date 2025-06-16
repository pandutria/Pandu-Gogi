using Pandu_Gogi_Backend.Models.Dtos.Menu;
using Pandu_Gogi_Backend.Models.Dtos.OrderHeader;

namespace Pandu_Gogi_Backend.Models.Dtos.OrderDetail
{
    public class OrderDetailMenuDto
    {
        public int menu_id { get; set; }
        public int qty { get; set; }
        public OrderDetailMenuResponse menu { get; set; }
    }
}
