using System.Text.Json.Serialization;

namespace Server.Models
{
    public class EventOrder
    {
        [JsonPropertyName("userId")]
        public int UserId { get; set; }
        [JsonPropertyName("eventId")]
        public int EventId { get; set; }
        [JsonPropertyName("numOfTickets")]
        public int NumOfTickets { get; set; }
    }
}
