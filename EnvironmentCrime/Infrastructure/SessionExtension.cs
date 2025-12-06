using System.Text.Json;

namespace EnvironmentCrime.Infrastructure
{
  public static class SessionExtension
  {
    // Extension methods to set and get complex objects in session using JSON serialization
    public static void Set<T>(this ISession session, string key, T value)
    {
      session.SetString(key, JsonSerializer.Serialize(value));
    }
    // Nullable reference type enabled for this method
    public static T? Get<T>(this ISession session, string key)
    {
      var value = session.GetString(key);
      return value == null ? default : JsonSerializer.Deserialize<T>(value);
    }
  }
}
