using System;
using System.IO;
using System.Text;
using System.Windows;
using Catel.IoC;
using Catel.Runtime.Serialization;
using Catel.Runtime.Serialization.Json;
using JsonSerializer = Catel.Runtime.Serialization.Json.JsonSerializer;
using CLP.Entities;
using Newtonsoft.Json;


namespace CTesting
{
    using System.Collections.Generic;
    using Catel.Data;

    class Program
    {
        private static readonly ISerializationManager SerializationManager = ServiceLocator.Default.ResolveType<ISerializationManager>();
        private static readonly IObjectAdapter ObjectAdapter = ServiceLocator.Default.ResolveType<IObjectAdapter>();

        static void Main(string[] args)
        {

            //var model = new Testy();
            //model.AllThePoints.Add(new Point(5, 5));

            //model.AllTheDictionaries.Add("test", 1);
            //model.AllTheDictionaries.Add("testy", 2);
            //model.AllTheDictionaries.Add("testo", 3);
            //string jsonStringTest = string.Empty;


            //using (var stream = new MemoryStream())
            //{
            //    var jsonSerializer = new JsonSerializer(SerializationManager, TypeFactory.Default, ObjectAdapter);
            //    jsonSerializer.Serialize(model, stream);

            //    stream.Position = 0L;

            //    using (var streamReaderTest = new StreamReader(stream))
            //    {
            //        jsonStringTest = streamReaderTest.ReadToEnd();
            //    }
            //}
            //Console.WriteLine(jsonStringTest);

            //using (var stream = new MemoryStream(Encoding.Default.GetBytes(jsonStringTest)))
            //{
            //    var jsonSerializer = new JsonSerializer(SerializationManager, TypeFactory.Default, ObjectAdapter);
            //    var deserialized = jsonSerializer.Deserialize(typeof(Testy), stream);

            //    var dTesty = (Testy)deserialized;

            //    Console.WriteLine(dTesty.AllThePoints);
            //}

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

        [Serializable]
        public class Testy : ModelBase
        {
            public Testy()
            {
            }

            /// <summary>SUMMARY</summary>
            public List<Point> AllThePoints
            {
                get { return GetValue<List<Point>>(AllThePointsProperty); }
                set { SetValue(AllThePointsProperty, value); }
            }

            public static readonly PropertyData AllThePointsProperty = RegisterProperty("AllThePoints", typeof(List<Point>), () => new List<Point>());

            /// <summary>SUMMARY</summary>
            public Dictionary<string, int> AllTheDictionaries
            {
                get { return GetValue<Dictionary<string, int>>(AllTheDictionariesProperty); }
                set { SetValue(AllTheDictionariesProperty, value); }
            }

            public static readonly PropertyData AllTheDictionariesProperty = RegisterProperty("AllTheDictionaries", typeof(Dictionary<string, int>), () => new Dictionary<string,int>());
            

        }
    }
}
