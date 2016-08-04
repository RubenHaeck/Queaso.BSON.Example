using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using Queaso.BSON.Example.Models;

namespace Queaso.BSON.Example
{
    class Program
    {
        static void Main(string[] args)
        {
            // From Object to Json
            var @event = new Event()
            {
                ID = 1,
                Name = "SCRUM daily standup",
                StartDate = new DateTime(2016, 8, 5, 10, 0, 0),
                EndDate = new DateTime(2016, 8, 5, 12, 0, 0),
                AllDay = false
            };

            string jsonOutput = JsonConvert.SerializeObject(@event);

            Console.WriteLine(jsonOutput);

            // From Object to Bson (Binary Json)

            var memoryStream = new MemoryStream();
            using (var bsonWriter = new BsonWriter(memoryStream))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(bsonWriter, @event);
            }

            string bsonOutput = Convert.ToBase64String(memoryStream.ToArray());

            // From Bson to Object
            byte[] data =
                Convert.FromBase64String(
                    "WAAAABBJRAABAAAAAk5hbWUAFAAAAFNDUlVNIGRhaWx5IHN0YW5kdXAACVN0YXJ0RGF0ZQAATLdZVgEAAAlFbmREYXRlAAApJVpWAQAACEFsbERheQAAAA==");

            var memoryStream2 = new MemoryStream(data);
            using (var bsonReader = new BsonReader(memoryStream2))
            {
                JsonSerializer serializer = new JsonSerializer();
                var myConvertedEvent = serializer.Deserialize<Event>(bsonReader);

                Console.WriteLine("{0} : {1} with following properties : {2}", "Event", myConvertedEvent.ID,
                    myConvertedEvent.AllDay);
            }

        }
    }
}
