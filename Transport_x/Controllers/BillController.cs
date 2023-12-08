using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using Transport_x.DTOs;
using Transport_x.Entities;
using Transport_x.Models;

namespace Transport_x.Controllers
{
    [ApiController]
    [Route("/api/bills")]
    public class BillController : Controller
    {
        private readonly ProjectContext _context;
        public BillController(ProjectContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Index() 
        {
            List<BillDTO> bill = _context.Bills
                .Include(a => a.User)
                .Include(a => a.ShippingType)
                .Select(b => new BillDTO
                {
                    IdBill = b.IdBill,
                    IdShippingType =b.IdShippingType,
                    ShippingType =b.ShippingType,
                    IdUser = b.IdUser,
                    User =b.User,
                    ConsigneeAddress=b.ConsigneeAddress,
                    ConsigneeName=b.ConsigneeName,
                    ConsigneeTel=b.ConsigneeTel,
                    Cod = b.Cod,
                    Change = b.Change,
                    Payer = b.Payer,
                    PickUp= b.PickUp,
                })
                .ToList();
            return Ok(bill);
        }
        [HttpGet("/searchbyid")]
        public async Task<ActionResult<Bill>> GetBill(int id) 
        {
            var bills = await _context.Bills
                .Include (a => a.User)
                .Include(a => a.ShippingType)
                .FirstOrDefaultAsync(a => a.IdBill == id);
            if(bills == null)
            {
                return NotFound();
            }
            return bills;
        }
        [HttpPost]
        public IActionResult Create(BillModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Bill bill = new Bill
                    {
                        IdShippingType = model.IdShippingType,
                        IdUser = model.IdUser,
                        ConsigneeAddress = model.ConsigneeAddress,
                        ConsigneeName = model.ConsigneeName,
                        ConsigneeTel = model.ConsigneeTel,
                        Cod = model.Cod,
                        Change = model.Change,
                        Payer = model.Payer,
                        PickUp= model.PickUp,

                    };
                    _context.Bills.Add(bill);
                    _context.SaveChanges();
                    return Created("", new BillDTO
                    {
                        IdShippingType = bill.IdShippingType,
                        IdUser = bill.IdUser,
                        ConsigneeAddress = bill.ConsigneeAddress,
                        ConsigneeName = bill.ConsigneeName,
                        ConsigneeTel = bill.ConsigneeTel,
                        Cod = bill.Cod,
                        Change = bill.Change,
                        Payer = bill.Payer,
                        PickUp = bill.PickUp,
                    });
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
            return BadRequest("Error");
        }
        [HttpPut]
        public IActionResult Edit(BillModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Bills.Update(new Bill
                    {
                        IdBill =model.IdBill,
                        IdShippingType = model.IdShippingType,
                        IdUser = model.IdUser,
                        ConsigneeAddress = model.ConsigneeAddress,
                        ConsigneeName = model.ConsigneeName,
                        ConsigneeTel = model.ConsigneeTel,
                        Cod = model.Cod,
                        Change = model.Change,
                        Payer = model.Payer,
                        PickUp = model.PickUp,

                    });
                    _context.SaveChanges();
                    return Ok("Successfully");
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
                Bill bill = _context.Bills.Find(id);
                _context.Bills.Remove(bill);
                _context.SaveChanges();
                return NoContent();
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
