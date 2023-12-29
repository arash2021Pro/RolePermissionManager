using CoreBussiness.BussinessEntity.Roles;
using CoreBussiness.Enumerations;
using CoreBussiness.ResultPattern;
using CoreBussiness.UnitsOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Segerations.Roles.Command;

public class EditRoleStatusCommand:IRequest<Result<bool>>
{
    public int RoleId { get; set; }
}

public class EditRoleStatusCommandHandler : IRequestHandler<EditRoleStatusCommand,Result<bool>>
{
    private IUnitOfWork _work;
    public EditRoleStatusCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }
    public async Task<Result<bool>> Handle(EditRoleStatusCommand request, CancellationToken cancellationToken)
    {
        var role = await _work.Set<Role>().AsTracking()
            .FirstOrDefaultAsync(x => x.Id == request.RoleId, cancellationToken);
        if (role == null)
        {
            return Result<bool>.Fail("نقشی پیدا نشد");
        }
        if (role.RoleStatus == RoleStatus.Active)
        {
            role.RoleStatus = RoleStatus.Inactive;
        }else if (role.RoleStatus == RoleStatus.Inactive)
        {
            role.RoleStatus = RoleStatus.Active;
        }
        var saveChange  =await _work.SaveChangesAsync(cancellationToken);
        return saveChange > 0 ? Result<bool>.IsSuccess(true) : Result<bool>.Fail("خطا در پاسخگویی ");
    }
}