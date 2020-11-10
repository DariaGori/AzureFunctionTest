using Newtonsoft.Json;
using System.Text;

namespace Helper
{
    public static class BinarySerializer
    {
        public static byte[] Serialize(object obj)
        {
            if (obj == null) return null;
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj));
        }

        public static T Deserialize<T>(byte[] byteInput)
        {
            return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(byteInput));
        }
    }
}
