namespace Pandu_Gogi_Backend.Models.Entites
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public string fullname { get; set; }
        public string password { get; set; }
        public string? image_url { get; set; }
        public bool isAdmin { get; set; }
    }
}
