using Booking.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Api.Models.Web
{
    public class RegisterTripModel
    {
        public int Id { get; set; }
        public int TripId { get; set; }
        public string Email { get; set; } = string.Empty;

        public static RegisterTripModel MapFromDb(RegisterTrip model)
        {
            return new RegisterTripModel()
            {
                Id = model.Id,
                TripId = model.TripId,
                Email = model.Email
            };
        }
    }
}
