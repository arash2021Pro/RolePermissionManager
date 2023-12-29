using FluentValidation;

namespace CoreBussiness.DTO.Users;

public class LoginModelValidation:AbstractValidator<LoginModel>
{
    public LoginModelValidation()
    {
        RuleFor(x => x.UserName)
            .NotNull().NotEmpty().WithMessage("نام کاربری الزامی است")
            .Must(x => x is string).WithMessage("نام کاربری باید یک رشته باشد");

        RuleFor(x => x.Password).NotNull().NotEmpty().WithMessage("کلمه عبور الزامیست")
            .Must(x => x is string).WithMessage("پسورد باید رشته ای باشد");
        //    .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$")
        //  .WithMessage("کلمه عبور باید شامل حداقل یک حرف کوچک، یک حرف بزرگ و یک عدد باشد");

    }
}