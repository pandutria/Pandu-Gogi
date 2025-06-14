namespace Pandu_Gogi_Backend.Models.Dtos.User
{
    public class UserDto
    {
        public string username { get; set; }
        public string? fullname { get; set; }
        public string password { get; set; }
        public string? image_url { get; set; }
        public bool isAdmin { get; set; }
    }
}
