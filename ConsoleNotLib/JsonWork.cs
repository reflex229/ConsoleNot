using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ConsoleNotLib
{
    public static class JsonWork
    {
        public static byte[] ToJson(Dictionary<string, object> dictionary) => Encoding.Unicode.
            GetBytes(JsonSerializer.Serialize(dictionary));

        public static Dictionary<string, object> FromJson(string jsonBytes) =>
            JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
    }
}