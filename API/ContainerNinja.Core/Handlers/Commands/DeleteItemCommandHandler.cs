using MediatR;
using ContainerNinja.Contracts.Data;

namespace ContainerNinja.Core.Handlers.Commands
{
    public class DeleteItemCommand : IRequest<int>
    {
        public int ItemId { get; }

        public DeleteItemCommand(int itemId)
        {
            ItemId = itemId;
        }
    }

    public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommand, int>
    {
        private readonly IUnitOfWork _repository;

        public DeleteItemCommandHandler(IUnitOfWork repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(DeleteItemCommand request, CancellationToken cancellationToken)
        {
            _repository.Items.Delete(request.ItemId);
            await _repository.CommitAsync();
            return request.ItemId;
        }
    }
}