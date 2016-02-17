using System.Net;
using System.Net.Http;
using System.Web.Http;
using DotNetNuke.Web.Api;
using Albatros.DNN.Modules.Balises.Common;
using Albatros.Balises.Core.Repositories;
using System.Collections.Generic;
using Albatros.Balises.Core.Models.Comments;

namespace Albatros.DNN.Modules.Balises.Api
{

    public partial class CommentsController : BalisesApiController
    {

        [HttpGet]
        [DnnModuleAuthorize(AccessLevel = DotNetNuke.Security.SecurityAccessLevel.View)]
        public HttpResponseMessage List(int id, int pageIndex, int pageSize)
        {
            try
            {
                var list = CommentRepository.Instance.GetComments(id, pageIndex, pageSize);
                return Request.CreateResponse(HttpStatusCode.OK, list);
            }
            catch
            {
                var res = new List<Comment>();
                return Request.CreateResponse(HttpStatusCode.OK, res);
            }
        }

        [HttpPost]
        [DnnModuleAuthorize(AccessLevel = DotNetNuke.Security.SecurityAccessLevel.View)]
        public HttpResponseMessage Add(int id, [FromBody]CommentBase data)
        {
            if (UserInfo.UserID <= 0)
            {
                return AccessViolation("You need to be logged in to submit a comment");
            }
            data.FlightId = id;
            data.UserId = UserInfo.UserID;
            data.Datime = System.DateTime.Now;
            CommentRepository.Instance.AddComment(ref data);
            return Request.CreateResponse(HttpStatusCode.OK, CommentRepository.Instance.GetComment(id, data.CommentId));
        }

        [HttpPost]
        [DnnModuleAuthorize(AccessLevel = DotNetNuke.Security.SecurityAccessLevel.View)]
        public HttpResponseMessage Delete(int id, [FromBody]CommentBase data)
        {
            var comment = CommentRepository.Instance.GetComment(id, data.CommentId);
            if (comment != null)
            {
                if (comment.UserId == UserInfo.UserID | BalisesModuleContext.Security.IsAdmin)
                {
                    CommentRepository.Instance.DeleteComment(id, data.CommentId);
                }
                else
                {
                    return AccessViolation("You're not allowed to delete this comment");
                }
            }
            return Request.CreateResponse(HttpStatusCode.OK, "");
        }

    }
}

