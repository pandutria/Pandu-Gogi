using System.ComponentModel.DataAnnotations.Schema;

namespace Pandu_Gogi_Backend.Models.Entites
{
    public class Menu
    {
        public int id {  get; set; }

        //[ForeignKey("category")]
        public int category_id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int price { get; set; }
        public string image_url { get; set; } 
        public int? order_count { get; set; }

        [ForeignKey("category_id")]
        public Category category { get; set; }  
    }
}
