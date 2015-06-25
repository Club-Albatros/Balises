using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using Albatros.DNN.Modules.Balises.Common;
using DotNetNuke.Security;
using DotNetNuke.Web.Api;

namespace Albatros.DNN.Modules.Balises.Controllers
{
    public class DocumentsController : BalisesApiController
    {

        #region " Service Methods "
        [HttpPost()]
        [DnnModuleAuthorize(AccessLevel = SecurityAccessLevel.View)]
        [ValidateAntiForgeryToken()]
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
                var path = new BalisesPath(UserInfo.UserID, "Upload\\" + file.FileName, Settings.BeaconPassDistanceMeters);
                return Request.CreateResponse(HttpStatusCode.OK, path);
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }

        }
        #endregion

    }
}