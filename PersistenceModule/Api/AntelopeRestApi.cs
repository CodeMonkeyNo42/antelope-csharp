using PersistenceModule.Data.Datamodules;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceModule.Api
{
    class AntelopeRestApi
    {
        private static AntelopeRestApi instance;
        public static AntelopeRestApi Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AntelopeRestApi("Ante", "Demo");
                }
                return instance;
            }
        }

        const string BaseUrl = "https://antelope.circinus.uberspace.de/";

        readonly string _accountSid;
        readonly string _secretKey;

        private AntelopeRestApi(string accountSid, string secretKey)
        {
            _accountSid = accountSid;
            _secretKey = secretKey;
        }

        public T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient();
            client.BaseUrl = BaseUrl;
            client.Authenticator = new HttpBasicAuthenticator(_accountSid, _secretKey);
            request.AddParameter("AccountSid", _accountSid, ParameterType.UrlSegment); // used on every request

            var response = client.Execute<T>(request);

            if (response.ErrorException != null)
            {
                const string message = "Error retrieving response.  Check inner details for more info.";
                var exe = new ApplicationException(message, response.ErrorException);
                throw exe;
            }

            return response.Data;
        }


        public Location GetLocation(int id)
        {
            var request = new RestRequest();
            request.Resource = "/locations/{id}";
            request.RootElement = "Location";

            request.AddParameter("id", id, ParameterType.UrlSegment);

            return Execute<Location>(request);
        }

        public List<Location> GetLocations()
        {
            var request = new RestRequest();
            //request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };
            request.Resource = "/locations";
            request.RootElement = "Locations";

            return Execute<List<Location>>(request);
        }
    }
}
