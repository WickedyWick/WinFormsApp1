using Client.Classes;
using Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace Client.Utils
{
     internal class APIConsumer
     {
        private static string APIRoute = "";
        private static HttpClient APIClient;
        static APIConsumer() {
            APIRoute = System.Configuration.ConfigurationManager.AppSettings["APIRoute"] ?? "http:localhost:7241/api";
            APIClient = new HttpClient();
            APIClient.DefaultRequestHeaders.Accept.Clear();
           // APIClient.DefaultRequestHeaders.Accept.Add(
             //   new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("text/json") );
        }

        public static async Task<List<Event>?> GetAllEvents()
        {
            HttpResponseMessage resp = await APIClient.GetAsync($"{APIRoute}/Events");

            bool isSuccessful = APIResultHandler.HandleGetAllEvents(resp.StatusCode);
            if (!isSuccessful)
                return null;
            return await resp.Content.ReadFromJsonAsync<List<Event>>();
        }

        public static async Task OrderEvent(int userId, int eventId, int ticketAmount)
        {
            HttpResponseMessage response = await APIClient.PostAsJsonAsync(
                $"{APIRoute}/Events",
                new EventOrder(userId, eventId, ticketAmount)
            );
     
            HttpStatusCode statusCode = response.StatusCode;

            APIResultHandler.HandleOrderEventResult(statusCode);

        }
     }
}
