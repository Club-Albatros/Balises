
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
   FlightStart = (DateTime)(Null.SetNull(dr["FlightStart"], FlightStart));
   DurationMins = Convert.ToInt32(Null.SetNull(dr["DurationMins"], DurationMins));
   Distance = Convert.ToInt32(Null.SetNull(dr["Distance"], Distance));
   Category = Convert.ToInt32(Null.SetNull(dr["Category"], Category));
   StartDescription = Convert.ToString(Null.SetNull(dr["StartDescription"], StartDescription));
   StartCoords = Convert.ToString(Null.SetNull(dr["StartCoords"], StartCoords));
   StartLatitude = Convert.ToDouble(Null.SetNull(dr["StartLatitude"], StartLatitude));
   StartLongitude = Convert.ToDouble(Null.SetNull(dr["StartLongitude"], StartLongitude));
   LandingDescription = Convert.ToString(Null.SetNull(dr["LandingDescription"], LandingDescription));
   LandingTime = (DateTime)(Null.SetNull(dr["LandingTime"], LandingTime));
   LandingCoords = Convert.ToString(Null.SetNull(dr["LandingCoords"], LandingCoords));
   LandingLatitude = Convert.ToDouble(Null.SetNull(dr["LandingLatitude"], LandingLatitude));
   LandingLongitude = Convert.ToDouble(Null.SetNull(dr["LandingLongitude"], LandingLongitude));
   Summary = Convert.ToString(Null.SetNull(dr["Summary"], Summary));
   Validated = Convert.ToBoolean(Null.SetNull(dr["Validated"], Validated));
   Rejected = Convert.ToBoolean(Null.SetNull(dr["Rejected"], Rejected));
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
    case "flightstart": // DateTime
     return FlightStart.ToString(strFormat, formatProvider);
    case "durationmins": // Int
     return DurationMins.ToString(strFormat, formatProvider);
    case "distance": // Int
     return Distance.ToString(strFormat, formatProvider);
    case "category": // Int
     return Category.ToString(strFormat, formatProvider);
    case "startdescription": // NVarChar
     return PropertyAccess.FormatString(StartDescription, strFormat);
    case "startcoords": // NVarChar
     if (StartCoords == null)
     {
         return "";
     };
     return PropertyAccess.FormatString(StartCoords, strFormat);
    case "startlatitude": // Float
     return StartLatitude.ToString(strFormat, formatProvider);
    case "startlongitude": // Float
     return StartLongitude.ToString(strFormat, formatProvider);
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
    case "summary": // NVarChar
     if (Summary == null)
     {
         return "";
     };
     return PropertyAccess.FormatString(Summary, strFormat);
    case "validated": // Bit
     return Validated.ToString();
    case "rejected": // Bit
     return Rejected.ToString();
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

