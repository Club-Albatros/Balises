
using System.Net;
using System.Net.Http;
using System.Web.Http;

using DotNetNuke.Web.Api;

namespace Albatros.DNN.Modules.Balises.Controllers
{

	public partial class BeaconsController : DnnApiController
	{

		#region " Service Methods "
		[HttpGet()]
		[DnnModuleAuthorize(AccessLevel = DotNetNuke.Security.SecurityAccessLevel.View)]
		public HttpResponseMessage List()
		{
			return Request.CreateResponse(HttpStatusCode.OK, GetBeacons(ActiveModule.PortalID));
		}
		#endregion

	}
}

