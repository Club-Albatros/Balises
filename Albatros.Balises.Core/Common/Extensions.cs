using System;

namespace Albatros.Balises.Core.Common
{
    public static class Extensions
    {
        public static string CutOffUntilCharacter(this string input, string cutoff)
        {
            if (input.IndexOf(cutoff, StringComparison.Ordinal) < 0)
            {
                return input;
            }
            return input.Substring(input.IndexOf(cutoff, StringComparison.Ordinal) + cutoff.Length);
        }

        public static string ResolveUrl(this DotNetNuke.Entities.Portals.PortalAliasInfo portalAlias, string url)
        {
            url = url.TrimStart('~');
            url = url.TrimStart('/');
            var childPortalAlias = portalAlias.GetChildPortalAlias();
            if (childPortalAlias.StartsWith(DotNetNuke.Common.Globals.ApplicationPath))
            {
                return String.Format("{0}/{1}", childPortalAlias, url);
            }
            else
            {
                return String.Format("{0}{1}/{2}", DotNetNuke.Common.Globals.ApplicationPath, childPortalAlias, url);
            }
        }

        public static string GetChildPortalAlias(this DotNetNuke.Entities.Portals.PortalAliasInfo portalAlias)
        {
            var currentAlias = portalAlias.HTTPAlias;
            var index = currentAlias.IndexOf('/');
            if (index > 0)
            {
                return "/" + currentAlias.Substring(index + 1);
            }
            else
            {
                return "";
            }
        }

        public static Point ToPoint(this string coordinates)
        {
            if (string.IsNullOrEmpty(coordinates) || coordinates.IndexOf('/') < 0)
            {
                return null;
            }
            var coords = coordinates.Split('/');
            int iLng;
            int iLat;
            double dLng;
            double dLat;
            if (int.TryParse(coords[0], out iLng) & int.TryParse(coords[1], out iLat))
            {
                double wgsLat = 0;
                double wgsLng = 0;
                double wgsAlt = 0;
                SwissProjection.LV03toWGS84((double)iLng, (double)iLat, 0, ref wgsLat, ref wgsLng, ref wgsAlt);
                return new Point() { Latitude = wgsLat, Longitude = wgsLng };
            }
            else if (double.TryParse(coords[0], out dLng) & double.TryParse(coords[1], out dLat))
            {
                return new Point() { Latitude = dLat, Longitude = dLng };
            }
            return null;
        }

    }
}
