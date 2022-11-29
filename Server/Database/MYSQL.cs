using Microsoft.Identity.Client;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data;
using System.Net;

namespace Server.Database
{
    public class MYSQL
    {
        private static MySqlConnection mySqlConnection = new MySqlConnection("server=localhost;database=events;uid=root;password=;");

        public async static Task<Tuple<HttpStatusCode,DataTable?>> GetAllEvents()
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand("", mySqlConnection);
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                cmd.CommandText = "Select *  from events";
                adapter.SelectCommand = cmd;
                DataTable dt = new DataTable();
                mySqlConnection.Open();
                await adapter.FillAsync(dt);

                if (dt.Rows.Count == 0)
                    return new Tuple<HttpStatusCode, DataTable?>(HttpStatusCode.NotFound, null);

                return new Tuple<HttpStatusCode, DataTable?>(HttpStatusCode.OK, dt);
            } catch(MySqlException e)
            {
                Console.Error.WriteLine(e.ToString());
                return new Tuple<HttpStatusCode, DataTable?>(HttpStatusCode.InternalServerError, null);
            }
            finally
            {
                mySqlConnection.Close();
            }
            
        }
        public void CreateOrder()
        {
            MySqlCommand cmd = new MySqlCommand($"insert into events values(deafult, 'testname', {DateTime.Now}, {DateTime.Now}, 20) ");
          //  cmd.Parameters.AddWithValue(")

        }

    }
}
