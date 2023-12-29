namespace CoreBussiness.DTO.Permissions;

public class RolePermissions
{
    public int RoleId { get; set; }
    public List<ControllerActions>Permissions { get; set; }
}