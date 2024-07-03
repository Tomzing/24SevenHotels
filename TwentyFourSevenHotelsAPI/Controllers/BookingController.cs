using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TwentyFourSevenHotelsAPI.Model;

namespace TwentyFourSevenHotelsAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingsController : ControllerBase
{
    private readonly HotelDbContext _context;
    public BookingsController(HotelDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult<Booking>> CreateBooking(Booking booking)
    {
        if (booking == null)
        {
            return BadRequest("Booking data is null.");
        }
        // Check if the room is already booked during the requested timeslot
        var roomAlreadyBooked = await _context.Bookings
            .AnyAsync(b => b.RoomId == booking.RoomId &&
                           ((booking.CheckinDate >= b.CheckinDate && booking.CheckinDate < b.CheckoutDate) ||
                            (booking.CheckoutDate > b.CheckinDate && booking.CheckoutDate <= b.CheckoutDate) ||
                            (booking.CheckinDate <= b.CheckinDate && booking.CheckoutDate >= b.CheckoutDate)));
        if (roomAlreadyBooked)
        {
            return Conflict("The room is already booked during the requested timeslot.");
        }
        
        // Add the new booking to the context
        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetBookingById), new { id = booking.Id }, booking);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Booking>> GetBookingById(int id)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking == null)
        {
            return NotFound();
        }
        return booking;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Booking>>> GetBookings()
    {
        return await _context.Bookings.ToListAsync();
    }
}
