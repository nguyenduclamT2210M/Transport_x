using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transport_x.DTOs;
using Transport_x.Entities;
using Transport_x.Models;

namespace Transport_x.Controllers
{
    [ApiController]
    [Route("/api/branches")]
    public class BranchesController : Controller
    {
        private readonly ProjectContext _context;
        public BranchesController(ProjectContext context) 
        {
            _context = context; 
        }
        [HttpGet]
        public IActionResult Index() 
        {
            List<BranchesDTO> branches = _context.Branches
                .Select(m => new BranchesDTO
                {
                    IdBranches = m.IdBranches,
                    NameBranches = m.NameBranches,
                    TelephoneBranches = m.TelephoneBranches,
                    AddressBranches = m.AddressBranches,
                }).ToList();
            return Ok(branches);
        }
        [HttpGet("searchbyid")]
        public async Task<ActionResult<Branches>> GetBranches(int id)
        {
            var branches = await _context.Branches
                .FindAsync(id);
            if(branches == null){
                return NotFound();
            }
            return branches;
        }
        [HttpPost]
        public IActionResult Create(BranchesModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Branches branches = new Branches
                    {
                        NameBranches = model.NameBranches,
                        TelephoneBranches = model.TelephoneBranches,
                        AddressBranches = model.AddressBranches,
                    };
                    _context.Branches.Add(branches);
                    _context.SaveChanges();
                    return Created("", new BranchesDTO
                    {
                        IdBranches = branches.IdBranches,
                        NameBranches = branches.NameBranches,
                        TelephoneBranches =branches.TelephoneBranches,
                        AddressBranches = branches.AddressBranches,
                    });
                }catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return Ok("Done");
        }
        [HttpPut]
        public IActionResult Edit(BranchesModel model) 
        {
            if(ModelState.IsValid)
            {

                try
                {
                    _context.Branches.Update(new Branches
                    {
                        IdBranches = model.IdBranches,
                        NameBranches = model.NameBranches,
                        TelephoneBranches = model.TelephoneBranches,
                        AddressBranches = model.AddressBranches,
                    });
                    _context.SaveChanges();
                    return Ok("Successfully");
                }catch(Exception ex)
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
                Branches branches = _context.Branches.Find(id);
                _context.Branches.Remove(branches);
                _context.SaveChanges();
                return NoContent();
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
