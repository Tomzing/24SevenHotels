using System.ComponentModel.DataAnnotations;


namespace TwentyFourSevenHotelsAPI.Model;
public class Guest
{
        public int Id { get; set; }
        [Required]
        public DateTime CheckinDate { get; set; }
        [Required]
        public DateTime CheckoutDate { get; set; }
}
