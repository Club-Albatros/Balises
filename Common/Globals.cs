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

    }
}