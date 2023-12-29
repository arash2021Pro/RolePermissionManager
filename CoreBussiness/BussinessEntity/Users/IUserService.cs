using CoreBussiness.ResultPattern;

namespace CoreBussiness.BussinessEntity.Users;

public interface IUserService
{
    public Task<Result<User>> GetUserAsync(int userId,CancellationToken cancellationToken);
    public Task<Result<bool>> ChangePasswordAsync(int userId,string password,CancellationToken cancellationToken);
    public Task<Result<string>> GetUserTokenAsync(int userId);
    public Task<Result<bool>> GetUserPermissionAsync(int userId,string? controllerName,string? actionName);
}