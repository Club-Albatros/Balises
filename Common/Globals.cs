
using System;

namespace Albatros.DNN.Modules.Balises.Common
{
    public static class Globals
    {

        public const string SharedResourceFileName = "~/DesktopModules/Albatros/Balises/App_LocalResources/SharedResources.resx";

        public static string CutOffUntilCharacter(this string input, string cutoff)
        {
            if (input.IndexOf(cutoff, StringComparison.Ordinal) < 0)
            {
                return input;
            }
            return input.Substring(input.IndexOf(cutoff, StringComparison.Ordinal) + cutoff.Length);
        }
    }
}