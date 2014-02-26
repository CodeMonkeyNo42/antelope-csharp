using RestSharp;
using RestSharp.Deserializers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceModule.Api
{
    public class RestSharpDataContractJsonDeserializer : IDeserializer
    {
        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string DateFormat { get; set; }
        public CultureInfo Culture { get; set; }

        public RestSharpDataContractJsonDeserializer()
        {
            Culture = CultureInfo.InvariantCulture;
        }

        public T Deserialize<T>(IRestResponse response)
        {
            Debug.WriteLine(response.Content);
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(response.Content)))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                return (T)ser.ReadObject(ms);
            }
        }
    }
}
