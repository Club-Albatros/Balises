using System.Runtime.Serialization;
using DotNetNuke.ComponentModel.DataAnnotations;
using System;

namespace Albatros.Balises.Core.Models.Comments
{

    [TableName("vw_Albatros_Balises_Comments")]
    [PrimaryKey("CommentId", AutoIncrement = true)]
    [DataContract]
    [Scope("FlightId")]                
    public partial class Comment  : CommentBase 
    {

        #region .ctor
        public Comment()  : base() 
        {
        }
        #endregion

        #region Properties
        [DataMember]
        public string DisplayName { get; set; }
        [DataMember]
        public DateTime TakeoffTime { get; set; }
        [DataMember]
        public string TakeoffDescription { get; set; }
        #endregion

        #region Methods
        public CommentBase GetCommentBase()
        {
            CommentBase res = new CommentBase();
             res.CommentId = CommentId;
             res.UserId = UserId;
             res.FlightId = FlightId;
             res.Datime = Datime;
             res.Remarks = Remarks;
            return res;
        }
        #endregion

    }
}
