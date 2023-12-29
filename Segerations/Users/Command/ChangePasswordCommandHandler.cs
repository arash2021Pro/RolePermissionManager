using CoreBussiness.BussinessEntity.Users;
using CoreBussiness.DTO.Users;
using CoreBussiness.ResultPattern;
using CoreBussiness.UnitsOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Segerations.Users.Command;

public class ChangePasswordCommand:IRequest<Result<bool>>
{
    public ChangePasswordModel? PasswordModel { get; set; }
    public int UserId { get; set; }
}

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Result<bool>>
{
    private IUnitOfWork _unitOfWork;
    
    public ChangePasswordCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user =  await _unitOfWork.Set<User>().AsTracking().FirstOrDefaultAsync(x => x.Id == request.UserId);
        if (user == null)
        {
            return Result<bool>.Fail("کاربری پیدا نشد");
        }
        user.Password = request.PasswordModel!.Password!;
        var saveChange = await _unitOfWork.SaveChangesAsync(cancellationToken);
        return saveChange > 0? Result<bool>.IsSuccess(true): Result<bool>.Fail(" خطا در پاسخگویی", 500);
    }
}