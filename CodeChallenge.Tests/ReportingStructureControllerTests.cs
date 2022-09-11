using CodeChallenge.Models;
using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CodeChallenge.Tests.Integration
{
    [TestClass]
    public class ReportingStructureControllerTests
    {
        private readonly TestServer _testServer;
        private readonly HttpClient _httpClient;

        public ReportingStructureControllerTests()
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [TestMethod]
        public void GetReportingStructureById_Returns_Ok()
        {
            // expected
            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            var numberOfReports = 4;

            // get Reporting Structure
            var getRequestTask = _httpClient.GetAsync($"api/reportingstructure/{employeeId}");
            var response = getRequestTask.Result;

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var reportingStructure = response.DeserializeContent<ReportingStructure>();
            Assert.AreEqual(reportingStructure.NumberOfReports, numberOfReports);
        }
    }
}
