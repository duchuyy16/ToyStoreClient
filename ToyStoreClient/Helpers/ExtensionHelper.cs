using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using ToyStoreClient.Models;

namespace ToyStoreClient.Helpers
{
    public static class ExtensionHelper
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, System.Text.Json.JsonSerializer.Serialize(value));
        }

        public static T? Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key) ?? default;
            return value != null ? System.Text.Json.JsonSerializer.Deserialize<T>(value) : default;
        }

        //public static string ConvertJwtToJsonObject(this string jwt)
        //{
        //    if (jwt == null) return string.Empty;

        //    var handler = new JwtSecurityTokenHandler();
        //    var jwtSecurityToken = handler.ReadJwtToken(jwt);
        //    var userModel = new CurrentUserModel()
        //    {
        //        Name = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name)?.Value!
        //    };

        //    return System.Text.Json.JsonSerializer.Serialize(userModel);
        //}

        public static string GetUserNameFromJwt(this string jwt)
        {
            if (jwt == null) return string.Empty;

            var handler = new JwtSecurityTokenHandler();
            var jwtSecurityToken = handler.ReadJwtToken(jwt);
            var nameClaim = jwtSecurityToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.Name);

            if (nameClaim != null)
                return nameClaim.Value;
            else
                return string.Empty;

        }
    }
}
