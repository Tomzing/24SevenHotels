using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TwentyFourSevenHotelsAPI.Model;

namespace TwentyFourSevenHotelsAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoomTypesController : ControllerBase
{
    private readonly HotelDbContext _context;
    public RoomTypesController(HotelDbContext context)
    {
        _context = context;
    }


    [HttpPost]
    public async Task<ActionResult<RoomType>> CreateRoomType(RoomType roomType)
    {
        if (roomType == null)
        {
            return BadRequest("RoomType data is null.");
        }
        _context.RoomTypes.Add(roomType);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetRoomTypeById), new { id = roomType.Id }, roomType);
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<RoomType>>> GetRoomTypes()
    {
        return await _context.RoomTypes.ToListAsync();
    }


    [HttpGet("{id}")]
    public async Task<ActionResult<RoomType>> GetRoomTypeById(int id)
    {
        var roomType = await _context.RoomTypes.FindAsync(id);
        if (roomType == null)
        {
            return NotFound();
        }
        return roomType;
    }
}
