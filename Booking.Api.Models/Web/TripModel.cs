using Booking.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Api.Models.Web
{
    public class TripModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public int NumberOfSeats { get; set; }
        public int? CountryId { get; set; }
        public string? Country { get; set; }

        public static TripModel MapFromDb(Trip trip)
        {
            return new TripModel
            {
                Id = trip.Id,
                Name = trip.Name,
                Description = trip.Description,
                StartDate = trip.StartDate,
                NumberOfSeats = trip.NumberOfSeats,
                CountryId = trip.CountryId,
                Country = trip?.Country?.Name
            };
        }
    }
}
