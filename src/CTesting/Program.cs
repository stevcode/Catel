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
    using System.Linq;
    using Catel.Data;

    class Program
    {
        private static readonly ISerializationManager SerializationManager = ServiceLocator.Default.ResolveType<ISerializationManager>();
        private static readonly IObjectAdapter ObjectAdapter = ServiceLocator.Default.ResolveType<IObjectAdapter>();

        static void Main(string[] args)
        {
            var configuration = new JsonSerializationConfiguration
            {
                DateParseHandling = DateParseHandling.DateTime,
                DateTimeKind = DateTimeKind.Unspecified,
                DateTimeZoneHandling = DateTimeZoneHandling.Unspecified,
                IsEnumSerializedWithString = true
            };

            //var model = new Testy();
            //model.AUnsignedNullable = 0;
            //model.Something = Somethings.Thing;

            //model.AllThePoints.Add(new Point(5, 5));

            //model.AllTheDictionaries.Add("test", 1);
            //model.AllTheDictionaries.Add("testy", 2);
            //model.AllTheDictionaries.Add("testo", 3);
            //var jsonStringTest = string.Empty;

            //using (var stream = new MemoryStream())
            //{
            //    var jsonSerializer = new JsonSerializer(SerializationManager, TypeFactory.Default, ObjectAdapter);
            //    jsonSerializer.Serialize(model, stream, configuration);

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
            //    var deserialized = jsonSerializer.Deserialize(typeof(Testy), stream, configuration);

            //    var dTesty = (Testy)deserialized;

            //    Console.WriteLine(dTesty.AllThePoints);
            //}

            var desktopFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var testFilePath = Path.Combine(desktopFolderPath, "catelTesting.json");
            var jsonString = File.ReadAllText(testFilePath);

            using (var stream = new MemoryStream(Encoding.Default.GetBytes(jsonString)))
            {
                var jsonSerializer = new JsonSerializer(SerializationManager, TypeFactory.Default, ObjectAdapter);
                var deserialized = jsonSerializer.Deserialize(typeof(CLPPage), stream, configuration);

                var page = (CLPPage)deserialized;
                var firstTag = page.Tags.First() as EquivalenceRelationDefinitionTag;
                var leftSide = firstTag.LeftRelationPart as MultiplicationRelationDefinitionTag;
                var firstFactor = leftSide.Factors.First() as NumericValueDefinitionTag;
                var val = firstFactor.NumericValue;

                Console.WriteLine(val);
            }

            Console.ReadLine();
        }

        public enum Somethings
        {
            Some,
            Thing,
            Is,
            An,
            Enumy
        }

        [Serializable]
        public class Testy : ModelBase
        {
            public Testy()
            {
            }

            /// <summary>SUMMARY</summary>
            public uint? AUnsignedNullable
            {
                get { return GetValue<uint?>(AUnsignedNullableProperty); }
                set { SetValue(AUnsignedNullableProperty, value); }
            }

            public static readonly PropertyData AUnsignedNullableProperty = RegisterProperty("AUnsignedNullable", typeof(uint?), null);
            

            /// <summary>SUMMARY</summary>
            public Somethings Something
            {
                get { return GetValue<Somethings>(SomethingProperty); }
                set { SetValue(SomethingProperty, value); }
            }

            public static readonly PropertyData SomethingProperty = RegisterProperty("Something", typeof(Somethings), Somethings.Some);
            

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
