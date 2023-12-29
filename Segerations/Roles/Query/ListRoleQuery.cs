using CoreBussiness.BussinessEntity.Roles;
using CoreBussiness.DTO.Roles;
using CoreBussiness.PaginationsService;
using CoreBussiness.ResultPattern;
using CoreBussiness.UnitsOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Segerations.Roles.Query;

public class ListRoleQuery:IRequest<Result<PaginationResult<RoleResponse>>>
{
    public RoleQueryFilter? RoleQueryFilter { get; set; }
}

public class ListRoleQueryHandler : IRequestHandler<ListRoleQuery, Result<PaginationResult<RoleResponse>>>
{
    private IUnitOfWork _unitOfWork;
    public ListRoleQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<PaginationResult<RoleResponse>>> Handle(ListRoleQuery request, CancellationToken cancellationToken)
    {
        var roles = _unitOfWork.Set<Role>().AsQueryable();
        var totalCount = await roles.CountAsync();
        var totalPages = (int)Math.Ceiling(totalCount / (double)request.RoleQueryFilter!.PageSize);
        if (!String.IsNullOrEmpty(request.RoleQueryFilter.Name))
        {
            roles = roles.Where(entity => entity.RoleName!.Contains(request.RoleQueryFilter.Name));
        }
        var entities = await roles
            .Skip((request.RoleQueryFilter.PageNumber - 1) * request.RoleQueryFilter.PageSize)
            .Take(request.RoleQueryFilter.PageSize)
            .ToListAsync(cancellationToken);

        var hasPreviousPage = request.RoleQueryFilter.PageNumber > 1;
        var hasNextPage = request.RoleQueryFilter.PageNumber < totalPages;

        var response = entities.Select(x => new RoleResponse()
        {
            id = x.Id,
            RoleName = x.RoleName,
            CreationDate = x.CreationTimeOffset!.Value,
            RoleStatus = x.RoleStatus
        });
        
        var result = new PaginationResult<RoleResponse>
        {
            HasPreviousPage = hasPreviousPage,
            HasNextPage = hasNextPage,
            TotalCount = totalCount,
            Items = response,
            PageSize = request.RoleQueryFilter.PageSize,
            PageNumber = request.RoleQueryFilter.PageNumber
        };
        return result.Items == null ? Result<PaginationResult<RoleResponse>>.Fail("موردی یافت نشد"):Result<PaginationResult<RoleResponse>>.IsSuccess(result);
    }
}