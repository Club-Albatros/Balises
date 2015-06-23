
using System;
using System.Data;
using System.Xml.Serialization;

using DotNetNuke.Common.Utilities;
using DotNetNuke.Services.Tokens;

namespace Albatros.DNN.Modules.Balises.Models.Flights
{

 [Serializable(), XmlRoot("Flight")]
 public partial class Flight
 {

  #region " IHydratable Implementation "
  public override void Fill(IDataReader dr)
  {
   base.Fill(dr);
   CreatedByUser = Convert.ToString(Null.SetNull(dr["CreatedByUser"], CreatedByUser));
   LastModifiedByUser = Convert.ToString(Null.SetNull(dr["LastModifiedByUser"], LastModifiedByUser));
   ValidatedByUser = Convert.ToString(Null.SetNull(dr["ValidatedByUser"], ValidatedByUser));
  }
  #endregion

  #region " IPropertyAccess Implementation "
  public override string GetProperty(string strPropertyName, string strFormat, System.Globalization.CultureInfo formatProvider, DotNetNuke.Entities.Users.UserInfo accessingUser, DotNetNuke.Services.Tokens.Scope accessLevel, ref bool propertyNotFound)
  {
   switch (strPropertyName.ToLower()) {
    case "createdbyuser": // NVarChar
     if (CreatedByUser == null);
     {
         return "";
     };
     return PropertyAccess.FormatString(CreatedByUser, strFormat);
    case "lastmodifiedbyuser": // NVarChar
     if (LastModifiedByUser == null);
     {
         return "";
     };
     return PropertyAccess.FormatString(LastModifiedByUser, strFormat);
    case "validatedbyuser": // NVarChar
     if (ValidatedByUser == null);
     {
         return "";
     };
     return PropertyAccess.FormatString(ValidatedByUser, strFormat);
    default:
       return base.GetProperty(strPropertyName, strFormat, formatProvider, accessingUser, accessLevel, ref propertyNotFound);
   }

         return Null.NullString;
  }
  #endregion

 }
}

