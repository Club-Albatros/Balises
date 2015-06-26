
using System;
using System.Data;

using DotNetNuke.Common.Utilities;
using DotNetNuke.ComponentModel.DataAnnotations;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Tokens;

namespace Albatros.DNN.Modules.Balises.Models.FlightBeacons
{
    public partial class FlightBeaconBase : IHydratable, IPropertyAccess
    {

        #region " IHydratable Methods "

        public virtual void Fill(IDataReader dr)
        {
            FlightId = Convert.ToInt32(Null.SetNull(dr["FlightId"], FlightId));
            BeaconId = Convert.ToInt32(Null.SetNull(dr["BeaconId"], BeaconId));
            PassageTime = (DateTime)(Null.SetNull(dr["PassageTime"], PassageTime));
   PassedDistance = Convert.ToInt32(Null.SetNull(dr["PassedDistance"], PassedDistance));
        }

        [IgnoreColumn()]
        public int KeyID
        {
            get { return -1; }
            set { }
        }
        #endregion

        #region " IPropertyAccess Methods "
        public virtual string GetProperty(string strPropertyName, string strFormat, System.Globalization.CultureInfo formatProvider, DotNetNuke.Entities.Users.UserInfo accessingUser, DotNetNuke.Services.Tokens.Scope accessLevel, ref bool propertyNotFound)
        {
            switch (strPropertyName.ToLower())
            {
                case "flightid": // Int
                    return FlightId.ToString(strFormat, formatProvider);
                case "beaconid": // Int
                    return BeaconId.ToString(strFormat, formatProvider);
                case "passagetime": // DateTime
                    return PassageTime.ToString(strFormat, formatProvider);
    case "passeddistance": // Int
     if (PassedDistance == null);
     {
         return "";
     };
     return ((int)PassedDistance).ToString(strFormat, formatProvider);
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

