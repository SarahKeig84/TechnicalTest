using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Linq;
using NUnit.Framework;
using RestSharp;
using System.Net;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Newtonsoft.Json.Linq;

namespace GenbaTechnicalTest
{


    public class APIData
    {
        public const string tokenURL = "https://www.galacticoeleven.com/login";
        public const string createURL = "https://www.galacticoeleven.com/api/league/";
        public string token = string.Empty;


        public void GetToken()
        {
            //arrange
            RestClient client = new RestClient(tokenURL);
            JObject jObjectbody = new JObject();
            jObjectbody.Add("email", "tetaxog820@vy89.com");
            jObjectbody.Add("password", "Thisisatestp4ssword");

            RestRequest restRequest = new RestRequest(Method.POST);
            restRequest.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);

            // act
            IRestResponse response = client.Execute(restRequest);
            Console.WriteLine(response.Content);
            Console.WriteLine(jObjectbody);
            this.token = "Bearer " + response.Content;

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
           
        }

        public void CreateLeague()
        {
            // arrange
            RestClient client = new RestClient(createURL);
            JObject jObjectbody = new JObject();
            jObjectbody.Add("name", "Some league name");
            jObjectbody.Add("competition", 4);
            RestRequest restRequest = new RestRequest(Method.POST);
            var bearerToken = this.token;
            Console.WriteLine(bearerToken);
            restRequest.AddHeader("Authorization", bearerToken);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);

            // act
            IRestResponse response = client.Execute(restRequest);
            Console.WriteLine(response.Content);
            Console.WriteLine(jObjectbody);
            

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));

        }

        public void postman()
        {
            var client = new RestClient("https://www.galacticoeleven.com/api/league/");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJfaWQiOiI2MDAzZmM5MDI5YjUwOTAwMTYxODIyNDQiLCJpYXQiOjE2MTA4OTMzOTcsImV4cCI6MTYxMzQ4NTM5N30.5AoHHTjo1zwnpl6et8N-TwGjjiHBg3L1X0lGYsV8LGE");
            request.AddHeader("Content-Type", "application/json");
            request.AddParameter("application/json", "{\r\n    \"name\":\"Some league name\",\r\n    \"competition\":4\r\n}", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }
    }
}