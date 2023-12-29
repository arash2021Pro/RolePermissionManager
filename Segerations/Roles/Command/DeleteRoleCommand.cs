using CoreBussiness.BussinessEntity.Roles;
using CoreBussiness.ResultPattern;
using CoreBussiness.UnitsOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Segerations.Roles.Command;

public class DeleteRoleCommand:IRequest<Result<bool>>
{
    public int RoleId { get; set; }
}

public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Result<bool>>
{
    private IUnitOfWork _unitOfWork;

    public DeleteRoleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _unitOfWork.Set<Role>().AsTracking().FirstOrDefaultAsync(x => x.Id == request.RoleId);
        if (role == null)
        {
            return Result<bool>.Fail("نقش پیدا نشد");
        }
        _unitOfWork.Set<Role>().Remove(role);
        var saveChange = await _unitOfWork.SaveChangesAsync(cancellationToken);
        return saveChange > 0 ? Result<bool>.IsSuccess(true) : Result<bool>.Fail("خطا در پاسخگویی ");
    }
}