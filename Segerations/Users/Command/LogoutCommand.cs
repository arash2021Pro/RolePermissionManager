using CoreBussiness.BussinessEntity.Users;
using CoreBussiness.ResultPattern;
using CoreBussiness.UnitsOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Segerations.Users.Command;

public class LogoutCommand:IRequest<Result<bool>>
{
    public int UserId { get; set; }
}

public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Result<bool>>
{
    private IUnitOfWork _unitOfWork;
    public LogoutCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.Set<User>().AsTracking().FirstOrDefaultAsync(x => x.Id == request.UserId, cancellationToken);
        if(user == null) return Result<bool>.Fail("کاربری پیدا نشد");
        user.Token = null;
        var saveChange = await _unitOfWork.SaveChangesAsync(cancellationToken);
        return saveChange > 0 ? Result<bool>.IsSuccess(true) : Result<bool>.Fail("خطا در پاسخگویی",500);

    }
}