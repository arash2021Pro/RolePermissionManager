using CoreBussiness.BussinessEntity.Roles;
using CoreBussiness.DTO.Roles;
using CoreBussiness.ResultPattern;
using CoreBussiness.UnitsOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Segerations.Roles.Command;

public class EditRoleCommand:IRequest<Result<bool>>
{
    public RoleModel? RoleModel { get; set; }
 
}

public class EditRoleCommandHandler : IRequestHandler<EditRoleCommand, Result<bool>>
{
    private IUnitOfWork _unitOfWork;
    public EditRoleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<bool>> Handle(EditRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _unitOfWork.Set<Role>().AsTracking().FirstOrDefaultAsync(x => x.Id == request.RoleModel!.RoleId!.Value, cancellationToken: cancellationToken);
        if (role == null)
        {
            return Result<bool>.Fail("نقشی پیدا نشد");
        }

        var roleBuilder = new RoleBuilder().SetRoleName(request.RoleModel!.RoleName)
            .Build();
        
        var saveChange = await _unitOfWork.SaveChangesAsync(cancellationToken);
        return saveChange > 0 ? Result<bool>.IsSuccess(true) : Result<bool>.Fail("خطا در پاسخگویی ");
    }
}