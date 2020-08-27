using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace ConsoleApp1
{
    public class BaseClient : RestClient
    { 
        public BaseClient(string baseUrl)
        {
            BaseUrl = new Uri(baseUrl);
        }

        public override IRestResponse Execute(IRestRequest request)
        {
            IRestResponse response = base.Execute(request);
            return response;
        }

        public override IRestResponse<T> Execute<T>(IRestRequest request)
        {
            IRestResponse<T> response = base.Execute<T>(request);

            return response;
        }

        public IRestResponse<R> Get<R>(string url,
            Dictionary<string, string> headers = null,
            Dictionary<string, string> parameters = null,
            string requestBody = null)
            where R : new()
        {
            var request = new RestRequest(url, Method.GET);
            request.AddJsonBody(requestBody);

            if (parameters != null)
            {
                foreach (KeyValuePair<string, string> item in parameters)
                {
                    request.AddParameter(item.Key, item.Value);
                }
            }
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> item in headers)
                {
                    request.AddHeader(item.Key, item.Value);
                }
            }

            IRestResponse<R> response = Execute<R>(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response;
            }
            else
            {
                return null;
            }
        }

        public IRestResponse<R> Post<R>(string url,
            string requestBody = null,
            Dictionary<string, string> headers = null,
            Dictionary<string, string> parametrs = null,
            string header = "application/json",
            string token = null)
            where R : new()
        {
            var request = new RestRequest(url, Method.POST);
            request.AddHeader("Content-Type", header);

            if (!String.IsNullOrWhiteSpace(token))
            {
                request.AddHeader("Authorization", "Bearer " + token);
            }
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> item in headers)
                {
                    request.AddHeader(item.Key, item.Value);
                }
            }

            if (parametrs != null)
            {
                foreach (KeyValuePair<string, string> item in parametrs)
                {
                    request.AddParameter(item.Key, item.Value);
                }
            }

            if (!String.IsNullOrWhiteSpace(requestBody))
            {
                request.AddJsonBody(requestBody);
            }

            IRestResponse<R> response = Execute<R>(request);
            if (response.StatusCode == HttpStatusCode.OK
                || response.StatusCode == HttpStatusCode.Created)
            {
                return response;
            }
            else
            {
                return null;
            }
        }

        public IRestResponse Post(string url,
            Dictionary<string, string> headers = null,
            Dictionary<string, string> parametrs = null,
            string requestBody = null,
            string header = "application/json",
            string token = null)
        {
            var request = new RestRequest(url, Method.POST);
            request.AddHeader("Content-Type", header);

            if (!String.IsNullOrWhiteSpace(token))
            {
                request.AddHeader("Authorization", "Bearer " + token);
            }
            if (headers != null)
            {
                foreach (KeyValuePair<string, string> item in headers)
                {
                    request.AddHeader(item.Key, item.Value);
                }
            }

            if (parametrs != null)
            {
                foreach (KeyValuePair<string, string> item in parametrs)
                {
                    request.AddParameter(item.Key, item.Value);
                }
            }

            if (!String.IsNullOrWhiteSpace(requestBody))
            {
                request.AddJsonBody(requestBody);
            }

            IRestResponse response = Execute(request);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response;
            }
            else
            {
                return null;
            }
        }
    }
}
