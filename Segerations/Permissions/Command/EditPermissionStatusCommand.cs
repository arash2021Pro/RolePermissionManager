using CoreBussiness.BussinessEntity.Permissions;
using CoreBussiness.DTO.Permissions;
using CoreBussiness.ResultPattern;
using CoreBussiness.UnitsOfWork;
using MapsterMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Segerations.Permissions.Command;

public class EditPermissionStatusCommand:IRequest<Result<bool>>
{
    public PermissionChangeStatus? PermissionChangeStatus { get; set; }
}

public class EditPermissionStatusCommandHandler : IRequestHandler<EditPermissionStatusCommand, Result<bool>>
{
    private IUnitOfWork _unitOfWork;
    private IMapper _mapper; 
    public EditPermissionStatusCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    public async Task<Result<bool>> Handle(EditPermissionStatusCommand request, CancellationToken cancellationToken)
    {
        var permission = await _unitOfWork.Set<Permission>().AsTracking()
            .FirstOrDefaultAsync(x => x.Id == request.PermissionChangeStatus!.PermissionId!, cancellationToken);
        if (permission == null)
        {
            return Result<bool>.Fail("این دسترسی وجود ندارد");
        }
        permission.IsActive = !permission.IsActive;
        var saveChange = await _unitOfWork.SaveChangesAsync(cancellationToken);
        return saveChange > 0 ? Result<bool>.IsSuccess(true) : Result<bool>.Fail("خطا در پاسخگویی");
    }
}