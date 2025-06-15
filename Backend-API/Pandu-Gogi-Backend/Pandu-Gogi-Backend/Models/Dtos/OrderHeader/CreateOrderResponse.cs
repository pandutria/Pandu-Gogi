using Pandu_Gogi_Backend.Models.Dtos.User;
using Pandu_Gogi_Backend.Models.Entites;

namespace Pandu_Gogi_Backend.Models.Dtos.OrderHeader
{
    public class CreateOrderResponse
    {
        public OrderHeaderDto OrderHeader { get; set; }
        public UserDto user { get; set; }
    }
}
