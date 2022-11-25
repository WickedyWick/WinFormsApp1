using System.ComponentModel.DataAnnotations;

namespace Server.Models
{
    public class Event
    {
        [Required]
        public int EventId { get; set; }
        [Required]
        public string EventName { get; set; }
        [Required]
        public string StartTime { get; set; }
        [Required]
        public string EndTime { get; set; }

        public int MaxSeats { get; set; }


    }
}
