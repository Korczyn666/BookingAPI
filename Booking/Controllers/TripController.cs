using Booking.Api.Models.Enum;
using Booking.Api.Models.Web;
using Booking.Service.Interfaces;
using Booking.Service.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Booking.WebApi.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("[controller]/[action]")]
    public class TripController : ControllerBase
    {
        public readonly ITripService _tripService;
        public TripController(ITripService tripService)
        {
            _tripService = tripService;
        }
        #region Manage Trips

        [HttpPost]
        public IActionResult AddTrip([FromBody] TripModel model)
        {
            var validationErrors = _tripService.ValidateTrip(model);

            string json;

            if (!validationErrors.Any())
            {
                var trip = _tripService.AddTrip(model);

                json = JsonConvert.SerializeObject(trip);

                return Ok(json);
            }
            else
            {
                json = JsonConvert.SerializeObject(validationErrors);

                return BadRequest(json);
            }
        }

        [HttpPost]
        public IActionResult EditTrip([FromBody] TripModel model)
        {
            var validationErrors = _tripService.ValidateTrip(model);

            string json;

            if (!validationErrors.Any())
            {
                var trip = _tripService.EditTrip(model);

                json = JsonConvert.SerializeObject(trip);

                return Ok(json);
            }
            else
            {
                json = JsonConvert.SerializeObject(validationErrors);

                return BadRequest(json);
            }
        }

        [HttpDelete]
        public IActionResult DeleteTrip(int tripId)
        {
            if (!_tripService.DeleteTrip(tripId))
                return BadRequest(new { message = "Error has occured while deleting the trip" });
            return Ok(true);
        }

        #endregion
        #region Search Trip

        [HttpGet]
        public List<TripModel> GetTripList()
        {
            return _tripService.GetTripList();
        }

        [HttpGet]
        public List<TripModel> GetTripListByCountry(int countryId)
        {
            return _tripService.GetTripListByCountry(countryId);
        }

        #endregion
        #region Single Trip

        [HttpGet]
        public TripModel GetTrip(int tripId)
        {
            return _tripService.GetTrip(tripId);
        }

        [HttpPost]
        public IActionResult RegisterTrip([FromBody] RegisterTripModel model)
        {
            var validationErrors = _tripService.ValidateTripRegestion(model, (int)RegestrationTypeEnum.Register);

            string json;

            if (!validationErrors.Any())
            {
                var trip =_tripService.RegisterTrip(model);

                json = JsonConvert.SerializeObject(trip);

                return Ok(json);
            }
            else
            {
                json = JsonConvert.SerializeObject(validationErrors);

                return BadRequest(json);
            }
        }

        [HttpPost]
        public IActionResult UnregisterTrip([FromBody] RegisterTripModel model)
        {
            var validationErrors = _tripService.ValidateTripRegestion(model, (int)RegestrationTypeEnum.Unregister);

            string json;

            if (!validationErrors.Any())
            {
                if (!_tripService.UnregisterTrip(model))
                    return BadRequest(new { message = "Error has occured while unregistering from the trip" });
                return Ok(true);
            }
            else
            {
                json = JsonConvert.SerializeObject(validationErrors);

                return BadRequest(json);
            }
        }

        #endregion

    }
}
