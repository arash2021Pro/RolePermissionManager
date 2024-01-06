using FluentValidation;

namespace CoreBussiness.DTO.Users;

public class UserModelValidation: AbstractValidator<UserModel>
{
    public UserModelValidation()
    {
        RuleFor(x => x.PhoneNumber).NotNull().NotEmpty()
            .Matches("^09\\d{9}$").WithMessage("شماره موبایل معتبر نیست");
        RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("پسورد الزامیست")
            .MinimumLength(6).WithMessage("حداقل شیش کارکتر مجاز هست").MaximumLength(16).WithMessage("شانزده کارکتر مجاز است");
        RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("نام الزامیست");
        RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("نام خانوادگی الزامیست");
    }
}