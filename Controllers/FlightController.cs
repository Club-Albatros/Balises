using System.Web.Mvc;
using DotNetNuke.Common;
using Albatros.DNN.Modules.Balises.Common;
using Albatros.Balises.Core.Repositories;
using Albatros.Balises.Core.Models.Flights;
using Albatros.Balises.Core.Common;
using System.Collections.Generic;

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
                flight = new Flight() { TakeoffTime = System.DateTime.Now, LandingTime = System.DateTime.Now, EntryMethod = 0, Category = 0 };
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
                if (flight.Status > 3)
                {
                    throw new System.Exception("This flight is locked");
                }
            }
            return View(flight.GetFlightBase());
        }

        [HttpPost]
        [BalisesAuthorize(SecurityLevel = SecurityAccessLevel.Pilot)]
        [PostButton(Name = "action", Argument = "save")]
        public ActionResult Edit(FlightBase flight)
        {

            var returnUserId = User.UserID;
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
                    Summary = flight.Summary,
                    TakeoffAltitude = flight.TakeoffAltitude,
                    LandingAltitude = flight.LandingAltitude,
                    Category = flight.Category
                };
                newFlight.ReadTakeoffCoordinates(flight.TakeoffCoords);
                newFlight.ReadLandingCoordinates(flight.LandingCoords);
                var newId = FlightRepository.Instance.AddFlight(ref newFlight, User.UserID);
                FlightBeaconRepository.Instance.ProcessFlightBeacons(newId, newFlight.TakeoffTime, Newtonsoft.Json.JsonConvert.DeserializeObject<List<PathBeacon>>(flight.BeaconList));
                newFlight.RecalculateTotals();
                newFlight.CheckLandingBeacon(BalisesModuleContext.Settings.BeaconPassDistanceMeters);
                FlightRepository.Instance.UpdateFlight(newFlight, User.UserID);
                return ReturnRoute(User.UserID, View("View", _repository.GetFlight(PortalSettings.PortalId, newId)));
            }
            else
            {
                returnUserId = existingFlight.UserId;
                if (existingFlight.UserId != User.UserID)
                {
                    if (!BalisesModuleContext.Security.IsVerifier)
                    {
                        throw new System.Exception("You don't have access to edit this");
                    }
                }
                if (existingFlight.Status > 3)
                {
                    throw new System.Exception("This flight is locked");
                }
                if (existingFlight.TakeoffCoords != flight.TakeoffCoords)
                {
                    existingFlight.ReadTakeoffCoordinates(flight.TakeoffCoords);
                }
                existingFlight.TakeoffAltitude = flight.TakeoffAltitude;
                existingFlight.TakeoffDescription = flight.TakeoffDescription;
                existingFlight.TakeoffTime = flight.TakeoffTime;
                if (existingFlight.LandingCoords != flight.LandingCoords)
                {
                    existingFlight.ReadLandingCoordinates(flight.LandingCoords);
                }
                existingFlight.LandingAltitude = flight.LandingAltitude;
                existingFlight.LandingDescription = flight.LandingDescription;
                existingFlight.LandingTime = flight.LandingTime;
                existingFlight.Summary = flight.Summary;
                existingFlight.Category = flight.Category;
                existingFlight.RecalculateTotals();
                existingFlight.CheckLandingBeacon(BalisesModuleContext.Settings.BeaconPassDistanceMeters);
                FlightRepository.Instance.UpdateFlight(existingFlight.GetFlightBase(), User.UserID);
                if (existingFlight.EntryMethod == 0)
                {
                    FlightBeaconRepository.Instance.ProcessFlightBeacons(existingFlight.FlightId, existingFlight.TakeoffTime, Newtonsoft.Json.JsonConvert.DeserializeObject<List<PathBeacon>>(flight.BeaconList));
                }
                if (!string.IsNullOrEmpty(existingFlight.TakeoffDescription))
                {
                    SitesRepository.Instance.SetNewSite(existingFlight.TakeoffLatitude, existingFlight.TakeoffLongitude, existingFlight.TakeoffDescription, BalisesModuleContext.Settings.BeaconPassDistanceMeters);
                }
                if (!string.IsNullOrEmpty(existingFlight.LandingDescription))
                {
                    SitesRepository.Instance.SetNewSite(existingFlight.LandingLatitude, existingFlight.LandingLongitude, existingFlight.LandingDescription, BalisesModuleContext.Settings.BeaconPassDistanceMeters);
                }
                return ReturnRoute(returnUserId, View("View", _repository.GetFlight(PortalSettings.PortalId, flight.FlightId)));
            }
        }

        [HttpPost]
        [BalisesAuthorize(SecurityLevel = SecurityAccessLevel.Pilot)]
        [PostButton(Name = "action", Argument = "delete")]
        public ActionResult Delete(FlightBase flight)
        {
            var existingFlight = FlightRepository.Instance.GetFlight(PortalSettings.PortalId, flight.FlightId);
            if (existingFlight != null)
            {
                if (existingFlight.UserId != User.UserID)
                {
                    if (!BalisesModuleContext.Security.IsVerifier)
                    {
                        throw new System.Exception("You don't have access to edit this");
                    }
                }
                FlightRepository.Instance.DeleteFlight(PortalSettings.PortalId, flight.FlightId);
            }
            return ReturnRoute(existingFlight.UserId, View("Index", "Home"));
        }
    }
}