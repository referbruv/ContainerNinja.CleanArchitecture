using ContainerNinja.Contracts.Services;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace ContainerNinja.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId
        {
            get
            {
                var context = _httpContextAccessor.HttpContext;
                if (context?.User != null && context?.User.Identity != null && context.User.Identity.IsAuthenticated)
                {
                    var identifier = context.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                    if (identifier != null)
                    {
                        return identifier.Value;
                    }
                }

                return string.Empty;
            }
        }
    }
}
