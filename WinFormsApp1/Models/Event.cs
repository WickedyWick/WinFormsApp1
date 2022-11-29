using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Client.Classes
{
    internal class Event
    {
        [JsonPropertyName("id")]
        public int EventId { get; set; }
        [JsonPropertyName("name")]
        public string EventName { get; set; }
        [JsonPropertyName("startTime")]
        public string StartTime { get; set; }
        [JsonPropertyName("endTime")]
        public string EndTime { get; set; }
        [JsonPropertyName("seatsAvailable")]
        public int? SeatsAvailable { get; set; }

    }
}
