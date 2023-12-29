using FluentValidation;

namespace CoreBussiness.DTO.Roles;

public class RoleModelValidation: AbstractValidator<RoleModel>
{
    public RoleModelValidation()
    {
        RuleFor(x => x.RoleName).NotNull().NotEmpty().WithMessage("انتخاب نقش الزامیست");
    }
}