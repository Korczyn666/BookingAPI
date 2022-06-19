using Booking.Api.Models.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Service.Interfaces
{
    public interface ITripService
    {
        public TripModel AddTrip(TripModel model);
        public TripModel EditTrip(TripModel model);
        public bool DeleteTrip(int tripId);
        public List<TripModel> GetTripList();
        public List<TripModel> GetTripListByCountry(int countryId);
        public TripModel GetTrip(int tripId);
        public RegisterTripModel RegisterTrip(RegisterTripModel model);
        public bool UnregisterTrip(RegisterTripModel model);
        public List<ErrorModel> ValidateTrip(TripModel model);
        public List<ErrorModel> ValidateTripRegestion(RegisterTripModel model, int regestrationTypeId);
    }
}
