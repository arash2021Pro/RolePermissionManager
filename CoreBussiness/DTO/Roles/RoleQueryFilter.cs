namespace CoreBussiness.DTO.Roles;

public class RoleQueryFilter
{
    public int PageSize { get; set; }
    public int PageNumber { get; set; }
    public string? Name { get; set; }
}