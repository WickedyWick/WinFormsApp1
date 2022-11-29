using Microsoft.AspNetCore.Mvc;
using Server.ControllerMethods;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Encodings;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        // GET: api/<EventsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            
            string respString = @"[{ 
                ""eventId"": 0 ,
                ""eventName"": ""event name 0"",
                ""startTime"": ""2022-02-01 10:00"",
                ""endTime"": ""2022-02-01 12:00"",
                ""maxSeats"": 10
            },
{ 
                ""eventId"": 1 ,
                ""eventName"": ""event name 2"",
                ""startTime"": ""2022-02-01 10:00"",
                ""endTime"": ""2022-02-01 12:00"",
                ""maxSeats"": 10
            },
            { 
                ""eventId"": 2 ,
                ""eventName"": ""event name 2"",
                ""startTime"": ""2022-02-01 10:00"",
                ""endTime"": ""2022-02-01 12:00"",
                ""maxSeats"": null
            }]
            ";
            
            // Ideally some kind of handler would be best here
            Tuple<HttpStatusCode, string?> res = await EventMethods.GetAllEvents();
            if (res.Item1 == HttpStatusCode.OK)
                return Ok(res.Item2);
            else if (res.Item1 == HttpStatusCode.NotFound)
                return NotFound();

            return Problem();
        }

        // GET api/<EventsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<EventsController>
        [HttpPost]
        public void Post([FromBody] int eventId, int userId)
        {
            Console.WriteLine(eventId.ToString(), userId.ToString());
        }

        // PUT api/<EventsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<EventsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
