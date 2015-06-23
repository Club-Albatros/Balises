
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
            ViewOrder = Convert.ToInt32(Null.SetNull(dr["ViewOrder"], ViewOrder));
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
                case "vieworder": // Int
                    return ViewOrder.ToString(strFormat, formatProvider);
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

