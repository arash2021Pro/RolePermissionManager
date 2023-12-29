using CoreBussiness.BussinessEntity.Roles;
using CoreBussiness.DTO.Roles;
using CoreBussiness.Enumerations;
using CoreBussiness.ResultPattern;
using CoreBussiness.UnitsOfWork;
using MediatR;

namespace Segerations.Roles.Command;

public class CreateRoleCommand:IRequest<Result<bool>>
{
    public RoleModel RoleModel { get; set; }
}

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand,Result<bool>>
{
    private IUnitOfWork _work;
    public CreateRoleCommandHandler(IUnitOfWork work)
    {
        _work = work;
    }
    
    public async Task<Result<bool>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = new RoleBuilder().SetRoleName(request.RoleModel.RoleName).SetRoleStatus(RoleStatus.Active).Build();
        await _work.Set<Role>().AddAsync(role);
        var saveChange = await _work.SaveChangesAsync(cancellationToken);
        return saveChange > 0 ? Result<bool>.IsSuccess(true) : Result<bool>.Fail("خطا در پاسخگویی");
    }
}