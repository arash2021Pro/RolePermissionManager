using FluentValidation;

namespace CoreBussiness.DTO.Users;

public class ChangePasswordModelValidation:AbstractValidator<ChangePasswordModel>
{
    public ChangePasswordModelValidation()
    {
        RuleFor(x => x.Password)
            .NotNull().NotEmpty().WithMessage("کلمه عبور نمیتواند خالی باشد")
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$")
          .WithMessage("کلمه عبور باید شامل حداقل یک حرف کوچک، یک حرف بزرگ و یک عدد باشد");
    }
}