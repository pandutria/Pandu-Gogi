using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Pandu_Gogi_Backend.Data;
using Pandu_Gogi_Backend.Models.Dtos.User;
using Pandu_Gogi_Backend.Models.Entites;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Pandu_Gogi_Backend.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
        AppDbContext db;
        public UserController(AppDbContext db)
        {
            this.db = db;
        }

        private string GenerateToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("pandu_gogi_rahasia_token_very_secure_123"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("id", user.id.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: "PanduGogiApi",
                audience: "PanduGogiClient",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost("register")]
        public IActionResult register(UserDto userDto)
        {
            try
            {
                var user = new User
                {
                    username = userDto.username,
                    fullname = userDto.fullname,
                    password = userDto.password,
                    isAdmin = false
                };

                db.users.Add(user);
                db.SaveChanges();

                return StatusCode(201, new {user = user});

            } catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }


        [HttpPost("login")]
        public IActionResult login(LoginUserDto loginsUserDto)
        {
            try
            {
                var user = db.users.FirstOrDefault(x => x.username == loginsUserDto.username 
                && x.password == loginsUserDto.password);

                if (user != null)
                {
                    if (user.isAdmin) return BadRequest(new { message = "Your role is not admin!!" });

                    return StatusCode(201, new {token = GenerateToken(user)});
                } else
                {
                    return BadRequest(new { message = "Your data is not valid!!" });
                }

            } catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }

        [Authorize]
        [HttpGet("profile")]
        public IActionResult profile()
        {
            var id = User.FindFirst("id").Value;
            var user = db.users.FirstOrDefault(x => x.id.ToString() == id);

            if (user == null) return BadRequest(new { message = "User not found" });

            return Ok(new {user = user});
        }

        [Authorize]
        [HttpPost("uploadPhoto")]
        public IActionResult postPhoto(UploadPhotoUserDto uploadPhotoUserDto)
        {
            var id = User.FindFirst("id").Value;
            var user = db.users.FirstOrDefault(x => x.id.ToString() == id);

            if (user == null) return BadRequest(new { message = "User not found" });

            user.image_url = uploadPhotoUserDto.image_url;
            db.SaveChanges();

            return StatusCode(201, new {user = user});
        }

        [Authorize]
        [HttpPut("updateProfile")]
        public IActionResult updateProfile(UserDto userDto)
        {
            var id = User.FindFirst("id").Value;

            var user = db.users.FirstOrDefault(x => x.id.ToString() == id);

            if (user == null) return BadRequest(new { message = "User not found" });

            user.username = userDto.username;
            user.fullname = userDto.fullname;
            user.password = userDto.password;
            user.image_url = user.image_url;

            db.SaveChanges();

            return Ok(new {user = user});
        }
    }
}
