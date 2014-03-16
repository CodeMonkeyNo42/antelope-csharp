using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceModule.Api
{
    public class RestSharpDataContractJsonSerializer : ISerializer
    {

        public RestSharpDataContractJsonSerializer()
        {
            ContentType = "application/json";
        }

        ///
        /// Serialize the object as JSON
        ///
        /// <param name="obj" />Object to serialize
        /// JSON as String
        public string Serialize(object obj)
        {
            //Create a stream to serialize the object to.
            using (MemoryStream ms = new MemoryStream())
            {
                // Serializer the User object to the stream.
                Type type = obj.GetType();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(type);
                ser.WriteObject(ms, obj);
                byte[] json = ms.ToArray();

                string output = Encoding.UTF8.GetString(json, 0, json.Length);
                output = "{ \"" + type.Name.ToLower() + "\" : " + output + "}";

                Debug.WriteLine("requestcontent: " + output);

                return output;
            }
        }

        ///
        /// Unused for JSON Serialization
        ///
        public string DateFormat { get; set; }
        ///
        /// Unused for JSON Serialization
        ///
        public string RootElement { get; set; }
        ///
        /// Unused for JSON Serialization
        ///
        public string Namespace { get; set; }
        ///
        /// Content type for serialized content
        ///
        public string ContentType { get; set; }
    }
}
