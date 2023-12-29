using CoreBussiness.BussinessEntity.Permissions;
using CoreBussiness.BussinessEntity.Roles;
using CoreBussiness.BussinessEntity.Users;

namespace CoreBussiness.ServiceManagement;

public class ServiceManager:IServiceManager
{
    public ServiceManager(IUserService userService, IRoleService roleService, IPermissionService permissionService)
    {
        UserService = userService;
        RoleService = roleService;
        PermissionService = permissionService;
    }
    public IUserService UserService { get; set; }
    public IRoleService RoleService { get; set; }
    public IPermissionService PermissionService { get; set; }
}