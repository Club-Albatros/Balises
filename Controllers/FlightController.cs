using System.Web.Mvc;
using DotNetNuke.Common;
using Albatros.DNN.Modules.Balises.Common;
using Albatros.Balises.Core.Repositories;
using Albatros.Balises.Core.Models.Flights;

namespace Albatros.DNN.Modules.Balises.Controllers
{
    public class FlightController : BalisesMvcController
    {
        private readonly IFlightRepository _repository;

        public FlightController() : this(FlightRepository.Instance) { }

        public FlightController(IFlightRepository repository)
        {
            Requires.NotNull(repository);
            _repository = repository;
        }

        [HttpGet]
        [BalisesAuthorize(SecurityLevel = SecurityAccessLevel.View)]
        public ActionResult View(int FlightId)
        {
            return View(FlightRepository.Instance.GetFlight(PortalSettings.PortalId, FlightId));
        }

        [HttpGet]
        [BalisesAuthorize(SecurityLevel = SecurityAccessLevel.Pilot)]
        public ActionResult Edit(int FlightId)
        {
            var flight = FlightRepository.Instance.GetFlight(PortalSettings.PortalId, FlightId);
            if (flight == null)
            {
                flight = new Flight() { TakeoffTime = System.DateTime.Now, LandingTime = System.DateTime.Now, EntryMethod = 0 };
            }
            else
            {
                if (flight.UserId != User.UserID)
                {
                    if (!BalisesModuleContext.Security.IsVerifier)
                    {
                        throw new System.Exception("You don't have access to edit this");
                    }
                }
            }
            return View(flight.GetFlightBase());
        }

        [HttpPost]
        [BalisesAuthorize(SecurityLevel = SecurityAccessLevel.Pilot)]
        public ActionResult Edit(FlightBase flight)
        {

            var existingFlight = FlightRepository.Instance.GetFlight(PortalSettings.PortalId, flight.FlightId);
            if (existingFlight == null)
            {
                var newFlight = new FlightBase()
                {
                    PortalId = PortalSettings.PortalId,
                    UserId = User.UserID,
                    TakeoffDescription = flight.TakeoffDescription,
                    TakeoffTime = flight.TakeoffTime,
                    LandingDescription = flight.LandingDescription,
                    LandingTime = flight.LandingTime,
                    Summary = flight.Summary
                };
                newFlight.ReadTakeoffCoordinates(flight.TakeoffCoords);
                newFlight.ReadLandingCoordinates(flight.LandingCoords);
                var newId = FlightRepository.Instance.AddFlight(ref newFlight, User.UserID);
                // add points

                // recalculate stuff
                newFlight.RecalculateDistanceAndTime();
                FlightRepository.Instance.UpdateFlight(newFlight, User.UserID);
                return ReturnRoute(flight.FlightId, View("View", _repository.GetFlight(PortalSettings.PortalId, newId)));
            }
            else
            {
                if (existingFlight.UserId != User.UserID)
                {
                    if (!BalisesModuleContext.Security.IsVerifier)
                    {
                        throw new System.Exception("You don't have access to edit this");
                    }
                }
                if (existingFlight.TakeoffCoords != flight.TakeoffCoords)
                {
                    existingFlight.ReadTakeoffCoordinates(flight.TakeoffCoords);
                }
                existingFlight.TakeoffDescription = flight.TakeoffDescription;
                existingFlight.TakeoffTime = flight.TakeoffTime;
                if (existingFlight.LandingCoords != flight.LandingCoords)
                {
                    existingFlight.ReadLandingCoordinates(flight.LandingCoords);
                }
                existingFlight.LandingDescription = flight.LandingDescription;
                existingFlight.LandingTime = flight.LandingTime;
                existingFlight.Summary = flight.Summary;
                existingFlight.RecalculateDistanceAndTime();
                FlightRepository.Instance.UpdateFlight(existingFlight.GetFlightBase(), User.UserID);

                if (!string.IsNullOrEmpty(existingFlight.TakeoffDescription))
                {
                    SitesRepository.Instance.SetNewSite(existingFlight.TakeoffLatitude, existingFlight.TakeoffLongitude, existingFlight.TakeoffDescription, BalisesModuleContext.Settings.BeaconPassDistanceMeters);
                }
                if (!string.IsNullOrEmpty(existingFlight.LandingDescription))
                {
                    SitesRepository.Instance.SetNewSite(existingFlight.LandingLatitude, existingFlight.LandingLongitude, existingFlight.LandingDescription, BalisesModuleContext.Settings.BeaconPassDistanceMeters);
                }
                return ReturnRoute(flight.FlightId, View("View", _repository.GetFlight(PortalSettings.PortalId, flight.FlightId)));
            }
        }
    }
}