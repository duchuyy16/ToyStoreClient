using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
namespace ToyStoreClient.Helpers
{
    public static class AppContext
    {
        private static IHttpContextAccessor? _httpContextAccessor;

        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public static HttpContext? Current => _httpContextAccessor?.HttpContext;
    }
}
