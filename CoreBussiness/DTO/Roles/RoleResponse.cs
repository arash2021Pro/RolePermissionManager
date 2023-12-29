using CoreBussiness.Enumerations;

namespace CoreBussiness.DTO.Roles;

public class RoleResponse
{
    public int id { get; set; }
    public string?RoleName { get; set; }
    public RoleStatus RoleStatus { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    
}