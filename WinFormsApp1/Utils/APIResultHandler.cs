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
            
            if (statusCode == HttpStatusCode.OK)
            {
                MessageBoxWrapper.SuccessMessage(
                    "Successful order!",
                    "Successful order"
                );
                return true;
            }
            return false;
        }
    }
}
