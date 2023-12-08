using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    [Route("/api/employee")]
    public class EmployeeController : Controller
    {
        private readonly ProjectContext _context;
        private readonly IConfiguration _configuration;
        public EmployeeController(ProjectContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        [HttpGet]
        public ActionResult Index()
        {
            List<EmployeeDTO> employees = _context.Employees
                .Include(e => e.Branches)
                .Select(m => new EmployeeDTO
                {
                    IdEmployee = m.IdEmployee,
                    IdBranches = m.IdBranches,
                    Branches = m.Branches,
                    FullName = m.FullName,
                    UserName = m.UserName,
                    Email = m.Email,
                    Password = m.Password,
                    role = m.role,

                }).ToList();
            return Ok(employees);
        }
        [HttpGet("searchbyid")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            var employees = await _context.Employees
                .Include(e =>e.Branches)
                .FirstOrDefaultAsync( e => e.IdEmployee ==id);
            if(employees == null)
            {
                return NotFound();
            }
            return employees;
        }
        [HttpPost("register")]
        public IActionResult Register(EmployeeModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    //hash
                    string hasd = BCrypt.Net.BCrypt.HashPassword(model.Password);
                    Employee employees = new Employee
                    {
                        FullName = model.FullName,
                        UserName = model.UserName,
                        Email = model.Email,
                        Password = hasd,
                        IdBranches = model.IdBranches,
                        role = model.role,
                    };
                    _context.Employees.Add(employees);
                    _context.SaveChanges();
                    return Created("", new EmployeeDTO
                    {
                        IdEmployee = employees.IdEmployee,
                        FullName = employees.FullName,
                        UserName = employees.UserName,
                        Email = employees.Email,
                        // ko tra ve vi ly do bao mat
                        IdBranches = employees.IdBranches,
                        role = employees.role,
                    });
                }catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            
            return Ok("Done");
        }
        
        [HttpPost("login")]
        public IActionResult Login(EmployeeModel model)
        {
            var employee = _context.Employees.SingleOrDefault(e => e.Email == model.Email);
            if (employee == null || !VerifyPassword(employee.Password, model.Password))
            {
                return Unauthorized();
            }
            var token = GenerateTokenLogin(employee.IdEmployee, model.FullName);
            return Ok(new {Token = token});
        }
        private bool VerifyPassword(string hashedPassword, string enteredPassword)
        {
            // compare enterpass with hashpass
            return BCrypt.Net.BCrypt.Verify(enteredPassword, hashedPassword);
        }
        private string GenerateTokenLogin(int Id, string Name)
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
        public IActionResult Edit(EmployeeModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
                    _context.Employees.Update(new Employee
                    {
                        IdEmployee = model.IdEmployee,
                        IdBranches = model.IdBranches,
                        FullName = model.FullName,
                        UserName = model.UserName,
                        Email = model.Email,
                        Password=hashedPassword,
                        role = model.role,
                    });
                    _context.SaveChanges();
                    return Ok("Successfully");
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
                Employee employee = _context.Employees.Find(id);
                _context.Employees.Remove(employee);
                _context.SaveChanges();
                return NoContent();
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
