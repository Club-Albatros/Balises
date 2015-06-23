using System.Collections;
using DotNetNuke.Entities.Modules;

namespace Albatros.DNN.Modules.Balises.Common.Settings
{
    public class ModuleScopedSettings : StringBasedSettings
    {
        public ModuleScopedSettings(int moduleId, Hashtable moduleSettings)
            : base(
                name => moduleSettings[name] as string,
                (name, value) => new ModuleController().UpdateModuleSetting(moduleId, name, value)
                ) { }
    }
}