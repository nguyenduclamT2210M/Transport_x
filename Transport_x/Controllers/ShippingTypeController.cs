using Microsoft.AspNetCore.Mvc;
using Transport_x.DTOs;
using Transport_x.Entities;
using Transport_x.Models;

namespace Transport_x.Controllers
{
    [ApiController]
    [Route("/api/shippingtype")]
    public class ShippingTypeController : Controller
    {
        private readonly ProjectContext _context;
        public ShippingTypeController(ProjectContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<ShippingTypeDTO> dTOs = _context.ShippingTypes
                .Select(m => new ShippingTypeDTO
                {
                    IdShipType = m.IdShipType,
                    NameShip=m.NameShip,
                    ChageRate = m.ChageRate,
                }).ToList();
            return Ok(dTOs);
        }
        [HttpGet("id")]
        public async Task<ActionResult<ShippingType>> GetShippingType (int id)
        {
            var shipping = await _context.ShippingTypes.FindAsync(id);
            if(shipping == null)
            {
                return NotFound();
            }
            return shipping;
        }
        [HttpPost]
        public IActionResult Create(ShippingTypeModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ShippingType shippingType = new ShippingType
                    {
                        NameShip = model.NameShip,
                        ChageRate = model.ChageRate,
                    };
                    _context.ShippingTypes.Add(shippingType);
                    _context.SaveChanges();
                    return Created("", new ShippingTypeDTO
                    {
                        IdShipType = shippingType.IdShipType,
                        NameShip= shippingType.NameShip,
                        ChageRate = shippingType.ChageRate,
                    });
                }catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return Ok("Done");
        }
        [HttpPut]
        public IActionResult Edit(ShippingTypeModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.ShippingTypes.Update(new ShippingType
                    {
                        IdShipType= model.IdShipType,
                        NameShip= model.NameShip,
                        ChageRate = model.ChageRate,
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
                ShippingType shippingType = _context.ShippingTypes.Find(id);
                _context.ShippingTypes.Remove(shippingType);
                _context.SaveChanges();
                return NoContent();
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
