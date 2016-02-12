using System.Linq;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;
using DotNetNuke.Security;
using DotNetNuke.Security.Permissions;

namespace Albatros.DNN.Modules.Balises.Common
{
    public class ContextSecurity
    {
        public bool CanView { get; set; }
        public bool CanEdit { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsPilot { get; set; }
        public bool IsVerifier { get; set; }
        private UserInfo user { get; set; }

        public int UserId
        {
            get
            {
                return user.UserID;
            }
        }

        public ContextSecurity(ModuleInfo objModule)
        {
            user = UserController.Instance.GetCurrentUserInfo();
            if (user.IsSuperUser)
            {
                CanView = CanEdit = IsAdmin = IsPilot = IsVerifier = true;
            }
            else
            {
                IsAdmin = PortalSecurity.IsInRole(PortalSettings.Current.AdministratorRoleName);
                if (IsAdmin)
                {
                    CanView = CanEdit = IsPilot = IsVerifier = true;
                }
                else
                {
                    CanView = ModulePermissionController.CanViewModule(objModule);
                    CanEdit = ModulePermissionController.HasModulePermission(objModule.ModulePermissions, "EDIT");
                    IsPilot = ModulePermissionController.HasModulePermission(objModule.ModulePermissions, "PILOT");
                    IsVerifier = ModulePermissionController.HasModulePermission(objModule.ModulePermissions, "VERIFIER");
                }
            }
        }

    }
}