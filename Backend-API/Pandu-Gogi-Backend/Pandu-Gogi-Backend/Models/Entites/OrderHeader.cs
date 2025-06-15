using System.ComponentModel.DataAnnotations.Schema;

namespace Pandu_Gogi_Backend.Models.Entites
{
    public class OrderHeader
    {
        public int id {  get; set; }
        public int user_id { get; set; }
        public DateTime date { get; set; }
        public int total_price { get; set; }
        public string status { get; set; }

        [ForeignKey("user_id")]
        public User user { get; set; }

    }
}
