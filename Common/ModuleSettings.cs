using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Settings;

namespace Albatros.DNN.Modules.Balises.Common
{
    public class ModuleSettings
    {
        [ModuleSetting]
        public string View { get; set; } = "Index";
        [ModuleSetting]
        public int BeaconPassDistanceMeters { get; set; } = 200;

        public static ModuleSettings GetSettings(ModuleInfo module)
        {
            var repo = new ModuleSettingsRepository();
            return repo.GetSettings(module);
        }

        public void SaveSettings(ModuleInfo module)
        {
            var repo = new ModuleSettingsRepository();
            repo.SaveSettings(module, this);
        }
    }
    public class ModuleSettingsRepository : SettingsRepository<ModuleSettings>
    {
    }
}