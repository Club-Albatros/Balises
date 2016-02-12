
using System;
using System.Data;

using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Tokens;

namespace Albatros.Balises.Core.Models.Beacons
{
    public partial class BeaconBase : IHydratable, IPropertyAccess
    {

        #region IHydratable

        public virtual void Fill(IDataReader dr)
        {
            FillAuditFields(dr);
   BeaconId = Convert.ToInt32(Null.SetNull(dr["BeaconId"], BeaconId));
   PortalId = Convert.ToInt32(Null.SetNull(dr["PortalId"], PortalId));
   Code = Convert.ToString(Null.SetNull(dr["Code"], Code));
   Name = Convert.ToString(Null.SetNull(dr["Name"], Name));
   Description = Convert.ToString(Null.SetNull(dr["Description"], Description));
   Coords = Convert.ToString(Null.SetNull(dr["Coords"], Coords));
   Latitude = Convert.ToDouble(Null.SetNull(dr["Latitude"], Latitude));
   Longitude = Convert.ToDouble(Null.SetNull(dr["Longitude"], Longitude));
   Altitude = Convert.ToInt32(Null.SetNull(dr["Altitude"], Altitude));
   Region = Convert.ToInt32(Null.SetNull(dr["Region"], Region));
   Points = Convert.ToInt32(Null.SetNull(dr["Points"], Points));
        }

        [IgnoreColumn()]
        public int KeyID
        {
            get { return BeaconId; }
            set { BeaconId = value; }
        }
        #endregion

        #region IPropertyAccess
        public virtual string GetProperty(string strPropertyName, string strFormat, System.Globalization.CultureInfo formatProvider, DotNetNuke.Entities.Users.UserInfo accessingUser, DotNetNuke.Services.Tokens.Scope accessLevel, ref bool propertyNotFound)
        {
            switch (strPropertyName.ToLower())
            {
    case "beaconid": // Int
     return BeaconId.ToString(strFormat, formatProvider);
    case "portalid": // Int
     return PortalId.ToString(strFormat, formatProvider);
    case "code": // NVarChar
     return PropertyAccess.FormatString(Code, strFormat);
    case "name": // NVarChar
     return PropertyAccess.FormatString(Name, strFormat);
    case "description": // NVarChar
     if (Description == null)
     {
         return "";
     };
     return PropertyAccess.FormatString(Description, strFormat);
    case "coords": // VarChar
     return PropertyAccess.FormatString(Coords, strFormat);
    case "latitude": // Float
     return Latitude.ToString(strFormat, formatProvider);
    case "longitude": // Float
     return Longitude.ToString(strFormat, formatProvider);
    case "altitude": // Int
     return Altitude.ToString(strFormat, formatProvider);
    case "region": // Int
     return Region.ToString(strFormat, formatProvider);
    case "points": // Int
     return Points.ToString(strFormat, formatProvider);
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

