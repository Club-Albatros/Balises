
using System;
using System.Data;

using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Tokens;

namespace Albatros.Balises.Core.Models.FlightBeacons
{
    public partial class FlightBeaconBase : IHydratable, IPropertyAccess
    {

        #region IHydratable

        public virtual void Fill(IDataReader dr)
        {
   FlightId = Convert.ToInt32(Null.SetNull(dr["FlightId"], FlightId));
   BeaconId = Convert.ToInt32(Null.SetNull(dr["BeaconId"], BeaconId));
   PassedDistance = Convert.ToInt32(Null.SetNull(dr["PassedDistance"], PassedDistance));
   PassOrder = Convert.ToInt32(Null.SetNull(dr["PassOrder"], PassOrder));
        }

        [IgnoreColumn()]
        public int KeyID
        {
            get { return Null.NullInteger; }
            set { }
        }
        #endregion

        #region IPropertyAccess
        public virtual string GetProperty(string strPropertyName, string strFormat, System.Globalization.CultureInfo formatProvider, DotNetNuke.Entities.Users.UserInfo accessingUser, DotNetNuke.Services.Tokens.Scope accessLevel, ref bool propertyNotFound)
        {
            switch (strPropertyName.ToLower())
            {
    case "flightid": // Int
     return FlightId.ToString(strFormat, formatProvider);
    case "beaconid": // Int
     return BeaconId.ToString(strFormat, formatProvider);
    case "passeddistance": // Int
     if (PassedDistance == null)
     {
         return "";
     };
     return ((int)PassedDistance).ToString(strFormat, formatProvider);
    case "passorder": // Int
     return PassOrder.ToString(strFormat, formatProvider);
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

