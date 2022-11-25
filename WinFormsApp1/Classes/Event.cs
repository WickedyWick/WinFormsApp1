using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Classes
{
    internal class Event
    {
        public int eventId { get; set; }
        public string eventName { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public int? maxSeats { get; set; }

    }
}
