using CoreBussiness.BussinessEntity.Roles;
using CoreBussiness.BussinessEntity.Users;
using CoreBussiness.DTO.Users;
using CoreBussiness.ResultPattern;
using CoreBussiness.UnitsOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Segerations.Users.Command;

public class EditUserRoleCommand:IRequest<Result<bool>>
{
    public EditUserRole? EditUserRole { get; set; }
}

public class EditUserRoleCommandHandler : IRequestHandler<EditUserRoleCommand, Result<bool>>
{
    private IUnitOfWork _unitOfWork;
    public EditUserRoleCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<bool>> Handle(EditUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Set<User>().AsTracking()
            .FirstOrDefaultAsync(x => x.Id == request.EditUserRole!.UserId!.Value, cancellationToken);
        if (user == null)
        {
            return Result<bool>.Fail("کاربری پیدا نشد");
        }
        var roleExist = await _unitOfWork.Set<Role>()
            .AnyAsync(x => x.Id == request.EditUserRole!.RoleId!.Value!, cancellationToken);
        if (!roleExist)
        {
            return Result<bool>.Fail("این نقش وجود ندارد");
        }
        user.RoleId = request.EditUserRole!.RoleId!.Value;
        var saveChange = await _unitOfWork.SaveChangesAsync(cancellationToken);
        return saveChange > 0 ? Result<bool>.IsSuccess(true) : Result<bool>.Fail("خطا در پاسخگویی");
    }
}