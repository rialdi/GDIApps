using ServiceStack;
using ServiceStack.DataAnnotations;
using System.Collections;
using ServiceStack.OrmLite;
using GDIApps.ServiceModel.Types;

namespace GDIApps.Migrations;

public class Migration1001 : MigrationBase
{
    public override void Up()
    {
        Db.CreateTable<AppRole>();
        Db.CreateTable<AppMenu>();
        // Db.CreateTable<AppMenuRole>();

        int roleIdAdmin = (int) CreateAppRole("Admin", "Super User Administrator");
        CreateAppRole("Guest", "Guest");
        CreateAppRole("Employee", "Employee");
        CreateAppRole("Developer", "Developer");
        CreateAppRole("Team Leader", "Team Leader");
        CreateAppRole("Manager", "Manager");
        CreateAppRole("Technical Writer", "Technical Writer");
        CreateAppRole("Administrative", "Administrative");

        // int parentAppMenuId = 0;
        int appMenuIdRoot = (int) CreateAppMenu(null, null, 1, "ROOT", "ROOT", null, true);

        var adminMenuList = GetMenuListAdmin(roleIdAdmin);
        CrateAppMenuList(adminMenuList, roleIdAdmin, appMenuIdRoot);
        // Db.Insert(adminMenuList);

        // int appMenuId = (int) CreateAppMenu(roleIdAdmin, appMenuIdRoot, 1, "Dashboards", "backend-dashboard", "si si-speedometer", true);
        // int appMenuIdAdminPage = (int) CreateAppMenu(roleIdAdmin, appMenuIdRoot, 2, "Admin Pages", null, "si si-grid", true);

        // parentAppMenuId = appMenuId;
        // CreateAppMenu(roleIdAdmin, appMenuIdAdminPage, 1, "User List", "backend-admins-userlist", null, true);
        // CreateAppMenu(roleIdAdmin, appMenuIdAdminPage, 2, "Lookups", "backend-admins-lookup", null, true);
        
        // int appMenuIdClient = (int) CreateAppMenu(roleIdAdmin, appMenuIdAdminPage, 3, "Clients", "backend-admins-client", null, true);
        // CreateAppMenu(roleIdAdmin, appMenuIdClient, 3, "Clients", "backend-admins-client", null, true);
        // CreateAppMenu(roleIdAdmin, appMenuIdClient, 4, "Client Address", "backend-admins-caddress", null, true);
        // CreateAppMenu(roleIdAdmin, appMenuIdClient, 5, "Client Banks", "backend-admins-cbank", null, true);
        // CreateAppMenu(roleIdAdmin, appMenuIdClient, 6, "Client Contracts", "backend-admins-ccontract", null, true);

        // CreateAppMenu(roleIdAdmin,appMenuIdAdminPage, 7, "Projects", "backend-admins-project", null, true);
        // CreateAppMenu(roleIdAdmin,appMenuIdAdminPage, 8, "Invoices", "backend-admins-invoices", null, true);
    }

    public override void Down()
    {
        Db.DropTable<AppMenu>();
        Db.DropTable<AppRole>();
    }

    private void CrateAppMenuList(List<AppMenu> menuList, int? appRoleId, int parentAppMenuId)
    {
        foreach(var parentAppMenu in menuList)
        {
            var lastAppMenuId = (int) CreateAppMenu(
                appRoleId, 
                parentAppMenuId, 
                parentAppMenu.Sequence, 
                parentAppMenu.Name, 
                parentAppMenu.To, 
                parentAppMenu.Icon, 
                parentAppMenu.IsActive
            );

            if(parentAppMenu.Sub != null)
            {
                foreach(var subMenu in parentAppMenu.Sub)
                {
                    var parentSubMenuId = (int) CreateAppMenu(
                        appRoleId, 
                        lastAppMenuId, 
                        subMenu.Sequence, 
                        subMenu.Name, 
                        subMenu.To, 
                        subMenu.Icon, 
                        subMenu.IsActive
                    );
                    if(subMenu.Sub != null)
                    {
                        CrateAppMenuList(subMenu.Sub, appRoleId, parentSubMenuId);
                    }
                }
            }
        }

    }

