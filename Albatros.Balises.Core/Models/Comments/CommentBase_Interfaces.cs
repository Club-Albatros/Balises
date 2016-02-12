
using System;
using System.Data;

using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Tokens;

namespace Albatros.Balises.Core.Models.Comments
{
    public partial class CommentBase : IHydratable, IPropertyAccess
    {

        #region IHydratable

        public virtual void Fill(IDataReader dr)
        {
   CommentId = Convert.ToInt32(Null.SetNull(dr["CommentId"], CommentId));
   UserId = Convert.ToInt32(Null.SetNull(dr["UserId"], UserId));
   FlightId = Convert.ToInt32(Null.SetNull(dr["FlightId"], FlightId));
   Datime = (DateTime)(Null.SetNull(dr["Datime"], Datime));
   Remarks = Convert.ToString(Null.SetNull(dr["Remarks"], Remarks));
        }

        [IgnoreColumn()]
        public int KeyID
        {
            get { return CommentId; }
            set { CommentId = value; }
        }
        #endregion

        #region IPropertyAccess
        public virtual string GetProperty(string strPropertyName, string strFormat, System.Globalization.CultureInfo formatProvider, DotNetNuke.Entities.Users.UserInfo accessingUser, DotNetNuke.Services.Tokens.Scope accessLevel, ref bool propertyNotFound)
        {
            switch (strPropertyName.ToLower())
            {
    case "commentid": // Int
     return CommentId.ToString(strFormat, formatProvider);
    case "userid": // Int
     return UserId.ToString(strFormat, formatProvider);
    case "flightid": // Int
     return FlightId.ToString(strFormat, formatProvider);
    case "datime": // DateTime
     return Datime.ToString(strFormat, formatProvider);
    case "remarks": // NVarCharMax
     return PropertyAccess.FormatString(Remarks, strFormat);
                default:
                    propertyNotFound = true;
                    break;
            }

            return Null.NullString;
        }

        [IgnoreColumn()]
        public CacheLevel Cacheability
        {
            get { return CacheLevel.fullyCacheable; }
        }
        #endregion

    }
}

