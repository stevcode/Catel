using System;
using System.IO;
using System.Text;
using Catel.IoC;
using Catel.Runtime.Serialization;
using Catel.Runtime.Serialization.Json;
using JsonSerializer = Catel.Runtime.Serialization.Json.JsonSerializer;
using CLP.Entities;
using Newtonsoft.Json;


namespace CTesting
{
    class Program
    {
        private static readonly ISerializationManager SerializationManager = ServiceLocator.Default.ResolveType<ISerializationManager>();
        private static readonly IObjectAdapter ObjectAdapter = ServiceLocator.Default.ResolveType<IObjectAdapter>();

        static void Main(string[] args)
        {
            var desktopFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var testFilePath = Path.Combine(desktopFolderPath, "catelTesting.json");
            var jsonString = File.ReadAllText(testFilePath);

            using (var stream = new MemoryStream(Encoding.Default.GetBytes(jsonString)))
            {
                var configuration = new JsonSerializationConfiguration
                {
                    DateParseHandling = DateParseHandling.DateTime,
                    DateTimeKind = DateTimeKind.Unspecified,
                    DateTimeZoneHandling = DateTimeZoneHandling.Unspecified
                };

                var jsonSerializer = new JsonSerializer(SerializationManager, TypeFactory.Default, ObjectAdapter);
                var deserialized = jsonSerializer.Deserialize(typeof(CLPPage), stream, configuration);

                var page = (CLPPage)deserialized;
                
                Console.WriteLine(page.PageNumber);
            }

            Console.ReadLine();
        }
    }
}
