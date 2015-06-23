using System;
using Albatros.DNN.Modules.Balises.Common;

namespace Albatros.DNN.Modules.Balises
{
    public class ModuleHome : ModuleBase
    {
        public string View { get; set; }

        protected override string RazorScriptFile
        {
            get
            {
                return string.Format("~/DesktopModules/Albatros/Balises/Views/{0}.cshtml", View);
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            View = "Home";
            AddService();
            LocalResourceFile = Globals.SharedResourceFileName;
        }

    }
}