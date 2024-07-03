using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TwentyFourSevenHotelsAPI.Model;


[Route("api/[controller]")]
[ApiController]
public class RoomsController : ControllerBase
{
    private readonly HotelDbContext _context;
    public RoomsController(HotelDbContext context)
    {
        _context = context;
    }


    [HttpPost]
    public async Task<ActionResult<Room>> CreateRoom(Room room)
    {
        if (room == null)
        {
            return BadRequest("Room data is null.");
        }
        _context.Rooms.Add(room);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetRoomById), new { id = room.Id }, room);
    }
    [HttpGet("{id}")]
    public async Task<ActionResult<Room>> GetRoomById(int id)
    {
        var room = await _context.Rooms.FindAsync(id);
        if (room == null)
        {
            return NotFound();
        }
        return room;
    }


    [HttpGet]
    public async Task<ActionResult<IEnumerable<Room>>> GetRooms([FromQuery] string roomNumber)
    {
        if (string.IsNullOrEmpty(roomNumber))
        {
            return await _context.Rooms.ToListAsync();
        }
        var rooms = await _context.Rooms
            .Where(r => r.RoomNumber.Contains(roomNumber))
            .ToListAsync();
        if (rooms == null || rooms.Count == 0)
        {
            return NotFound();
        }
        return rooms;
    }
}
