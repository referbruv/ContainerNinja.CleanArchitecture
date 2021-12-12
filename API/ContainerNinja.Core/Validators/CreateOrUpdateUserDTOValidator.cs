using ContainerNinja.Contracts.DTO;
using FluentValidation;

namespace ContainerNinja.Core.Validators
{
    public class CreateOrUpdateUserDTOValidator : AbstractValidator<CreateOrUpdateUserDTO>
    {
        public CreateOrUpdateUserDTOValidator()
        {
            RuleFor(x => x.EmailAddress).NotEmpty().WithMessage("EmailAddress is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
            RuleFor(x => x.EmailAddress).EmailAddress().WithMessage("Not a valid EmailAddress");
            RuleFor(x => x.Role).NotEmpty().WithMessage("User Role is required");
        }
    }
}
