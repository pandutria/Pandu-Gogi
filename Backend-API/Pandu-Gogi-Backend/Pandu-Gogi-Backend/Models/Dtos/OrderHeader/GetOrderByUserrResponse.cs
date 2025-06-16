using Pandu_Gogi_Backend.Models.Dtos.OrderDetail;

namespace Pandu_Gogi_Backend.Models.Dtos.OrderHeader
{
    public class GetOrderByUserrResponse
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public int total_price { get; set; }
        public string status { get; set; }
        public List<OrderDetailMenuDto> order { get; set; }
    }
}
