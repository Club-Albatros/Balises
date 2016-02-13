
using System;
using System.Data;
using System.Xml.Serialization;

using DotNetNuke.Common.Utilities;
using DotNetNuke.Services.Tokens;

namespace Albatros.Balises.Core.Models.Comments
{

 [Serializable(), XmlRoot("Comment")]
 public partial class Comment
 {

  #region IHydratable
  public override void Fill(IDataReader dr)
  {
   base.Fill(dr);
   DisplayName = Convert.ToString(Null.SetNull(dr["DisplayName"], DisplayName));
   TakeoffTime = (DateTime)(Null.SetNull(dr["TakeoffTime"], TakeoffTime));
   TakeoffDescription = Convert.ToString(Null.SetNull(dr["TakeoffDescription"], TakeoffDescription));
  }
  #endregion

  #region IPropertyAccess
  public override string GetProperty(string strPropertyName, string strFormat, System.Globalization.CultureInfo formatProvider, DotNetNuke.Entities.Users.UserInfo accessingUser, DotNetNuke.Services.Tokens.Scope accessLevel, ref bool propertyNotFound)
  {
   switch (strPropertyName.ToLower()) {
    case "displayname": // NVarChar
     if (DisplayName == null)
     {
         return "";
     };
     return PropertyAccess.FormatString(DisplayName, strFormat);
    case "takeofftime": // DateTime
     return TakeoffTime.ToString(strFormat, formatProvider);
    case "takeoffdescription": // NVarChar
     return PropertyAccess.FormatString(TakeoffDescription, strFormat);
    default:
       return base.GetProperty(strPropertyName, strFormat, formatProvider, accessingUser, accessLevel, ref propertyNotFound);
   }
  }
  #endregion

 }
}

