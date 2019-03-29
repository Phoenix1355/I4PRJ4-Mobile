using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Requests;
using i4prj.SmartCab.Responses;
using i4prj.SmartCab.Services;
using NSubstitute;
using NUnit.Framework;

namespace i4prj.SmartCab.UnitTests.Services
{
    [TestFixture]
    public class AzureApiServiceTests
    {
        private IBackendApiService _uut;
        private CreateCustomerRequest _createCustomerRequest;
        private CreateCustomerResponse _createCustomerResponse;

        [SetUp]
        public void SetUp()
        {
            _uut=new AzureApiService();
        }

        [Test]
        public void SubmitCreateCustomerRequest_Request()
        {
            //todo
            //Kalder rent faktisk op mod API'et hvilket jo ikke er hvad vi vil.
            //Samme også med login og "get all rides".
            //var result = _uut.SubmitCreateCustomerRequest(_createCustomerRequest);
        }
    }
}
