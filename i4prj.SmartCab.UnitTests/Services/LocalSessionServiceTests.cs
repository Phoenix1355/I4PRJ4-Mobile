using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Responses;
using i4prj.SmartCab.Services;
using i4prj.SmartCab.UnitTests.Mocks;
using Newtonsoft.Json;
using NSubstitute;
using NUnit.Framework;

namespace i4prj.SmartCab.UnitTests.Services
{
    [TestFixture]
    public class LocalSessionServiceTests
    {


        [SetUp]
        public void SetUp()
        {

        }


        [Test]
        public async Task SubmitCreateCustomerCommand_ReceivesValidJsonResponse_ReturnsExpectedValue()
        {

        }


    }
}