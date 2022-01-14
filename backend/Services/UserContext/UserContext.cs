using System.Security.Claims;

namespace Sharperio.Services.UserContext
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor httpContext;

        public UserContext(IHttpContextAccessor httpContext)
        {
            this.httpContext = httpContext;
        }
        public string Id => httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        public string Name => httpContext.HttpContext.User.FindFirstValue(ClaimTypes.Name);
        public string Role => httpContext.HttpContext.User.FindFirstValue(ClaimTypes.Role);
    }
}