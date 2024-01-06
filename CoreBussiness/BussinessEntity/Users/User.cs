using CoreBussiness.BaseEntity;
using CoreBussiness.BussinessEntity.Roles;
using CoreBussiness.Enumerations;

namespace CoreBussiness.BussinessEntity.Users;

public class User:Core
{
    public User()
    {
        Serial = GenerateSerial();
    }
    public int ?CreatorId { get; set; }
    public User? Creator { get; set; } 
    public ICollection<User> Users { get; set; }
    public int RoleId { get; set; }
    public Role Role { get; set; }
    private string GenerateSerial() => Guid.NewGuid().ToString().Replace("-", "").Substring(1, 6);
    public string? PhoneNumber { get; set; }
    public UserStatus UserStatus { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string?Token { get; set; }
    
    public string? Email { get; set; }
    public bool IsPin { get; set; }
    public string FullName { get; set; } 
    public string GetFullName(string? name, string? lastName) => FullName = name + " " + lastName;
    public string?NationalCode { get; set; }
    public string? Serial { get; set;}
}