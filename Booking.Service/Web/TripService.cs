using Booking.Api.Models.Enum;
using Booking.Api.Models.Web;
using Booking.Database;
using Booking.Database.Models;
using Booking.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Service.Web
{
    public  class TripService : ITripService
    {
        private readonly BookingDbContext _context;
        public TripService(BookingDbContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }
        public TripModel AddTrip(TripModel model)
        {
            try
            {
                var trip = new Trip
                {
                    Name = model.Name,
                    Description = model.Description,
                    NumberOfSeats = model.NumberOfSeats,
                    StartDate = model.StartDate,
                    CountryId = model.CountryId
                };

                _context.Trip.Add(trip);

                _context.SaveChanges();

                var result = TripModel.MapFromDb(trip);

                return result;

            }catch(Exception e)
            {
                throw new Exception($"Something went wrong {e.Message}");
            }
        }
        public TripModel EditTrip(TripModel model)
        {
            try
            {
                var tripDb = _context.Trip.Find(model.Id);

                if (tripDb == null)
                    throw new Exception("Could not find the trip");

                tripDb.Name = model.Name;
                tripDb.Description = model.Description;
                tripDb.StartDate = model.StartDate;
                tripDb.NumberOfSeats = model.NumberOfSeats;

                _context.SaveChanges();

                var result = TripModel.MapFromDb(tripDb);

                return result;

            }
            catch(Exception e)
            {
                throw new Exception($"Something went wrong {e.Message}");
            }
        }
        public bool DeleteTrip(int tripId)
        {
            try
            {
                var tripDb = _context.Trip.Find(tripId);

                if (tripDb == null)
                    return false;

                _context.Trip.Remove(tripDb);

                _context.SaveChanges();

                return true;

            }catch(Exception e)
            {
                throw new Exception($"Something went wrong {e.Message}");
            }
        }
        public List<TripModel> GetTripList()
        {
            var tripListDb = _context.Trip
                .Include(x => x.Country)
                .Where(x => string.IsNullOrEmpty(x.Description));

            if (tripListDb == null)
                return null;

            var resultList = tripListDb.Select(TripModel.MapFromDb).ToList();

            return resultList;
        }
        public List<TripModel> GetTripListByCountry(int countryId)
        {
            var tripListDb = _context.Trip
                .Include(x => x.Country)
                .Where(x => x.CountryId == countryId && string.IsNullOrEmpty(x.Description));

            if (tripListDb == null)
                return null;

            var resultList = tripListDb.Select(TripModel.MapFromDb).ToList();

            return resultList;
        }
        public TripModel GetTrip(int tripId)
        {
            var tripDb = _context.Trip
                .Include(x => x.Country)
                .FirstOrDefault(x => x.Id == tripId);

            if (tripDb == null)
                throw new Exception("Could not find the trip");

            var result = TripModel.MapFromDb(tripDb);

            return result;
        }
        public RegisterTripModel RegisterTrip(RegisterTripModel model)
        {
            try
            { 
                var registerTrip = new RegisterTrip
                {
                    Email = model.Email,
                    TripId = model.TripId
                };

                _context.RegisterTrip.Add(registerTrip);

                _context.SaveChanges();

                var result = RegisterTripModel.MapFromDb(registerTrip);

                return result;
            }
            catch(Exception e)
            {
                throw new Exception($"Something went wrong {e.Message}");
            }
        }
        public bool UnregisterTrip(RegisterTripModel model)
        {
            try
            {
                var registerTripDb = _context.RegisterTrip
                    .FirstOrDefault(x => x.TripId == model.TripId && x.Email.Equals(model.Email));

                if (registerTripDb == null)
                    return false;

                _context.RegisterTrip.Remove(registerTripDb);

                _context.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                throw new Exception($"Something went wrong {e.Message}");
            }
        }
        public List<ErrorModel> ValidateTrip(TripModel model)
        {
            List<ErrorModel> errors = new List<ErrorModel>();

            var isNotUnique = _context.Trip.Any(x => x.Name.Equals(model.Name));

            if (isNotUnique)
                errors.Add(new ErrorModel { Message = "Trip name must be unique" });

            return errors;
        }
        public List<ErrorModel> ValidateTripRegestion(RegisterTripModel model, int regestrationTypeId)
        {
            List<ErrorModel> errors = new List<ErrorModel>();

            var providedEmail = !string.IsNullOrEmpty(model.Email);

            if (!providedEmail)
                errors.Add(new ErrorModel { Message = "Email cannot be empty" });

            switch (regestrationTypeId)
            {
                case (int)RegestrationTypeEnum.Register:
                    {
                        var hasRegisteredAlready = _context.RegisterTrip
                            .Where(x => x.TripId == model.TripId).Any(x => x.Email.Equals(model.Email));

                        if (hasRegisteredAlready)
                            errors.Add(new ErrorModel { Message = "A email can be registered for the trip only once" });

                        break;
                    }
                case (int)RegestrationTypeEnum.Unregister:
                    {
                        var isRegistered = _context.RegisterTrip
                            .FirstOrDefault(x => x.TripId == model.TripId && x.Email.Equals(model.Email));

                        if (isRegistered == null)
                            errors.Add(new ErrorModel { Message = "Email hasn't been registered for the trip" });

                        break;
                    }
                default:
                    break;
            }

            return errors;
        }
    }
}
