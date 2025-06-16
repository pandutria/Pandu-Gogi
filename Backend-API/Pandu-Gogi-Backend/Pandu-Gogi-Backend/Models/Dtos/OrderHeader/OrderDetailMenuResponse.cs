using Pandu_Gogi_Backend.Models.Dtos.Category;
using System.ComponentModel.DataAnnotations.Schema;

namespace Pandu_Gogi_Backend.Models.Dtos.OrderHeader
{
    public class OrderDetailMenuResponse
    {
        public int id { get; set; } 
        public int category_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public string image_url { get; set; }
        public OrderDetailMenuCategoryResponse category { get; set; }
    }
}
