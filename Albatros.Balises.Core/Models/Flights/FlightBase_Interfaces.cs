
using System;
using System.Data;

using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Tokens;

namespace Albatros.Balises.Core.Models.Flights
{
    public partial class FlightBase : IHydratable, IPropertyAccess
    {

        #region IHydratable

        public virtual void Fill(IDataReader dr)
        {
            FillAuditFields(dr);
   FlightId = Convert.ToInt32(Null.SetNull(dr["FlightId"], FlightId));
   PortalId = Convert.ToInt32(Null.SetNull(dr["PortalId"], PortalId));
   UserId = Convert.ToInt32(Null.SetNull(dr["UserId"], UserId));
   TakeoffDescription = Convert.ToString(Null.SetNull(dr["TakeoffDescription"], TakeoffDescription));
   TakeoffTime = (DateTime)(Null.SetNull(dr["TakeoffTime"], TakeoffTime));
   TakeoffCoords = Convert.ToString(Null.SetNull(dr["TakeoffCoords"], TakeoffCoords));
   TakeoffLatitude = Convert.ToDouble(Null.SetNull(dr["TakeoffLatitude"], TakeoffLatitude));
   TakeoffLongitude = Convert.ToDouble(Null.SetNull(dr["TakeoffLongitude"], TakeoffLongitude));
   LandingDescription = Convert.ToString(Null.SetNull(dr["LandingDescription"], LandingDescription));
   LandingTime = (DateTime)(Null.SetNull(dr["LandingTime"], LandingTime));
   LandingCoords = Convert.ToString(Null.SetNull(dr["LandingCoords"], LandingCoords));
   LandingLatitude = Convert.ToDouble(Null.SetNull(dr["LandingLatitude"], LandingLatitude));
   LandingLongitude = Convert.ToDouble(Null.SetNull(dr["LandingLongitude"], LandingLongitude));
   DurationMins = Convert.ToInt32(Null.SetNull(dr["DurationMins"], DurationMins));
   Distance = Convert.ToInt32(Null.SetNull(dr["Distance"], Distance));
   Status = Convert.ToInt32(Null.SetNull(dr["Status"], Status));
   EntryMethod = Convert.ToInt32(Null.SetNull(dr["EntryMethod"], EntryMethod));
   Summary = Convert.ToString(Null.SetNull(dr["Summary"], Summary));
   ValidatedByUserID = Convert.ToInt32(Null.SetNull(dr["ValidatedByUserID"], ValidatedByUserID));
   ValidatedOnDate = (DateTime)(Null.SetNull(dr["ValidatedOnDate"], ValidatedOnDate));
        }

        [IgnoreColumn()]
        public int KeyID
        {
            get { return FlightId; }
            set { FlightId = value; }
        }
        #endregion

        #region IPropertyAccess
        public virtual string GetProperty(string strPropertyName, string strFormat, System.Globalization.CultureInfo formatProvider, DotNetNuke.Entities.Users.UserInfo accessingUser, DotNetNuke.Services.Tokens.Scope accessLevel, ref bool propertyNotFound)
        {
            switch (strPropertyName.ToLower())
            {
    case "flightid": // Int
     return FlightId.ToString(strFormat, formatProvider);
    case "portalid": // Int
     return PortalId.ToString(strFormat, formatProvider);
    case "userid": // Int
     return UserId.ToString(strFormat, formatProvider);
    case "takeoffdescription": // NVarChar
     return PropertyAccess.FormatString(TakeoffDescription, strFormat);
    case "takeofftime": // DateTime
     return TakeoffTime.ToString(strFormat, formatProvider);
    case "takeoffcoords": // NVarChar
     if (TakeoffCoords == null)
     {
         return "";
     };
     return PropertyAccess.FormatString(TakeoffCoords, strFormat);
    case "takeofflatitude": // Float
     return TakeoffLatitude.ToString(strFormat, formatProvider);
    case "takeofflongitude": // Float
     return TakeoffLongitude.ToString(strFormat, formatProvider);
    case "landingdescription": // NVarChar
     if (LandingDescription == null)
     {
         return "";
     };
     return PropertyAccess.FormatString(LandingDescription, strFormat);
    case "landingtime": // DateTime
     return LandingTime.ToString(strFormat, formatProvider);
    case "landingcoords": // NVarChar
     if (LandingCoords == null)
     {
         return "";
     };
     return PropertyAccess.FormatString(LandingCoords, strFormat);
    case "landinglatitude": // Float
     return LandingLatitude.ToString(strFormat, formatProvider);
    case "landinglongitude": // Float
     return LandingLongitude.ToString(strFormat, formatProvider);
    case "durationmins": // Int
     return DurationMins.ToString(strFormat, formatProvider);
    case "distance": // Int
     return Distance.ToString(strFormat, formatProvider);
    case "status": // Int
     return Status.ToString(strFormat, formatProvider);
    case "entrymethod": // Int
     return EntryMethod.ToString(strFormat, formatProvider);
    case "summary": // NVarChar
     if (Summary == null)
     {
         return "";
     };
     return PropertyAccess.FormatString(Summary, strFormat);
    case "validatedbyuserid": // Int
     if (ValidatedByUserID == null)
     {
         return "";
     };
     return ((int)ValidatedByUserID).ToString(strFormat, formatProvider);
    case "validatedondate": // DateTime
     if (ValidatedOnDate == null)
     {
         return "";
     };
     return ((DateTime)ValidatedOnDate).ToString(strFormat, formatProvider);
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

