using ContainerNinja.Contracts.DTO;
using FluentValidation;

namespace ContainerNinja.Core.Validators
{
    public class CreateOrUpdateItemDTOValidator : AbstractValidator<CreateOrUpdateItemDTO>
    {
        public CreateOrUpdateItemDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Provide a brief description about the Item");
            RuleFor(x => x.Categories).NotEmpty().WithMessage("Provide atleast a single Category");
            RuleFor(x => x.ColorCode).NotEmpty().WithMessage("Tag a colorCode to the Item");
        }
    }
}
