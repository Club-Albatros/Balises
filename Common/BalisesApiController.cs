using DotNetNuke.Web.Api;
using System.Net;
using System.Net.Http;

namespace Albatros.DNN.Modules.Balises.Common
{
    public class BalisesApiController : DnnApiController
    {
        private ContextHelper _balisesModuleContext;
        public ContextHelper BalisesModuleContext
        {
            get { return _balisesModuleContext ?? (_balisesModuleContext = new ContextHelper(this)); }
        }

        public HttpResponseMessage ServiceError(string message) {
            return Request.CreateResponse(HttpStatusCode.InternalServerError, message);
        }

        public HttpResponseMessage AccessViolation(string message)
        {
            return Request.CreateResponse(HttpStatusCode.Unauthorized, message);
        }

    }
}