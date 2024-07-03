using System.ComponentModel.DataAnnotations;

namespace TwentyFourSevenHotelsAPI.Model;


public class Booking
{
    public int Id { get; set; }
    [Required]
    public DateTime CheckinDate { get; set; }
    [Required]
    public DateTime CheckoutDate { get; set; }
    [Required]
    public DateTime OrderDate { get; set; } = new DateTime();
    [Required]
    public float TotalPrice{ get; set; }

    [Required]
    public string BookingStatus { get; set; }

    //Foreign key for room
    public Room Room { get; set; }
    public int RoomId { get; set; }

    //Foreign key for guest
    public Guest Guest { get; set; }
    public int GuestId { get; set; }

}