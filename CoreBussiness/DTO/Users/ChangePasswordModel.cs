using FluentValidation;

namespace CoreBussiness.DTO.Users;

public class ChangePasswordModel
{
    public string?Password { get; set; }
}