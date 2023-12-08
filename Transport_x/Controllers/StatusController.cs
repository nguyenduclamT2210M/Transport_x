using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transport_x.DTOs;
using Transport_x.Entities;
using Transport_x.Models;

namespace Transport_x.Controllers
{
    [ApiController]
    [Route("/api/status")]
    public class StatusController : Controller
    {
        private readonly ProjectContext _context;
        public StatusController(ProjectContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<StatusDTO> status = _context.Statuses
                .Include(s => s.Bill)
                .Include(s => s.Employee)
                 
                .Select(m => new StatusDTO
                {
                    IdStatus = m.IdStatus,
                    IdEmployee = m.IdEmployee,
                    Employee=m.Employee,
                    IdBill = m.IdBill,
                    Bill=m.Bill,
                    StatusTime = m.StatusTime,
                    TypeStatus = m.TypeStatus,
                }).ToList(); 
            return Ok(status);
        }
        [HttpGet("searchbyid")]
        public async Task<ActionResult<Status>> GetStatus(int id)
        {
            var status = await _context.Statuses
                .Include(s => s.IdBill)
                .Include(s => s.IdEmployee)
                .FirstOrDefaultAsync(s => s.IdStatus == id);
            return status;
        }
        [HttpPost]
        public IActionResult Create(StatusModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Status status = new Status
                    {
                        IdEmployee = model.IdEmployee,
                        IdBill=model.IdBill,
                        StatusTime = model.StatusTime,
                        TypeStatus = model.TypeStatus,
                    };
                    _context.Statuses.Add(status);
                    _context.SaveChanges();
                    return Created("", new StatusDTO
                    {
                        IdStatus = status.IdStatus,
                        IdEmployee = status.IdEmployee,
                        IdBill=status.IdBill,
                        StatusTime = status.StatusTime,
                        TypeStatus = status.TypeStatus,
                    });
                }catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return Ok();
        }
        [HttpPut]
        public IActionResult Edit(StatusModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Statuses.Update(new Status
                    {
                        IdStatus = model.IdStatus,
                        IdEmployee = model.IdEmployee,
                        IdBill=model.IdBill,
                        StatusTime = model.StatusTime,
                        TypeStatus = model.TypeStatus,
                    });
                }catch (Exception ex)
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
                Status status = _context.Statuses.Find(id);
                _context.Statuses.Remove(status);
                return NoContent();
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
