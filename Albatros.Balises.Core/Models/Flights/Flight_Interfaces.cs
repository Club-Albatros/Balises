
using System;
using System.Data;
using System.Xml.Serialization;

using DotNetNuke.Common.Utilities;
using DotNetNuke.Services.Tokens;

namespace Albatros.Balises.Core.Models.Flights
{

 [Serializable(), XmlRoot("Flight")]
 public partial class Flight
 {

  #region IHydratable
  public override void Fill(IDataReader dr)
  {
   base.Fill(dr);
   NrBeacons = Convert.ToInt32(Null.SetNull(dr["NrBeacons"], NrBeacons));
   NrComments = Convert.ToInt32(Null.SetNull(dr["NrComments"], NrComments));
   Pilot = Convert.ToString(Null.SetNull(dr["Pilot"], Pilot));
   CreatedByUser = Convert.ToString(Null.SetNull(dr["CreatedByUser"], CreatedByUser));
   LastModifiedByUser = Convert.ToString(Null.SetNull(dr["LastModifiedByUser"], LastModifiedByUser));
   ValidatedByUser = Convert.ToString(Null.SetNull(dr["ValidatedByUser"], ValidatedByUser));
  }
  #endregion

  #region IPropertyAccess
  public override string GetProperty(string strPropertyName, string strFormat, System.Globalization.CultureInfo formatProvider, DotNetNuke.Entities.Users.UserInfo accessingUser, DotNetNuke.Services.Tokens.Scope accessLevel, ref bool propertyNotFound)
  {
   switch (strPropertyName.ToLower()) {
    case "nrbeacons": // Int
     if (NrBeacons == null)
     {
         return "";
     };
     return ((int)NrBeacons).ToString(strFormat, formatProvider);
    case "nrcomments": // Int
     if (NrComments == null)
     {
         return "";
     };
     return ((int)NrComments).ToString(strFormat, formatProvider);
    case "pilot": // NVarChar
     if (Pilot == null)
     {
         return "";
     };
     return PropertyAccess.FormatString(Pilot, strFormat);
    case "createdbyuser": // NVarChar
     if (CreatedByUser == null)
     {
         return "";
     };
     return PropertyAccess.FormatString(CreatedByUser, strFormat);
    case "lastmodifiedbyuser": // NVarChar
     if (LastModifiedByUser == null)
     {
         return "";
     };
     return PropertyAccess.FormatString(LastModifiedByUser, strFormat);
    case "validatedbyuser": // NVarChar
     if (ValidatedByUser == null)
     {
         return "";
     };
     return PropertyAccess.FormatString(ValidatedByUser, strFormat);
    default:
       return base.GetProperty(strPropertyName, strFormat, formatProvider, accessingUser, accessLevel, ref propertyNotFound);
   }
  }
  #endregion

 }
}

