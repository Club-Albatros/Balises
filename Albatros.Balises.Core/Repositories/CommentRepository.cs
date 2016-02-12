using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using DotNetNuke.Collections;
using DotNetNuke.Common;
using DotNetNuke.Data;
using DotNetNuke.Framework;
using Albatros.Balises.Core.Data;
using Albatros.Balises.Core.Models.Comments;

namespace Albatros.Balises.Core.Repositories
{

	public class CommentRepository : ServiceLocator<ICommentRepository, CommentRepository>, ICommentRepository
 {
        protected override Func<ICommentRepository> GetFactory()
        {
            return () => new CommentRepository();
        }
        public IEnumerable<Comment> GetComments(int flightId)
        {
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<Comment>();
                return rep.Get(flightId);
            }
        }
        public Comment GetComment(int flightId, int commentId)
        {
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<Comment>();
                return rep.GetById(commentId, flightId);
            }
        }
        public int AddComment(ref CommentBase comment)
        {
            Requires.NotNull(comment);
            Requires.PropertyNotNegative(comment, "FlightId");
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<CommentBase>();
                rep.Insert(comment);
            }
            return comment.CommentId;
        }
        public void DeleteComment(CommentBase comment)
        {
            Requires.NotNull(comment);
            Requires.PropertyNotNegative(comment, "CommentId");
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<CommentBase>();
                rep.Delete(comment);
            }
        }
        public void DeleteComment(int flightId, int commentId)
        {
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<CommentBase>();
                rep.Delete("WHERE FlightId = @0 AND CommentId = @1", flightId, commentId);
            }
        }
        public void UpdateComment(CommentBase comment)
        {
            Requires.NotNull(comment);
            Requires.PropertyNotNegative(comment, "CommentId");
            using (var context = DataContext.Instance())
            {
                var rep = context.GetRepository<CommentBase>();
                rep.Update(comment);
            }
        } 
 }

    public interface ICommentRepository
    {
        IEnumerable<Comment> GetComments(int flightId);
        Comment GetComment(int flightId, int commentId);
        int AddComment(ref CommentBase comment);
        void DeleteComment(CommentBase comment);
        void DeleteComment(int flightId, int commentId);
        void UpdateComment(CommentBase comment);
    }
}

