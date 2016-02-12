
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DotNetNuke.Web.Api;
using Albatros.DNN.Modules.Balises.Common;
using Albatros.Balises.Core.Repositories;

namespace Albatros.DNN.Modules.Balises.Api
{

	public partial class BeaconsController : BalisesApiController
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

