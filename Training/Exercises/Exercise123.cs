using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using commercetools.Sdk.Client;
using commercetools.Sdk.Domain;
using commercetools.Sdk.HttpApi;
using commercetools.Sdk.Serialization;
using Microsoft.Extensions.Configuration;
using Training.GraphQL;

namespace Training
{
    public class Exercise123 : IExercise
    {
        private const string CLIENTNAME = "MachineLClient";
        
        private readonly IHttpClientFactory httpClientFactory;
        private readonly ISerializerService serializerService;
        
        
        private HttpClient httpClient;
        
        private HttpClient HttpClient
        {
            get
            {
                if (this.httpClient == null)
                {
                    this.httpClient = this.httpClientFactory.CreateClient(CLIENTNAME);
                }

                return this.httpClient;
            }
        }

        public Exercise123(IHttpClientFactory httpClientFactory, ISerializerService serializerService)
        {
            this.httpClientFactory = httpClientFactory;
            this.serializerService = serializerService;
        }
        
        public void Execute()
        {
            TestML();
        }

        private async void TestML()
        {
            var requestMessage = this.GetRequestMessage();
            
            var result = await this.HttpClient.SendAsync(requestMessage).ConfigureAwait(false);
            string content = await result.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (result.IsSuccessStatusCode)
            {
                
            }
        }
        /// <summary>
        /// Create HttpRequest Message with Query as json in the content
        /// </summary>
        /// <returns></returns>
        private HttpRequestMessage GetRequestMessage()
        {
            var requestUri = this.GetRequestUri();
            
            HttpRequestMessage request = new HttpRequestMessage
            {
                RequestUri = requestUri,
                Method = HttpMethod.Get
            };
            request.Headers.Add("Authorization", "Bearer i9tJLvlJD3OkVMdkbZ6y-ue3_CTQPeWL");
            //addHeader token
            return request;
        }
        private Uri GetRequestUri()
        {
            string requestUri = $"https://ml-eu.europe-west1.gcp.commercetools.com/test-dotnet-michele-32/recommendations/general-categories?productName=black";
            //string requestUri = $"https://api.sphere.io/test-dotnet-michele-32/categories/b96ec169-9870-4286-a2cd-bf41caaba491";
            return new Uri(requestUri);
        }
    }
    
    
}