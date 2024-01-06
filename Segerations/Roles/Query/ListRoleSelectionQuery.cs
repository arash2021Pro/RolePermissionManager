using CoreBussiness.BussinessEntity.Roles;
using CoreBussiness.DTO.Roles;
using CoreBussiness.Globals.Selections;
using CoreBussiness.ResultPattern;
using CoreBussiness.UnitsOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Segerations.Roles.Query;

public class ListRoleSelectionQuery:IRequest<Result<List<EnumSelection>>>
{
    
}

public class ListRoleSelectionQueryHandler : IRequestHandler<ListRoleSelectionQuery, Result<List<EnumSelection>>>
{
    private IUnitOfWork _unitOfWork;

    public ListRoleSelectionQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<Result<List<EnumSelection>>> Handle(ListRoleSelectionQuery request, CancellationToken cancellationToken)
    {
        List<EnumSelection> roles = await _unitOfWork.Set<Role>().Select(x => new EnumSelection()
        {
            Id = x.Id, Name = x.RoleName
        }).ToListAsync(cancellationToken);
        if (roles.Count == 0)
        {
            return Result<List<EnumSelection>>.Fail("نقشی وجود ندارد");
        }
        return Result<List<EnumSelection>>.IsSuccess(roles);
    }
}