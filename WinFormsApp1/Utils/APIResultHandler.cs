using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
namespace Client.Utils
{
    internal class APIResultHandler
    {
        public static bool HandleOrderEventResult(HttpStatusCode statusCode)
        {
            // return false or true depending if result has been successfull or not!
            
            if (statusCode == HttpStatusCode.Created)
            {
                MessageBoxWrapper.SuccessMessage(
                    "Successful order!",
                    "Successful order"
                );
                return true;
            }
            MessageBoxWrapper.ErrorMessage(
                "There was an error during ordering tickets",
                "Error during ordering tickets"
            ) ;
            return false;
        }

        public static bool HandleGetAllEvents(HttpStatusCode statusCode)
        {
            if (statusCode != HttpStatusCode.OK)
            {
                if (statusCode == HttpStatusCode.NotFound)
                {
                    MessageBoxWrapper.InformationalMessage(
                        "No events found!",
                        "No events found!"
                    );
                    return false;
                }

                MessageBoxWrapper.ErrorMessage(
                    "Error during loading events!",
                    "There was an error during loading events, please try again"
                );
                return false;

            } 
            return true;
        }
    }
}
