using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transport_x.DTOs;
using Transport_x.Entities;
using Transport_x.Models;

namespace Transport_x.Controllers
{
    [ApiController]
    [Route("/api/goods")]
    public class GoodsController : Controller
    {
       private readonly ProjectContext _context;
        public GoodsController(ProjectContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index() 
        {
            List<GoodsDTO> good = _context.Goods
                .Include( a=>a.Bill)
                .Select(g => new GoodsDTO
                {
                    IdGoods = g.IdGoods,
                    IdBill = g.IdBill,
                    Bill = g.Bill,
                    Name = g.Name,
                    Nature = g.Nature,
                    Quantity= g.Quantity,
                    Valuse = g.Valuse,
                    Weight = g.Weight,
                })
                .ToList();
            return Ok(good);
        }
        [HttpGet("searchbyid")]
       public async Task<ActionResult<Goods>> GetGoods (int id)
       {
            var goods = await _context.Goods
                .Include(g => g.Bill)
                .FirstOrDefaultAsync( g => g.IdGoods == id);
            if(goods == null)
            {
                return NotFound();
            }
            return Ok(goods);
       }
        [HttpPost]
        public IActionResult CreateGoods (GoodsModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Goods good = new Goods
                    {
                        IdBill= model.IdBill,
                        Name = model.Name,
                        Nature = model.Nature,
                        Quantity= model.Quantity,
                        Valuse = model.Valuse,
                        Weight = model.Weight,
                    };
                    _context.Goods.Add(good);
                    _context.SaveChanges();
                    return Created("", new GoodsDTO
                    {
                        IdGoods= good.IdGoods,
                        IdBill= good.IdBill,
                        Name= good.Name,
                        Nature= good.Nature,
                        Quantity= good.Quantity,
                        Valuse= good.Valuse,
                        Weight= good.Weight,
                    });
                }catch(Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest("Done");
        }
        [HttpPut]
        public IActionResult Edit(GoodsModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Goods.Update(new Goods
                    {
                        IdGoods = model.IdGoods,
                        IdBill= model.IdBill,
                        Name= model.Name,
                        Nature= model.Nature,
                        Quantity= model.Quantity,
                        Valuse= model.Valuse,
                        Weight= model.Weight,
                    });
                    _context.SaveChanges();
                    return BadRequest("Successfully");  
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
                Goods goods = _context.Goods.Find(id);
                _context.Goods.Remove(goods);
                _context.SaveChanges();
                return NoContent();
            }
            catch(Exception ex) 
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
