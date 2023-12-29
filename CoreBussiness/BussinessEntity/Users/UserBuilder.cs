using CoreBussiness.BussinessEntity.Roles;
using CoreBussiness.Enumerations;

namespace CoreBussiness.BussinessEntity.Users;

public class UserBuilder
{
    private User user;

    public UserBuilder()
    {
        user = new User();
    }

    public UserBuilder SetCreatorId(int? creatorId)
    {
        user.CreatorId = creatorId;
        return this;
    }

    public UserBuilder SetCreator(User creator)
    {
        user.Creator = creator;
        return this;
    }

    public UserBuilder SetUsers(ICollection<User> users)
    {
        user.Users = users;
        return this;
    }

    public UserBuilder SetRoleId(int roleId)
    {
        user.RoleId = roleId;
        return this;
    }

    public UserBuilder SetRole(Role role)
    {
        user.Role = role;
        return this;
    }

    public UserBuilder SetPhoneNumber(string phoneNumber)
    {
        user.PhoneNumber = phoneNumber;
        return this;
    }

    public UserBuilder SetUserStatus(UserStatus userStatus)
    {
        user.UserStatus = userStatus;
        return this;
    }

    public UserBuilder SetUserName(string userName)
    {
        user.UserName = userName;
        return this;
    }

    public UserBuilder SetPassword(string password)
    {
        user.Password = password;
        return this;
    }

    public UserBuilder SetToken(string token)
    {
        user.Token = token;
        return this;
    }

    public UserBuilder SetEmail(string email)
    {
        user.Email = email;
        return this;
    }

    public UserBuilder SetIsPin(bool isPin)
    {
        user.IsPin = isPin;
        return this;
    }

    public UserBuilder SetFullName(string fullName)
    {
        user.FullName = fullName;
        return this;
    }

    public UserBuilder SetNationalCode(string nationalCode)
    {
        user.NationalCode = nationalCode;
        return this;
    }

    public UserBuilder SetSerial(string serial)
    {
        user.Serial = serial;
        return this;
    }

    public User Build()
    {
        return user;
    }
}