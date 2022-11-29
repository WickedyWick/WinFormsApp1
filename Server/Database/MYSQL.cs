using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using MySql.Data;
using MySql.Data.MySqlClient;
using Server.Models;
using System.Data;
using System.Net;
using System.Security.Cryptography;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
                await mySqlConnection.OpenAsync();
                await adapter.FillAsync(dt);
                await mySqlConnection.CloseAsync();
                if (dt.Rows.Count == 0)
                    return new Tuple<HttpStatusCode, DataTable?>(HttpStatusCode.NotFound, null);

                return new Tuple<HttpStatusCode, DataTable?>(HttpStatusCode.OK, dt);
            } catch(MySqlException e)
            {
                mySqlConnection.Close();
                Console.Error.WriteLine(e.ToString());
                return new Tuple<HttpStatusCode, DataTable?>(HttpStatusCode.InternalServerError, null);
            }
            
        }
        public async static Task<HttpStatusCode> CreateOrder(EventOrder eventOrder)
        {
            try
            {

                await mySqlConnection.OpenAsync();
                MySqlCommand cmd = mySqlConnection.CreateCommand();
                MySqlTransaction mySqlTransaction = await mySqlConnection.BeginTransactionAsync();
                cmd.Connection = mySqlConnection;
                cmd.Transaction = mySqlTransaction;
                try
                {
                    cmd.Parameters.AddWithValue("@USERID", eventOrder.UserId);
                    cmd.Parameters.AddWithValue("@EVENTID", eventOrder.EventId);
                    cmd.Parameters.AddWithValue("@NUMOFTICKETS", eventOrder.NumOfTickets);
                    cmd.CommandText = "INSERT INTO orders (purchaseDate, numOfSeats, eventId, userId) SELECT NOW(), @NUMOFTICKETS, @EVENTID, @USERID where ( select events.seatsAvailable from events where events.id = @EVENTID) >= @NUMOFTICKETS;";
                    int res = await cmd.ExecuteNonQueryAsync();
                    if (res == 0)
                    {
                        await mySqlTransaction.RollbackAsync();
                        // rethink this
                        return HttpStatusCode.BadRequest;
                    }
                    cmd.CommandText = "Update events set seatsAvailable = seatsAvailable - @NUMOFTICKETS where id = @EVENTID";
                    res = await cmd.ExecuteNonQueryAsync();
                    if (res == 0)
                    {
                        // rethink this 
                        return HttpStatusCode.BadRequest;
                    }
                    await mySqlTransaction.CommitAsync();
                    return HttpStatusCode.Created;

                } catch (Exception e)
                {
                    try
                    {
                        await mySqlTransaction.RollbackAsync();
                    } catch (MySqlException ex)
                    {
                        if (mySqlTransaction.Connection != null)
                        {
                            Console.Error.WriteLine($"Error during rolling back transaction ${ex}");
                        }
                    }

                    Console.Error.WriteLine($"Error duruing transaction ${e}");
                    return HttpStatusCode.InternalServerError;

                } finally
                {
                    await mySqlConnection.CloseAsync();
                }
                            
            }
            catch (MySqlException e)
            {
                if (e.Number == 1452)
                {
                    return HttpStatusCode.BadRequest;
                }
                Console.Error.WriteLine(e.ToString());
                return HttpStatusCode.InternalServerError;
                
            }
               
                
           

        }

    }
}
