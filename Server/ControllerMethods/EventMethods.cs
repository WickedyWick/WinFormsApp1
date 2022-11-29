using Microsoft.AspNetCore.Mvc;
using Server.Database;
using Server.Utils;
using System.Data;
using System.Net;
namespace Server.ControllerMethods
{
    public class EventMethods
    {
        public static async Task<Tuple<HttpStatusCode, string?>> GetAllEvents()
        {
            // Re think if you can re use data tuple instead of creating a new one?
            Tuple<HttpStatusCode,DataTable?> data = await MYSQL.GetAllEvents();
            if (data.Item1 != HttpStatusCode.OK)
                return new Tuple<HttpStatusCode, string?>(data.Item1, null);

            // ! tells compiler that I am sure this won't be null check 
            string jsonString = await ObjectToJSONParser.DataTableToJSONString(data.Item2!);
            return new Tuple<HttpStatusCode, string?>(data.Item1, jsonString);
        }
    }
}
