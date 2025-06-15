using System.ComponentModel.DataAnnotations.Schema;

namespace Pandu_Gogi_Backend.Models.Entites
{
    public class OrderDetail
    {
        public int id {  get; set; }    
        public int order_header_id { get; set; }
        public int menu_id { get; set; }
        public int qty { get; set; }

        [ForeignKey("order_header_id")]
        public OrderHeader orderHeader { get; set; }

        [ForeignKey("menu_id")]
        public Menu menu { get; set; }
        public ICollection<Menu> menus { get; set; }

    }
}