    private List<AppMenu> GetMenuListAdmin(int adminRoleId)
    {
        List<AppMenu> adminMenu = new List<AppMenu>{
            new AppMenu {
                AppRoleId = adminRoleId,
                AppMenuId = null,
                Sequence = 1,
                Name = "Dashboards",
                To = "backend-dashboard",
                Icon = "si si-speedometer",
                IsActive = true
            },
            new AppMenu {
                AppRoleId = adminRoleId,
                AppMenuId = null,
                Sequence = 2,
                Name = "Admin Pages",
                To = null,
                Icon = "si si-grid",
                IsActive = true,
                Sub = new List<AppMenu>{
                    new AppMenu {
                        Sequence = 1,
                        Name = "User List",
                        To = "backend-admins-client",
                        Icon = "si si-speedometer",
                        IsActive = true
                    },
                    new AppMenu {
                        Sequence = 2,
                        Name = "Lookups",
                        To = "backend-admins-lookup",
                        Icon = "si si-speedometer",
                        IsActive = true
                    },
                    new AppMenu {
                        Sequence = 3,
                        Name = "Clients",
                        To = null,
                        Icon = "si si-speedometer",
                        IsActive = true,
                        Sub = new List<AppMenu>{
                            new AppMenu {
                                Sequence = 1,
                                Name = "Client List",
                                To = "backend-admins-client",
                                Icon = "si si-speedometer",
                                IsActive = true
                            },
                            new AppMenu {
                                Sequence = 2,
                                Name = "Address",
                                To = "backend-admins-caddress",
                                Icon = "si si-speedometer",
                                IsActive = true
                            },
                            new AppMenu {
                                Sequence = 3,
                                Name = "Banks",
                                To = "backend-admins-cbank",
                                Icon = "si si-speedometer",
                                IsActive = true
                            },
                            new AppMenu {
                                Sequence = 4,
                                Name = "Contracts",
                                To = "backend-admins-ccontract",
                                Icon = "si si-speedometer",
                                IsActive = true
                            },
                        }
                    }
                }
            },

        };
        return adminMenu;
    }

    private long CreateAppRole(string roleName, string description) =>
        Db.Insert(new AppRole {
            RoleName = roleName,
            Description = description
        });

    // private int GetAppMenuId(APP_MENU_TYPE appMenuType, int appMenuSequence)
    // {
    //     int appMenuId = 0;

    //     var appMenuItem = Db.Select<AppMenu>(w => w.MenuType == appMenuType && w.Sequence == appMenuSequence).FirstOrDefault();
    //     if(appMenuItem != null)
    //     {
    //         appMenuId = appMenuItem.Id;

    //     }
    //     return appMenuId;
    // }
    private long CreateAppMenu(
        int? appRoleId, int? parentAppMenuId, int sequence, string name, string? to, string? icon, bool isActive
    ) =>
         Db.Insert(new AppMenu {
            AppRoleId = appRoleId,
            AppMenuId = parentAppMenuId,
            Sequence = sequence,
            Name = name,
            To = to,
            Icon = icon,
            IsActive = isActive
        }, selectIdentity:true);

    // private long CreateAppMenuRole(int appMenuId, string role) =>
    //     Db.Insert(new AppMenuRole {
    //         AppMenuId = appMenuId,
    //         Role = role
    //     });


    // private void CreateAppSubMenu(int appMenuId, int sequence, string name, string? to, string? icon, string? roles, bool isActive
    // ) =>
    //     Db.Insert(new AppSubMenu {
    //         AppMenuId = appMenuId,
    //         Sequence = sequence,
    //         Name = name,
    //         To = to,
    //         Icon = icon,
    //         Roles = roles,
    //         IsActive = isActive,
    //         CreatedBy="rialdi@ptgdi.com",
    //         CreatedDate= DateTime.Now,
    //         ModifiedBy="rialdi@ptgdi.com",
    //         ModifiedDate = DateTime.Now
    //     });

    
}