
using System.Net;
using System.Net.Http;
using System.Web.Http;

using DotNetNuke.Web.Api;

namespace Albatros.DNN.Modules.Balises.Controllers
{

	public partial class FlightsController : DnnApiController
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

