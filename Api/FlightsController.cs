using System.Net;
using System.Net.Http;
using System.Web.Http;
using DotNetNuke.Web.Api;
using Albatros.DNN.Modules.Balises.Common;
using System.Web;
using System.IO;
using Albatros.Balises.Core.Repositories;
using Albatros.Balises.Core.Models.Flights;
using System.Text.RegularExpressions;
using Albatros.Balises.Core.IGC;
using System.Collections.Generic;

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
            var sortField = "TakeoffTime";
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
            if (file.FileName.ToLower().EndsWith(".igc"))
            {
                var igcText = "";
                using (var s = new StreamReader(file.InputStream))
                {
                    igcText = s.ReadToEnd();
                }
                return Request.CreateResponse(HttpStatusCode.OK, IgcController.AddFlightToUser(
                        PortalSettings.PortalId,
                        UserInfo.UserID,
                        igcText,
                        BalisesModuleContext.Settings.BeaconPassDistanceMeters));
            }
            else if (file.FileName.ToLower().EndsWith(".zip"))
            {
                var res = new List<Flight>();
                using (var zip = new System.IO.Compression.ZipArchive(file.InputStream))
                {
                    foreach (var entry in zip.Entries)
                    {
                        if (entry.Name.ToLower().EndsWith(".igc") & entry.Length > 0)
                        {
                            using (var stream = entry.Open())
                            {
                                var igcText = "";
                                using (var s = new StreamReader(stream))
                                {
                                    igcText = s.ReadToEnd();
                                }
                                res.Add(IgcController.AddFlightToUser(
                                    PortalSettings.PortalId,
                                    UserInfo.UserID,
                                    igcText,
                                    BalisesModuleContext.Settings.BeaconPassDistanceMeters));
                            }
                        }
                    }
                }
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Bad file");
            }
        }

        public class ChangeStatusDTO
        {
            public int NewStatus { get; set; }
        }

        [HttpPost]
        [BalisesAuthorize(SecurityLevel = SecurityAccessLevel.Pilot)]
        [ValidateAntiForgeryToken]
        public HttpResponseMessage ChangeStatus(int id, [FromBody]ChangeStatusDTO data)
        {
            var flight = FlightRepository.Instance.GetFlight(PortalSettings.PortalId, id);
            if (flight == null) { return ServiceError("Flight doesn't exist"); }
            if (BalisesModuleContext.Security.IsVerifier)
            {
                flight.Status = data.NewStatus;
                FlightRepository.Instance.UpdateFlight(flight.GetFlightBase(), UserInfo.UserID);
                return Request.CreateResponse(HttpStatusCode.OK, data.NewStatus);
            }
            else if (BalisesModuleContext.Security.IsPilot && UserInfo.UserID == flight.UserId)
            {
                if (data.NewStatus == 0 | data.NewStatus == 1 | data.NewStatus == 3)
                {
                    flight.Status = data.NewStatus;
                    FlightRepository.Instance.UpdateFlight(flight.GetFlightBase(), UserInfo.UserID);
                    return Request.CreateResponse(HttpStatusCode.OK, data.NewStatus);
                }
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest, "Not allowed");
        }

    }
}

