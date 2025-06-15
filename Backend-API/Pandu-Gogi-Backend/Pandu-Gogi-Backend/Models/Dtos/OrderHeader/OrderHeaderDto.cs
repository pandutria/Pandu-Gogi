using System.ComponentModel.DataAnnotations.Schema;

namespace Pandu_Gogi_Backend.Models.Dtos.OrderHeader
{
    public class OrderHeaderDto
    {
        public int total_price { get; set; }
        public string status { get; set; }
    }
}
