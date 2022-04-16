using AutoMapper;
using ContainerNinja.Contracts.Data;
using ContainerNinja.Contracts.DTO;
using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace ContainerNinja.Core.Handlers.Queries
{
    public class GetAllItemsQuery : IRequest<IEnumerable<ItemDTO>>
    {
    }

    public class GetAllItemsQueryHandler : IRequestHandler<GetAllItemsQuery, IEnumerable<ItemDTO>>
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;

        public GetAllItemsQueryHandler(IUnitOfWork repository, IMapper mapper, IDistributedCache cache)
        {
            _repository = repository;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<IEnumerable<ItemDTO>> Handle(GetAllItemsQuery request, CancellationToken cancellationToken)
        {
            var cachedEntitiesString = await _cache.GetStringAsync("all_items");
            
            if (cachedEntitiesString == null)
            {
                var entities = await Task.FromResult(_repository.Items.GetAll());
                var result = _mapper.Map<IEnumerable<ItemDTO>>(entities);

                await _cache.SetStringAsync("all_items", JsonConvert.SerializeObject(result));
                return result;
            }
            else
            {
                var cachedEntities = JsonConvert.DeserializeObject<IEnumerable<ItemDTO>>(cachedEntitiesString);
                return cachedEntities;
            }

        }
    }
}