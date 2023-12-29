using CoreBussiness.BaseEntity;
using CoreBussiness.BussinessEntity.Permissions;
using CoreBussiness.BussinessEntity.Users;
using CoreBussiness.Enumerations;


namespace CoreBussiness.BussinessEntity.Roles;

public class Role:Core
{
    public string?RoleName { get; set; }
    public virtual ICollection<Permission>Permissions { get; set; }
    public virtual ICollection<User>Users { get; set; }
    public RoleStatus RoleStatus { get; set; }
}