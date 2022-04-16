using ContainerNinja.Contracts.Data;
using MediatR;

namespace ContainerNinja.Core.Handlers.Queries
{
    public class GetCountOfAllItemsQuery : IRequest<int>
    {
    }

    public class GetCountOfAllItemsQueryHandler : IRequestHandler<GetCountOfAllItemsQuery, int>
    {
        private readonly IUnitOfWork _repository;

        public GetCountOfAllItemsQueryHandler(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(GetCountOfAllItemsQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_repository.Items.Count());
        }
    }
}