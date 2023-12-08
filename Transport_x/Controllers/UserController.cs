using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Transport_x.DTOs;
using Transport_x.Entities;
using Transport_x.Models;

namespace Transport_x.Controllers
{
    [ApiController]
    [Route("/api/user")]
    public class UserController : Controller
    {
        private readonly ProjectContext _context;
        private readonly IConfiguration _configuration;
        public UserController(ProjectContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult index()
        {
            List<UserDTO > users = _context.Users
                .Select(m => new UserDTO
                {
                    IdUser = m.IdUser,
                    FullNameUser = m.FullNameUser,
                    EmailUser = m.EmailUser,
                    PassWordUser = m.PassWordUser,
                    TelephoneUser = m.TelephoneUser,
                    AddressUser = m.AddressUser,
                }).ToList();
            return Ok(users);
        }
        [HttpGet("searchbyid")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var users = await _context.Users.FindAsync(id);

            if (users == null)
            {
                return NotFound(); // Trả về 404 Not Found nếu không tìm thấy user
            }

            return users; // Trả về thông tin chi tiết của user
        }
        [HttpPost("register")]
        public IActionResult Register(UserModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // ma hoa pass
                    string hanshedPassword = BCrypt.Net.BCrypt.HashPassword(model.PassWordUser);
                    User user = new User
                    {
                        FullNameUser = model.FullNameUser,
                        EmailUser = model.EmailUser,
                        PassWordUser = hanshedPassword,
                        TelephoneUser = model.TelephoneUser,
                        AddressUser = model.AddressUser,
                    };
                    _context.Add(user);
                    _context.SaveChanges();
                    return Created("", new UserDTO
                    {
                        IdUser = user.IdUser,
                        FullNameUser = model.FullNameUser,
                        EmailUser = model.EmailUser,
                        //ko tra lai pass vi ly do bao mat
                        TelephoneUser = model.TelephoneUser,
                        AddressUser = model.AddressUser,
                    });
                }catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return Ok("Done");
        }
            
        [HttpPost("login")]
        public IActionResult Login (UserModel model)
        {
            var user = _context.Users.SingleOrDefault( u => u.EmailUser == model.EmailUser );
            if (user == null || !VerifyPassword(user.PassWordUser, model.PassWordUser))
            {
                return Unauthorized();
            }
            var token = GenerateTokenLogin(user.IdUser, user.FullNameUser);
            return Ok(new {Token = token});
        }
        private bool VerifyPassword(string hanshedPassword, string enterPassword)
        {
            return BCrypt.Net.BCrypt.Verify(enterPassword, hanshedPassword);
        }
        private string GenerateTokenLogin(int IdUser, string Name)
        {
            // Tạo một key có kích thước đủ lớn (256 bits)
            var securityKey = new byte[32]; // 32 bytes = 256 bits

            // Sử dụng một cơ chế để tạo key ngẫu nhiên
            using (var rng = new System.Security.Cryptography.RNGCryptoServiceProvider())
            {
                rng.GetBytes(securityKey);
            }

            var credentials = new SigningCredentials(new SymmetricSecurityKey(securityKey), SecurityAlgorithms.HmacSha256);

            // Tạo token
            var token = new JwtSecurityToken(
                issuer: "your_issuer_here",
                audience: "your_audience_here",
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            // Trả về token dưới dạng chuỗi
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        [HttpPut]
        public IActionResult Edit(UserModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var existingUser = _context.Users.FirstOrDefault(u => u.IdUser == model.IdUser);

                    if (existingUser != null)
                    {
                        // Update only the fields that were provided in the model
                        if (!string.IsNullOrEmpty(model.FullNameUser))
                        {
                            existingUser.FullNameUser = model.FullNameUser;
                        }

                        if (!string.IsNullOrEmpty(model.EmailUser))
                        {
                            existingUser.EmailUser = model.EmailUser;
                        }

                        if (!string.IsNullOrEmpty(model.TelephoneUser))
                        {
                            existingUser.TelephoneUser = model.TelephoneUser;
                        }

                        if (!string.IsNullOrEmpty(model.AddressUser))
                        {
                            existingUser.AddressUser = model.AddressUser;
                        }

                        if (!string.IsNullOrEmpty(model.PassWordUser))
                        {
                            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.PassWordUser);
                            existingUser.PassWordUser = hashedPassword;
                        }

                        _context.SaveChanges();
                        return Ok("Successfully updated");
                    }
                    else
                    {
                        return BadRequest("User not found");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest("Error");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                User user = _context.Users.Find(id);
                _context.Users.Remove(user);
                _context.SaveChanges();
                return NoContent();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
