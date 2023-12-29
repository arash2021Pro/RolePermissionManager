using CoreBussiness.BussinessEntity.Permissions;
using CoreBussiness.BussinessEntity.Users;
using CoreBussiness.Enumerations;

namespace CoreBussiness.BussinessEntity.Roles;

public class RoleBuilder
{
    private Role role;

    public RoleBuilder()
    {
        role = new Role();
    }
    
    public RoleBuilder SetRoleName(string? roleName)
    {
        role.RoleName = roleName;
        return this;
    }

    public RoleBuilder SetRoleStatus(RoleStatus roleStatus)
    {
        role.RoleStatus = roleStatus;
        return this;
    }

    public RoleBuilder SetUsers(ICollection<User> users)
    {
        role.Users = users;
        return this;
    }

    public RoleBuilder SetPermissions(ICollection<Permission> permissions)
    {
        role.Permissions = permissions;
        return this;
    }
    
    public Role Build()
    {
        return role;
    }
    
}