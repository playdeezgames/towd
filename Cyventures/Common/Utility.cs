using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Utility
    {
        public static void Save<T>(T thing, string fileName)
        {
            using (var writer = File.CreateText(fileName))
            {
                writer.Write(JsonConvert.SerializeObject(thing));
            }
        }

        public static T Load<T>(string fileName)
        {
            using (var reader = File.OpenText(fileName))
            {
                return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
            }
        }
        public static T LoadEmbedded<T>(Assembly assembly, string resourceName)
        {
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                return JsonConvert.DeserializeObject<T>(reader.ReadToEnd());
            }
        }

    }
}
