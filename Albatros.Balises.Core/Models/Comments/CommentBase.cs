
using System;
using System.Runtime.Serialization;
using DotNetNuke.ComponentModel.DataAnnotations;
using Albatros.Balises.Core.Common;
using Albatros.Balises.Core.Data;

namespace Albatros.Balises.Core.Models.Comments
{
    [TableName("Albatros_Balises_Comments")]
    [PrimaryKey("CommentId", AutoIncrement = true)]
    [DataContract]
    [Scope("FlightId")]
    public partial class CommentBase     {

        #region .ctor
        public CommentBase()
        {
            CommentId = -1;
        }
        #endregion

        #region Properties
        [DataMember]
        public int CommentId { get; set; }
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public int FlightId { get; set; }
        [DataMember]
        public DateTime Datime { get; set; }
        [DataMember]
        public string Remarks { get; set; }
        #endregion

        #region Methods
        public void ReadCommentBase(CommentBase comment)
        {
            if (comment.CommentId > -1)
                CommentId = comment.CommentId;

            if (comment.UserId > -1)
                UserId = comment.UserId;

            if (comment.FlightId > -1)
                FlightId = comment.FlightId;

            Datime = comment.Datime;

            if (!String.IsNullOrEmpty(comment.Remarks))
                Remarks = comment.Remarks;

        }
        #endregion

    }
}



