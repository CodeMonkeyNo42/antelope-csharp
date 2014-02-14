using Microsoft.Practices.Prism.Events;
using PersistenceModule.Data.Datamodules;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Interfaces.Events;
using Interfaces.PersisenceModule.Datamodule;
using RestSharp.Deserializers;
using System.Collections.ObjectModel;

namespace PersistenceModule.Api
{
    class AntelopeRestApi
    {
        const string BaseUrl = @"https://antelope.circinus.uberspace.de/";

        string Login { get; set; }
        string Password { get; set; }
        IEventAggregator EventAggregator { get; set; }

        public AntelopeRestApi(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;

            EventAggregator.GetEvent<LoginAndPasswordChangedEvent>().Subscribe(OnLoginAndPasswordChanged);
        }

        private void OnLoginAndPasswordChanged(Tuple<string,string> loginAndPassword)
        {
            Login = loginAndPassword.Item1;
            Password = loginAndPassword.Item2;
        }

        public T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient();

            client.BaseUrl = BaseUrl;
            client.Authenticator = new HttpBasicAuthenticator(Login, Password);

            // used on every request
            request.AddHeader("User-Agent", "antelope-csharp/1.0 (alpha Version)");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            request.AddParameter("AccountSid", Login, ParameterType.UrlSegment);
            

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
            //request.RootElement = "Location";

            request.AddParameter("id", id, ParameterType.UrlSegment);

            return Execute<Location>(request);
        }

        public Location PostLocation(Location location)
        {
            var request = new RestRequest();
            request.Method = Method.POST;
            request.Resource = "/locations";

            // ## first option ##
            request.RequestFormat = DataFormat.Json;
            //request.RootElement = "location";
            request.AddBody( new { name = location.Name } );

            // ## second option ##
            // request.AddParameter("name", location.Name);
 
            return Execute<Location>(request);
        }

        public Location PutLocation(Location location)
        {
            var request = new RestRequest();
            request.Method = Method.PUT;
            request.Resource = "/locations";

            // ## first option ##
            request.RequestFormat = DataFormat.Json;
            //request.RootElement = "location";
            request.AddBody(new { id = location.Id, name = location.Name });

            // ## second option ##
            // request.AddParameter("name", location.Name);

            return Execute<Location>(request);
        }

        public List<Location> GetLocations()
        {
            var request = new RestRequest();
            
            //request.RequestFormat = DataFormat.Json;
            //request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; };

            request.Resource = "/locations";
            //request.RootElement = "Locations"; // kein Rootelement für arrays setzen

            return Execute<List<Location>>(request);
        }
    }
}
