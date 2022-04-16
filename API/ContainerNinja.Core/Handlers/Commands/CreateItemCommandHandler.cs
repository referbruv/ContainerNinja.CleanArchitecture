using MediatR;
using ContainerNinja.Contracts.Data;
using ContainerNinja.Contracts.DTO;
using ContainerNinja.Contracts.Data.Entities;
using FluentValidation;
using ContainerNinja.Core.Exceptions;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace ContainerNinja.Core.Handlers.Commands
{
    public class CreateItemCommand : IRequest<ItemDTO>
    {
        public CreateOrUpdateItemDTO Model { get; }
        public CreateItemCommand(CreateOrUpdateItemDTO model)
        {
            this.Model = model;
        }
    }

    public class CreateItemCommandHandler : IRequestHandler<CreateItemCommand, ItemDTO>
    {
        private readonly IUnitOfWork _repository;
        private readonly IValidator<CreateOrUpdateItemDTO> _validator;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateItemCommandHandler> _logger;

        public CreateItemCommandHandler(ILogger<CreateItemCommandHandler> logger, IUnitOfWork repository, IValidator<CreateOrUpdateItemDTO> validator, IMapper mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<ItemDTO> Handle(CreateItemCommand request, CancellationToken cancellationToken)
        {
            CreateOrUpdateItemDTO model = request.Model;

            var result = _validator.Validate(model);

            _logger.LogInformation($"CreateItem Validation result: {result}");

            if (!result.IsValid)
            {
                var errors = result.Errors.Select(x => x.ErrorMessage).ToArray();
                throw new InvalidRequestBodyException
                {
                    Errors = errors
                };
            }

            var entity = new Item
            {
                Name = model.Name,
                Description = model.Description,
                Categories = model.Categories,
                ColorCode = model.ColorCode
            };

            _repository.Items.Add(entity);
            await _repository.CommitAsync();

            return _mapper.Map<ItemDTO>(entity);
        }
    }
}