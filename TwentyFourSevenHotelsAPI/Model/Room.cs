using System.ComponentModel.DataAnnotations;

namespace TwentyFourSevenHotelsAPI.Model;

public class Room
{
    public int Id { get; set; }
    [Required]
    public string RoomNumber { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public int PricePerNight { get; set; }

    [Required]
    public bool NeedsCleaning { get; set; } = false;

    [Required]
    public bool OutstandingRepairs { get; set; } = false;



    // Foreign key for roomtype
    [Required]
    public int RoomTypeId { get; set; }
    public RoomType RoomType { get; set; }
}