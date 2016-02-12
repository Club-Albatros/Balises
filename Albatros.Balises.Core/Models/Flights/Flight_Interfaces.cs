
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
   TotalPoints = Convert.ToInt32(Null.SetNull(dr["TotalPoints"], TotalPoints));
   NrComments = Convert.ToInt32(Null.SetNull(dr["NrComments"], NrComments));
   CreatedByUser = Convert.ToString(Null.SetNull(dr["CreatedByUser"], CreatedByUser));
   ModifiedByUser = Convert.ToString(Null.SetNull(dr["ModifiedByUser"], ModifiedByUser));
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
    case "totalpoints": // Int
     if (TotalPoints == null)
     {
         return "";
     };
     return ((int)TotalPoints).ToString(strFormat, formatProvider);
    case "nrcomments": // Int
     if (NrComments == null)
     {
         return "";
     };
     return ((int)NrComments).ToString(strFormat, formatProvider);
    case "createdbyuser": // NVarChar
     if (CreatedByUser == null)
     {
         return "";
     };
     return PropertyAccess.FormatString(CreatedByUser, strFormat);
    case "modifiedbyuser": // NVarChar
     if (ModifiedByUser == null)
     {
         return "";
     };
     return PropertyAccess.FormatString(ModifiedByUser, strFormat);
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

