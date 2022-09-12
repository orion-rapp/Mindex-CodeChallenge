using CodeChallenge.Models;
using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net;
using System.Net.Http;
using System.Text;

namespace CodeChallenge.Tests.Integration
{
    [TestClass]
    public class CompensationTests
    {
        private readonly TestServer _testServer;
        private readonly HttpClient _httpClient;

        public CompensationTests()
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        /// <summary>
        /// Tests the saving of Compensation models
        /// </summary>
        [TestMethod]
        public void CreateCompensation_Returns_Created()
        {
            // expected
            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f"; // John
            var compensation = new Compensation()
            {
                Salary = 140000.29M,
                EffectiveDate = DateTime.Now
            };

            // make the post
            var requestContent = new JsonSerialization().ToJson(compensation);

            var postRequestTask = _httpClient.PostAsync($"api/employee/{employeeId}/compensation",
                new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;
            
            // Assert behavior
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            
            // Assert values
            var returnedCompensation = response.DeserializeContent<Compensation>();
            Assert.AreEqual(compensation.Salary, returnedCompensation.Salary);
            Assert.AreEqual(compensation.EffectiveDate, returnedCompensation.EffectiveDate);
            Assert.AreEqual(employeeId, returnedCompensation.Employee.EmployeeId);
        }

        /// <summary>
        /// Since Database is seeded with no compensation entities, this first creates a compensation
        /// entity to then retrieve correctly
        /// </summary>
        [TestMethod]
        public void NewlyCreatedCompensationReturns_Ok()
        {
            // expected
            var employeeId = "b7839309-3348-463b-a7e3-5de1c168beb3"; // Paul
            var compensation = new Compensation()
            {
                Salary = 140000.29M,
                EffectiveDate = DateTime.Now
            };

            // post to save
            var requestContent = new JsonSerialization().ToJson(compensation);

            var postRequestTask = _httpClient.PostAsync($"api/employee/{employeeId}/compensation",
                new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var postResponse = postRequestTask.Result;

            // make get request
            var getRequestTask = _httpClient.GetAsync($"api/employee/{employeeId}/compensation");
            var response = getRequestTask.Result;

            // Assert behavior
            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            // Assert values
            var returnedCompensation = response.DeserializeContent<Compensation>();
            Assert.AreEqual(employeeId, returnedCompensation.Employee.EmployeeId);
            Assert.AreEqual(compensation.Salary, returnedCompensation.Salary);
            Assert.AreEqual(compensation.EffectiveDate, returnedCompensation.EffectiveDate);
        }
    }
}
