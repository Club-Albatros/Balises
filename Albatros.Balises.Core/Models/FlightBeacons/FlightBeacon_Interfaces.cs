
using System;
using System.Data;
using System.Xml.Serialization;

using DotNetNuke.Common.Utilities;
using DotNetNuke.Services.Tokens;

namespace Albatros.Balises.Core.Models.FlightBeacons
{

 [Serializable(), XmlRoot("FlightBeacon")]
 public partial class FlightBeacon
 {

  #region IHydratable
  public override void Fill(IDataReader dr)
  {
   base.Fill(dr);
   Code = Convert.ToString(Null.SetNull(dr["Code"], Code));
   Name = Convert.ToString(Null.SetNull(dr["Name"], Name));
   Description = Convert.ToString(Null.SetNull(dr["Description"], Description));
   Points = Convert.ToInt32(Null.SetNull(dr["Points"], Points));
   Coords = Convert.ToString(Null.SetNull(dr["Coords"], Coords));
   Latitude = Convert.ToDouble(Null.SetNull(dr["Latitude"], Latitude));
   Longitude = Convert.ToDouble(Null.SetNull(dr["Longitude"], Longitude));
   Altitude = Convert.ToInt32(Null.SetNull(dr["Altitude"], Altitude));
   Region = Convert.ToInt32(Null.SetNull(dr["Region"], Region));
  }
  #endregion

  #region IPropertyAccess
  public override string GetProperty(string strPropertyName, string strFormat, System.Globalization.CultureInfo formatProvider, DotNetNuke.Entities.Users.UserInfo accessingUser, DotNetNuke.Services.Tokens.Scope accessLevel, ref bool propertyNotFound)
  {
   switch (strPropertyName.ToLower()) {
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
    case "points": // Int
     return Points.ToString(strFormat, formatProvider);
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
    default:
       return base.GetProperty(strPropertyName, strFormat, formatProvider, accessingUser, accessLevel, ref propertyNotFound);
   }
  }
  #endregion

 }
}

