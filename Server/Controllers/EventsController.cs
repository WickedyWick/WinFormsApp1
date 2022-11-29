using Microsoft.AspNetCore.Mvc;
using Server.ControllerMethods;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Encodings;
using Server.Models;
using Server.Database;
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
        public async Task<IActionResult> Post([FromBody] EventOrder eventOrder)
        {
            HttpStatusCode statusCode = await EventMethods.CreateOrder(eventOrder);
            if (statusCode == HttpStatusCode.Created)
            {
                // Rethink this
                return Created("/Orders/{id}", null);
                    
            } else if (statusCode == HttpStatusCode.BadRequest)
            {
                return BadRequest();
            }

            return Problem();

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
