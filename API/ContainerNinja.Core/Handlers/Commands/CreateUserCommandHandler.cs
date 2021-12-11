using ContainerNinja.Contracts.Data;
using ContainerNinja.Contracts.Data.Entities;
using ContainerNinja.Contracts.DTO;
using ContainerNinja.Contracts.Services;
using ContainerNinja.Core.Exceptions;
using FluentValidation;
using MediatR;

namespace ContainerNinja.Core.Handlers.Commands
{
    public class CreateUserCommand : IRequest<AuthTokenDTO>
    {
        public CreateUserCommand(CreateOrUpdateUserDTO model)
        {
            Model = model;
        }

        public CreateOrUpdateUserDTO Model { get; }
    }

    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, AuthTokenDTO>
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<CreateOrUpdateUserDTO> _validator;
        private readonly ITokenService _token;

        public CreateUserCommandHandler(IUnitOfWork repository, IValidator<CreateOrUpdateUserDTO> validator, ITokenService token)
        {
            _repository = repository;
            _validator = validator;
            _token = token;
        }

        public async Task<AuthTokenDTO> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var model = request.Model;
            var result = _validator.Validate(model);

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors
                };
            }

            var user = new User
            {
                EmailAddress = model.EmailAddress,
                Password = model.Password,
                Role = model.Role
            };

            _repository.Users.Add(user);
            await _repository.CommitAsync();

            return _token.Generate(user);
        }
    }
}
