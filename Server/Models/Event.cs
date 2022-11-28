using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public DateTime EndTime { get; set; }
        public int MaxSeats { get; set; }
        [Required]
        public int SeatsAvailable { get; set; } 
    }
}
