using System;
using System.Collections;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;

namespace Albatros.DNN.Modules.Balises.Common.Settings
{
    public class ModuleSettings
    {

        #region Properties
        internal ISettingsStore Store;

        public string MySetting
        {
            get { return Store.Get("Default"); }
            set { Store.Set(value); }
        }
        public string Version = typeof(ModuleSettings).Assembly.GetName().Version.ToString();
        #endregion

        #region .ctor
        public ModuleSettings(int moduleId, Hashtable settings)
        {
            Store = new ModuleScopedSettings(moduleId, settings);
        }
        #endregion

        #region Public Members
        public void SaveSettings()
        {
            Store.Save();
        }

        public static ModuleSettings GetSettings(ModuleInfo ctlModule)
        {

            ModuleSettings res = null;
            try
            {
                res = (ModuleSettings)DataCache.GetCache(CacheKey(ctlModule.ModuleID));
            }
            catch (Exception ex)
            {
            }
            if (res == null)
            {
                res = new ModuleSettings(ctlModule.ModuleID, ctlModule.ModuleSettings);
                DataCache.SetCache(CacheKey(ctlModule.ModuleID), res);
            }
            return res;
        }

        public static string CacheKey(int moduleId)
        {
            return string.Format("SettingsModule{0}", moduleId);
        }
        #endregion

    }
}