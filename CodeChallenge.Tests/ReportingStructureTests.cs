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
    public class ReportingStructureTests
    {
        private readonly TestServer _testServer;
        private readonly HttpClient _httpClient;

        public ReportingStructureTests()
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        /// <summary>
        /// Test with the reporting structure depth of 2 (max depth with current test data)
        /// </summary>
        [TestMethod]
        public void GetReportingStructureById_Returns_Ok_Depth_2()
        {
            // expected
            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f"; // John
            var numberOfReports = 4;

            // get Reporting Structure
            var getRequestTask = _httpClient.GetAsync($"api/employee/{employeeId}/ReportingStructure");
            var response = getRequestTask.Result;

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var reportingStructure = response.DeserializeContent<ReportingStructure>();
            Assert.AreEqual(numberOfReports, reportingStructure.NumberOfReports);
        }

        /// <summary>
        /// Test with the reporting structure depth of 1
        /// </summary>
        [TestMethod]
        public void GetReportingStructureById_Returns_Ok_Depth_1()
        {
            // expected
            var employeeId = "03aa1462-ffa9-4978-901b-7c001562cf6f"; // Ringo
            var numberOfReports = 2;

            // get Reporting Structure
            var getRequestTask = _httpClient.GetAsync($"api/employee/{employeeId}/ReportingStructure");
            var response = getRequestTask.Result;

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var reportingStructure = response.DeserializeContent<ReportingStructure>();
            Assert.AreEqual(numberOfReports, reportingStructure.NumberOfReports);
        }

        /// <summary>
        /// Test with reporting structure depth of 0 (no direct reports)
        /// </summary>
        [TestMethod]
        public void GetReportingStructureById_Returns_Ok_Depth_0()
        {
            // expected
            var employeeId = "62c1084e-6e34-4630-93fd-9153afb65309"; // Pete
            var numberOfReports = 0;

            // get Reporting Structure
            var getRequestTask = _httpClient.GetAsync($"api/employee/{employeeId}/ReportingStructure");
            var response = getRequestTask.Result;

            Assert.IsNotNull(response);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var reportingStructure = response.DeserializeContent<ReportingStructure>();
            Assert.AreEqual(numberOfReports, reportingStructure.NumberOfReports);
        }
    }
}
