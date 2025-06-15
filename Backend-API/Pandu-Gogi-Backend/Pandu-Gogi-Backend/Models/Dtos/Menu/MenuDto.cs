using System.ComponentModel.DataAnnotations.Schema;

namespace Pandu_Gogi_Backend.Models.Dtos.Menu
{
    public class MenuDto
    {
        [ForeignKey("Category")]
        public int category_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public string image_url { get; set; }
    }
}
