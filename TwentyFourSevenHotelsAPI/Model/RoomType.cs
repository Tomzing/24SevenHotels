
using System.ComponentModel.DataAnnotations;


namespace TwentyFourSevenHotelsAPI.Model
{
    public class RoomType
    {
        public int Id { get; set; }

        [Required]
        public string RoomTypeName { get; set; }

        [Required]
        public string Description { get; set; }
    }
}