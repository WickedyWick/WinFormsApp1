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
        [JsonPropertyName("userId")]
        public int UserId { get; set; }
        [JsonPropertyName("eventId")]
        public int EventId { get; set; }
        [JsonPropertyName("numOfTickets")]
        public int NumOfTickets { get; set; }

        public EventOrder(int userId, int eventId, int numOfTickets) { 
            UserId = userId;
            EventId = eventId;
            NumOfTickets = numOfTickets;
        } 
    }
}
