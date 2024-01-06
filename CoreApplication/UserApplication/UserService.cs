using CoreBussiness.BussinessEntity.Permissions;
using CoreBussiness.BussinessEntity.Roles;
using CoreBussiness.BussinessEntity.Users;
using CoreBussiness.ResultPattern;
using CoreBussiness.UnitsOfWork;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CoreApplication.UserApplication;

public class UserService:IUserService
{
    private IUnitOfWork _unitOfWork;
    private IConfiguration _configuration;
    public UserService(IUnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }
    public async Task<Result<User>> GetUserAsync(int userId,CancellationToken cancellationToken)
    {
        var user =  await _unitOfWork.Set<User>().AsTracking().FirstOrDefaultAsync(x => x.Id == userId);
        return user == null ? Result<User>.Fail("کاربری یافت نشد") : Result<User>.IsSuccess(user);
    }
    public async Task<Result<bool>> ChangePasswordAsync(int userId, string password,CancellationToken cancellationToken)
    {
        var userResult = await GetUserAsync(userId,cancellationToken);
        userResult.Data.Password = password;
        var saveChange = await _unitOfWork.SaveChangesAsync(cancellationToken);
        return saveChange > 0? Result<bool>.IsSuccess(true): Result<bool>.Fail(" خطا در پاسخگویی", 500);
    }
    public async Task<Result<string>> GetUserTokenAsync(int userId)
    {
        var user= await _unitOfWork.Set<User>().AsTracking().FirstOrDefaultAsync(x => x.Id == userId);
        return user == null ? Result<string>.Fail("کاربری یافت نشد") : Result<string>.IsSuccess(user.Token!);
    }
    public async Task<Result<bool>> GetUserPermissionAsync(int userId,string? controllerName,string? actionName)
    {
        var superAccess = _configuration.GetSection("AccessSettings:SuperAccess").Value;
        var user = await _unitOfWork.Set<User>().FirstOrDefaultAsync(x => x.Id == userId);
        var isPermitted = await _unitOfWork.Set<Permission>().AnyAsync(x =>
            x.RoleId == user!.RoleId && x.ControllerName == controllerName && x.ActionName == actionName &&
            x.IsActive == true || x.PermissionName == superAccess);
        return isPermitted ? Result<bool>.IsSuccess(true) : Result<bool>.Fail("عدم دسترسی");
    }
    
}