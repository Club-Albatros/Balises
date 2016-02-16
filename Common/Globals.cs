namespace Albatros.DNN.Modules.Balises.Common
{
    public static class Globals
    {

        public const string SharedResourceFileName = "~/DesktopModules/MVC/Albatros/Balises/App_LocalResources/SharedResources.resx";

        public static string ToKmPerHour(this double? speed)
        {
            if (speed == null) { return ""; }
            return ((double)speed * 3.6).ToString("0.0", System.Threading.Thread.CurrentThread.CurrentCulture);
        }

        public static string ToKm(this int meters)
        {
            return (meters / 1000).ToString("0.0");
        }

        public static string ToTime(this int minutes)
        {
            return (new System.TimeSpan(0, minutes, 0)).ToString("%h\\:mm");
        }

    }
}