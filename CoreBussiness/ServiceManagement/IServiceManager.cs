using CoreBussiness.BussinessEntity.Permissions;
using CoreBussiness.BussinessEntity.Roles;
using CoreBussiness.BussinessEntity.Users;

namespace CoreBussiness.ServiceManagement;

public interface IServiceManager
{
    public IUserService UserService { get; set; }
    public IRoleService RoleService { get; set; }
    public IPermissionService PermissionService { get; set; }
    
}