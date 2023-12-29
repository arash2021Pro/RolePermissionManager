using CoreBussiness.BussinessEntity.Permissions;
using CoreBussiness.BussinessEntity.Roles;
using CoreBussiness.DTO.Permissions;
using CoreBussiness.ResultPattern;
using CoreBussiness.UnitsOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Segerations.Permissions.Command;

public class SetRolePermissionCommand:IRequest<Result<bool>>
{
    public RolePermissions RolePermissions { get; set; }
}

public class SetRolePermissionCommandHandler : IRequestHandler<SetRolePermissionCommand, Result<bool>>
{
    private IUnitOfWork _unitOfWork;
    public SetRolePermissionCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<bool>> Handle(SetRolePermissionCommand request, CancellationToken cancellationToken)
    {
        var roleExist = await _unitOfWork.Set<Role>().AnyAsync(x => x.Id == request.RolePermissions.RoleId,cancellationToken);
        if (!roleExist)
        {
            return Result<bool>.Fail("نقشی پیدا نشد");
        }
        if (request.RolePermissions.Permissions.Count == 0)
        {
            return Result<bool>.Fail("هیچ دسترسی برای این نقش انتخاب نشده");
        }
        var permissions = new List<Permission>();
        foreach (var item in request.RolePermissions.Permissions)
        {
           permissions.Add(new Permission(){RoleId = request.RolePermissions.RoleId,ControllerName = item.ControllerName,ActionName = item.ActionName});
        }
        await _unitOfWork.Set<Permission>().AddRangeAsync(permissions);
        var saveChange = await _unitOfWork.SaveChangesAsync(cancellationToken);
        return saveChange > 0 ? Result<bool>.IsSuccess(true) : Result<bool>.Fail("خطا پاسخگویی");
    }
}