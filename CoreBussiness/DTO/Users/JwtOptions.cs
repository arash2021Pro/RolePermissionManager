namespace CoreBussiness.DTO.Users;

public class JwtOptions
{ 
    public string? SecretKey { get; set; }
    public string? Issuer { get; set; }
    public int ValidTokenTime { get; set; }
    public string?Audience { get; set; }
}