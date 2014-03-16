﻿using Microsoft.Practices.Prism.Events;
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
using System.Net;

namespace PersistenceModule.Api
{
    class AntelopeRestApi
    {
        const string BaseUrl = @"https://antelope.circinus.uberspace.de/";
        //const string BaseUrl = @"https://antelope:8443/"; // ip für antelope in c:\windows\system32\drivers\etc\hosts konfiguriert
        //const string BaseUrl = @"http://antelope:3000/"; // ip für antelope in c:\windows\system32\drivers\etc\hosts konfiguriert
        //const string BaseUrl = @"http://antelope2:3000/"; // ip für antelope2 in c:\windows\system32\drivers\etc\hosts konfiguriert

        string Login { get; set; }
        string Password { get; set; }
        IEventAggregator EventAggregator { get; set; }

        public AntelopeRestApi(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;

            EventAggregator.GetEvent<LoginAndPasswordChangedEvent>().Subscribe(OnLoginAndPasswordChanged);

            // to ignore errors from selfcertified servers 
            ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
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

            // we use our own deserializer
            client.AddHandler("application/json", new RestSharpDataContractJsonDeserializer());

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

            var statusCode = (int)response.StatusCode;
            if(statusCode < 200 || statusCode > 399)
            {
                const string message = "Error HTTP.  Check data for more info.";
                var exe = new ApplicationException(message);
                exe.Data["StatusCode"] = (int)response.StatusCode;
                exe.Data["StatusCodeDescription"] = response.StatusDescription;
                exe.Data["Content"] = response.Content;
                throw exe;
            }

            return response.Data;
        }

        public DatamoduleType Get<DatamoduleType>(int id, int championshipId = 0, int matchId = 0) where DatamoduleType : IDatamodul, new()
        {
            var request = new RestRequest();
            request.JsonSerializer = new RestSharpDataContractJsonSerializer();
            var datamodule = new DatamoduleType();

            var requestUrlPart = datamodule.GetRequestUrlPart();

            if (championshipId != 0)
            {
                requestUrlPart = requestUrlPart.Replace("{ChampionshipId}", championshipId.ToString());
            }

            if (matchId != 0)
            {
                requestUrlPart = requestUrlPart.Replace("{MatchId}", matchId.ToString());
            }

            request.Resource = "/" + requestUrlPart + "/" + id.ToString();

            return Execute<DatamoduleType>(request);
        }

        public DatamoduleType Post<DatamoduleType>(DatamoduleType datamodul) where DatamoduleType : IDatamodul, new()
        {
            var request = new RestRequest();

            request.Method = Method.POST;
            //request.JsonSerializer.RootElement = typeof(DatamoduleType).Name;
            request.JsonSerializer = new RestSharpDataContractJsonSerializer();
            request.Resource = "/" + datamodul.GetRequestUrlPart();

            request.RequestFormat = DataFormat.Json;
            //request.AddBody(datamodul.GetPostObject());
            request.AddBody(datamodul);

            return Execute<DatamoduleType>(request);
        }

        public DatamoduleType Put<DatamoduleType>(DatamoduleType datamodul) where DatamoduleType : IDatamodul, new()
        {
            var request = new RestRequest();

            request.Method = Method.PUT;
            request.JsonSerializer = new RestSharpDataContractJsonSerializer();
            request.Resource = "/" + datamodul.GetRequestUrlPart() + "/" + datamodul.Id;

            request.RequestFormat = DataFormat.Json;
            // request.AddBody(datamodul.GetPutObject());
            request.AddBody(datamodul);

            return Execute<DatamoduleType>(request);
        }

        public List<DatamoduleType> GetCollection<DatamoduleType>() where DatamoduleType : IDatamodul, new()
        {
            var request = new RestRequest();
            request.JsonSerializer = new RestSharpDataContractJsonSerializer();

            var datamodule = new DatamoduleType();

            request.Resource = "/" + datamodule.GetRequestUrlPart();

            return Execute<List<DatamoduleType>>(request);
        }

    }
}
