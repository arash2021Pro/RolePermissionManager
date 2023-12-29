using System.Reflection;
using CoreBussiness.BussinessEntity.Permissions;
using CoreBussiness.BussinessEntity.Roles;
using CoreBussiness.BussinessEntity.Users;
using CoreBussiness.Enumerations;
using CoreStorage.AppContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace InstaManager.ModuleApplication.SeedApplication;

public class DatabaseInitialService:IDatabaseInitializer
{
    private ApplicationContext _applicationContext;
    private IServiceProvider _serviceProvider;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="applicationContext"></param>
    /// <param name="serviceProvider"></param>
    public DatabaseInitialService(ApplicationContext applicationContext, IServiceProvider serviceProvider)
    {
        _applicationContext = applicationContext;
        _serviceProvider = serviceProvider;
        applicationContext.Database.Migrate();
    }
    public void SeedData()
    {
        
        if (!_applicationContext.Users.Any())
        {
            var role = new Role()
            {
                RoleName = "SuperAdmin",
                RoleStatus = RoleStatus.Active
            };
            _applicationContext.Roles.Add(role);
            var permission = new Permission() { PermissionName = "FullAccess",ActionName = "AddUser",ControllerName = "User",Role = role};
            _applicationContext.Permissions.AddRange(permission);

                var user = new User()
                {
                    Creator = null,
                    Password = "arash1380",
                    UserName = "Arash",
                    CreatorId = null,
                    IsPin = true,
                    PhoneNumber = "09130242717",
                    Role = role,
                    UserStatus = UserStatus.Active,
                    Email = null,
                    FullName = "ArashMadadi",
                    NationalCode = null
                };
                _applicationContext.Users.Add(user);
                _applicationContext.SaveChanges();
                
        }
            
    }
    
}
