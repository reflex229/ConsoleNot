using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace ConsoleNotLib
{
    public static class JsonWork
    {
        public static byte[] ToJson(Dictionary<string, string> dictionary) => Encoding.Unicode.
            GetBytes(JsonSerializer.Serialize(dictionary));

        public static Dictionary<string, string> FromJson(string json) =>
            JsonSerializer.Deserialize<Dictionary<string, string>>(json);
    }
}