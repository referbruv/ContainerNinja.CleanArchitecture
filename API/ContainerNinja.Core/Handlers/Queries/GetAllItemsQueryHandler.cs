using AutoMapper;
using ContainerNinja.Contracts.Data;
using ContainerNinja.Contracts.DTO;
using MediatR;

namespace ContainerNinja.Providers.Handlers.Queries
{
    public class GetAllItemsQuery : IRequest<IEnumerable<ItemDTO>>
    {
    }

    public class GetAllItemsQueryHandler : IRequestHandler<GetAllItemsQuery, IEnumerable<ItemDTO>>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;

        public GetAllItemsQueryHandler(IUnitOfWork repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ItemDTO>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
        {
            var entities = await Task.FromResult(_repository.Items.GetAll());
            return _mapper.Map<IEnumerable<ItemDTO>>(entities);
        }
    }
}