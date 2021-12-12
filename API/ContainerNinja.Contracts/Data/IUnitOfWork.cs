using ContainerNinja.Contracts.Data.Repositories;

namespace ContainerNinja.Contracts.Data
{
    public interface IUnitOfWork
    {
        IItemRepository Items { get; }
        IUserRepository Users { get; }
        Task CommitAsync();
    }
}