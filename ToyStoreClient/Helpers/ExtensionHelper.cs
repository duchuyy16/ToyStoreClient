using Microsoft.AspNetCore.Http;

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
    }
}
