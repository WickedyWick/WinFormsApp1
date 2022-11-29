using Server.Models;
using System.Data;
using System.Text.Json;
using Newtonsoft.Json;
namespace Server.Utils
{
    public class ObjectToJSONParser
    {
        public static async Task<string> DataTableToJSONString(DataTable dt)
        {

            string jsonString =  JsonConvert.SerializeObject(dt);
                /*
            var data = dt.Rows.OfType<DataRow>()
                .Select(row => dt.Columns.OfType<DataColumn>()
                    .ToDictionary(col => col.ColumnName, c => row[c])) ;
                */
            return jsonString;

        }
    }
}
