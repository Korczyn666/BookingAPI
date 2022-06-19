using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Database.Models
{
    public class RegisterTrip
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int TripId { get; set; }
        public string Email { get; set; }
        [ForeignKey("TripId")]
        public virtual Trip? Trip { get; set; }
    }
}
