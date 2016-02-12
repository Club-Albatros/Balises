
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DotNetNuke.Web.Api;
using Albatros.DNN.Modules.Balises.Common;

namespace Albatros.DNN.Modules.Balises.Api
{

	public partial class FlightBeaconsController : BalisesApiController
	{

		#region " Service Methods "
		[HttpGet()]
		[DnnModuleAuthorize(AccessLevel = DotNetNuke.Security.SecurityAccessLevel.View)]
		public HttpResponseMessage MyMethod(int id)
		{
			bool res = true;
			return Request.CreateResponse(HttpStatusCode.OK, res);
		}
		#endregion

	}
}

