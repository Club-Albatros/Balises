using System.Net;
using System.Net.Http;
using System.Web.Http;
using DotNetNuke.Web.Api;
using Albatros.DNN.Modules.Balises.Common;
using System.Web;
using System.IO;
using Albatros.Balises.Core.Common;
using Albatros.Balises.Core.Repositories;
using Albatros.Balises.Core.Models.FlightBeacons;
using Albatros.Balises.Core.Models.Flights;
using System.Text.RegularExpressions;

namespace Albatros.DNN.Modules.Balises.Api
{

    public partial class FlightsController : BalisesApiController
    {

        [HttpPost]
        [BalisesAuthorize(SecurityLevel = SecurityAccessLevel.View)]
        public HttpResponseMessage List(int id, BootGridPostData postData)
        {
            var res = new BootGridResponseData<Flight>();
            res.current = postData.current;
            res.rowCount = postData.rowCount;
            var sortField = "FlightStart";
            var sortOrder = "desc";
            foreach (var key in HttpContext.Current.Request.Form.AllKeys)
            {
                var m = Regex.Match(key, "sort\\[([^\\]]+)\\]");
                if (m.Success)
                {
                    sortField = m.Groups[1].Value;
                    sortOrder = HttpContext.Current.Request.Form[key];
                }
            }
            var searchRecs = FlightRepository.Instance.GetFlightsByPilot(PortalSettings.PortalId, id, sortField, postData.searchPhrase, sortField, sortOrder, postData.current - 1, postData.rowCount);
            res.total = searchRecs.TotalCount;
            res.rows = searchRecs;
            return Request.CreateResponse(HttpStatusCode.OK, res);
        }

        [HttpPost]
        [BalisesAuthorize(SecurityLevel = SecurityAccessLevel.Pilot)]
        [ValidateAntiForgeryToken]
        public HttpResponseMessage Upload()
        {

            HttpContext context = HttpContext.Current;

            // Verify Folder
            var folderPath = string.Format("{0}\\Albatros\\Balises\\{1}\\Upload", PortalSettings.HomeDirectoryMapPath, UserInfo.UserID);
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            // Save File
            HttpPostedFile file = context.Request.Files[0];
            if (!file.FileName.ToLower().EndsWith(".igc"))
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad file");
            }
            var filePath = string.Format("{0}\\{1}", folderPath, file.FileName);
            file.SaveAs(filePath);

            // Parse File
            try
            {
                var path = new BalisesPath(UserInfo.UserID, "Upload\\" + file.FileName, BalisesModuleContext.Settings.BeaconPassDistanceMeters);
                var flight = FlightRepository.Instance.FindFlight(PortalSettings.PortalId, UserInfo.UserID, path.Igc.DetectedStart);
                if (flight == null)
                {
                    var f = new Albatros.Balises.Core.Models.Flights.FlightBase()
                    {
                        FlightStart = path.Igc.DetectedStart,
                        DurationMins = (int)path.Igc.FlightTime.TotalMinutes,
                        Distance = (int)path.Igc.Distance,
                        LandingCoords = path.Igc.Landing.ToSwissCoordinates(),
                        LandingDescription = path.Landing.Description,
                        LandingLatitude = path.Igc.Landing.Latitude,
                        LandingLongitude = path.Igc.Landing.Longitude,
                        LandingTime = path.Igc.DetectedLandingTime,
                        PortalId = PortalSettings.PortalId,
                        StartCoords = path.Igc.Takeoff.ToSwissCoordinates(),
                        StartDescription = path.TakeOff.Description,
                        StartLatitude = path.Igc.Takeoff.Latitude,
                        StartLongitude = path.Igc.Takeoff.Longitude,
                        Summary = path.Igc.Report(),
                        Validated = false,
                        ValidatedOnDate = new System.DateTime(1900, 1, 1),
                        Rejected = false
                    };
                    FlightRepository.Instance.AddFlight(ref f, UserInfo.UserID);
                    foreach (var pt in path.PassedBeacons)
                    {
                        var fb = new FlightBeaconBase()
                        {
                            FlightId = f.FlightId,
                            BeaconId = pt.BeaconId,
                            PassageTime = pt.PassageTime,
                            PassedDistance = pt.PassedDistance
                        };
                        FlightBeaconRepository.Instance.AddFlightBeacon(fb);
                    }
                    flight = FlightRepository.Instance.GetFlight(PortalSettings.PortalId, f.FlightId);
                }
                return Request.CreateResponse(HttpStatusCode.OK, flight);
            }
            catch (System.Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }

        }
    }
}

