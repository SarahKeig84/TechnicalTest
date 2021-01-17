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
        Users users = new Users();
        public const string tokenURL = "https://www.galacticoeleven.com/login";
        public const string createURL = "https://www.galacticoeleven.com/api/league/";
        public string token = string.Empty;
        public string leagueID = string.Empty;


        public void GetToken()
        {
            //arrange
            RestClient client = new RestClient(tokenURL);
            JObject jObjectbody = new JObject();
            jObjectbody.Add("email", users.email );
            jObjectbody.Add("password", users.password);

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

        public void CreateLeague(string leagueName, int competition)
        {
            // arrange
            RestClient client = new RestClient(createURL);
            JObject jObjectbody = new JObject();
            jObjectbody.Add("name", leagueName);
            jObjectbody.Add("competition", competition);
            RestRequest restRequest = new RestRequest(Method.POST);
            var bearerToken = this.token;
           // Console.WriteLine(bearerToken);
            restRequest.AddHeader("Authorization", bearerToken);
            restRequest.AddHeader("Content-Type", "application/json");
            restRequest.AddParameter("application/json", jObjectbody, ParameterType.RequestBody);

            // act
            IRestResponse response = client.Execute(restRequest);
            //Console.WriteLine(response.Content);
            //Console.WriteLine(jObjectbody);            

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(response.Content.Contains(leagueName));
            Assert.That(response.Content.Contains(competition.ToString()));
            var jObject = JObject.Parse(response.Content);
           // Console.WriteLine(jObject);
            this.leagueID = jObject.GetValue("_id").ToString();
           // Console.WriteLine(this.leagueID);

        }

        public void CheckLeagueCreated(string leagueName, int competition)
        {
            //arrange
            RestClient client = new RestClient(createURL);
            RestRequest restRequest = new RestRequest(Method.GET);
            var bearerToken = this.token;
            // Console.WriteLine(bearerToken);
            restRequest.AddHeader("Authorization", bearerToken);
            restRequest.AddHeader("Content-Type", "application/json");

            // act
            IRestResponse response = client.Execute(restRequest);
            //Console.WriteLine(response.Content);

            // assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            var responseArray = JArray.Parse(response.Content);
            Console.WriteLine("League Id: " + this.leagueID);
            JObject createdLeague = responseArray.Children<JObject>()
                .FirstOrDefault(o => o["_id"] != null && o["_id"].ToString() == this.leagueID);
            //Console.WriteLine(createdLeague);
            var createdLeagueName = createdLeague["name"].ToString();
            //Console.WriteLine(createdLeagueName);
            var createdLeagueCompetition = createdLeague["competition"].ToString();
            //Console.WriteLine(createdLeagueCompetition);
            Assert.AreEqual(createdLeagueName, leagueName);
            Assert.AreEqual(createdLeagueCompetition, competition.ToString());
        }
    }
}