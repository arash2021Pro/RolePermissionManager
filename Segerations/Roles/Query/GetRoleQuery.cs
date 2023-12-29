using CoreBussiness.BussinessEntity.Roles;
using CoreBussiness.ResultPattern;
using CoreBussiness.UnitsOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Segerations.Roles.Query;

public class GetRoleQuery:IRequest<Result<Role>>
{
    public int RoleId { get; set; }
}

public class GetRoleQueryHandler : IRequestHandler<GetRoleQuery, Result<Role>>
{
    private IUnitOfWork _unitOfWork;
    public GetRoleQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Role>> Handle(GetRoleQuery request, CancellationToken cancellationToken)
    {
        var role = await _unitOfWork.Set<Role>().FirstOrDefaultAsync(x => x.Id == request.RoleId,cancellationToken);
        if (role == null)
        {
            return Result<Role>.Fail("نقشی پیدا نشد");
        }
        return Result<Role>.IsSuccess(role);
    }
}