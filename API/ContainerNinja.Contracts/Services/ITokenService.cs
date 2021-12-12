using ContainerNinja.Contracts.Data.Entities;
using ContainerNinja.Contracts.DTO;

namespace ContainerNinja.Contracts.Services
{
    public interface ITokenService
    {
        AuthTokenDTO Generate(User user);
    }
}