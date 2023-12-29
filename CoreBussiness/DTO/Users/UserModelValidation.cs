using FluentValidation;

namespace CoreBussiness.DTO.Users;

public class UserModelValidation: AbstractValidator<UserModel>
{
    public UserModelValidation()
    {
        RuleFor(x => x.RoleId).NotNull().WithMessage("انتخاب نقش الزامیست");
        RuleFor(x => x.PhoneNumber).NotNull().NotEmpty().WithMessage("شماره موبایل الزامیست");
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("نام الزامیست");
        RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("نام خانوادگی الزامیست");
    }
}