using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Server.Models
{
    public class Orders
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int EventId { get; set; }

        public int UserId { get; set; }

    }
}
