using Microsoft.EntityFrameworkCore;
using Server.Models;
namespace Server.Data
{
    public class EventDbContext : DbContext
    {

        public EventDbContext(DbContextOptions<EventDbContext> options)
            :base(options) {
        
        }

        // this would create database
        public DbSet<Event> Events { get; set; }
    }
}
