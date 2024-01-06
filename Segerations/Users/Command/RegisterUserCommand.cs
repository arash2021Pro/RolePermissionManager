using CoreBussiness.BussinessEntity.Users;
using CoreBussiness.DTO.Users;
using CoreBussiness.ResultPattern;
using CoreBussiness.UnitsOfWork;
using MediatR;

namespace Segerations.Users.Command;

public class RegisterUserCommand:IRequest<Result<bool>>
{
    public UserModel UserModel { get; set; }   
    public int CreatorId { get; set; }
}

public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Result<bool>>
{
    private IUnitOfWork _unitOfWork;

    public RegisterUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<bool>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (!request.UserModel.RoleId.HasValue)
        {
            return Result<bool>.Fail("لطفا نقش انتخاب کنید");
        }
        var user = new UserBuilder()
            .SetFullName(request.UserModel.Name!, request.UserModel.LastName!)
            .SetPhoneNumber(request.UserModel.PhoneNumber!)
            .SetRoleId(request.UserModel.RoleId.Value)
            .SetNationalCode(request.UserModel.NationalCode!)
            .SetCreatorId(request.CreatorId)
            .SetPassword(request.UserModel.Password!)
            .SetUserName(request.UserModel.UserName!)
            .Build();
        var saveChange = await _unitOfWork.ExecuteAddAsync(user, cancellationToken: cancellationToken);
        return saveChange > 0 ? Result<bool>.IsSuccess(true) : Result<bool>.Fail("خطا در پاسخگویی");
        
    }
}