using Albatros.Balises.Core.Common;
using Albatros.Balises.Core.Models.FlightBeacons;
using Albatros.Balises.Core.Models.Flights;
using Albatros.Balises.Core.Repositories;
using DotNetNuke.Entities.Portals;

namespace Albatros.Balises.Core.IGC
{
    public class IgcController
    {
        public static Flight AddFlightToUser(int portalId, int userId, string igcText, int beaconPassDistanceMeters)
        {
            var path = new BalisesPath(userId, new IgcFile(igcText), beaconPassDistanceMeters);
            var flight = FlightRepository.Instance.FindFlight(portalId, userId, path.Igc.DetectedStart);
            if (flight == null)
            {
                var f = new FlightBase()
                {
                    FlightStart = path.Igc.DetectedStart,
                    DurationMins = (int)path.Igc.FlightTime.TotalMinutes,
                    Distance = path.OfficialDistance,
                    LandingCoords = path.Igc.Landing.ToSwissCoordinates(),
                    LandingDescription = path.Landing.Description,
                    LandingLatitude = path.Igc.Landing.Latitude,
                    LandingLongitude = path.Igc.Landing.Longitude,
                    LandingTime = path.Igc.DetectedLandingTime,
                    PortalId = portalId,
                    StartCoords = path.Igc.Takeoff.ToSwissCoordinates(),
                    StartDescription = path.TakeOff.Description,
                    StartLatitude = path.Igc.Takeoff.Latitude,
                    StartLongitude = path.Igc.Takeoff.Longitude,
                    Summary = path.Igc.Report(),
                    Validated = false,
                    ValidatedOnDate = new System.DateTime(1900, 1, 1),
                    Rejected = false
                };
                FlightRepository.Instance.AddFlight(ref f, userId);
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
                var fullPath = string.Format("{0}\\Albatros\\Balises\\{1}", PortalSettings.Current.HomeDirectoryMapPath, userId);
                if (!System.IO.Directory.Exists(fullPath))
                {
                    System.IO.Directory.CreateDirectory(fullPath);
                }
                fullPath += "\\" + string.Format("path-{0}.igc", f.FlightId);
                using (var s = new System.IO.StreamWriter(fullPath))
                {
                    s.Write(igcText);
                }
                flight = FlightRepository.Instance.GetFlight(portalId, f.FlightId);
            }
            return flight;
        }
    }
}
