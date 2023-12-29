using System.Reflection;
using CoreBussiness.BaseEntity;
using CoreBussiness.BussinessEntity.Roles;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace CoreBussiness.BussinessEntity.Permissions;

public class Permission:Core
{
    public string?PermissionName { get; set; }
    public string?ControllerName { get; set; }
    public string?ActionName { get; set; }
    public bool IsActive { get; set; } = true;
    public int RoleId { get; set; }
    public Role Role { get; set; }

  
}