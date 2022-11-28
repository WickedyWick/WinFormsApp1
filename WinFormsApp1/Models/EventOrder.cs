using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Client.Models
{
    internal class EventOrder
    {
        [JsonPropertyName("üserId")]
        public int UserId { get; set; }
        [JsonPropertyName("eventId")]
        public int EventId { get; set; }

        public EventOrder(int userId, int eventId) { 
            UserId = userId;
            EventId = eventId;
        } 
    }
}
