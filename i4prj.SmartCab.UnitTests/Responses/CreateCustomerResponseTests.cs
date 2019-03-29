using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using i4prj.SmartCab.Responses;
using NSubstitute;
using NUnit.Framework;

namespace i4prj.SmartCab.UnitTests.Responses
{
    [TestFixture]
    public class CreateCustomerResponseTests
    {
        private CreateCustomerResponse _uut;
        private HttpResponseMessage _responseMessage;

        [SetUp]
        public void SetUp()
        {
            _responseMessage = new HttpResponseMessage(HttpStatusCode.Accepted);
            _uut =new CreateCustomerResponse(_responseMessage);

        }

        #region Ctor

        [Test]
        public void Ctor_PassedHttpResponseMessage_PropertyIsSet()
        {
            Assert.That(_uut.HttpResponseMessage == _responseMessage);
        }

        [Test]
        public void Ctor_PassesHttpResponseMessage_BodyPropertyIsSet()
        {
            //todo
            //ikke sikker på hvad der skal testes her.
            //makebody bliver kaldt, ved ikke hvordan jeg skal teste, hvad "body sættes til"
        }

        #endregion

        #region HttpResponseFunctions

        [Test]
        public void WasSuccessfull_PassSuccessfullHttpResponse_ReturnsTrue()
        {
            Assert.That(_uut.WasSuccessfull()==true);
        }

        [Test]
        public void WasSuccessfull_PassUnsuccessfullHttpResponse_ReturnsFalse()
        {
            _responseMessage=new HttpResponseMessage(HttpStatusCode.NotFound);
            _uut=new CreateCustomerResponse(_responseMessage);
            Assert.That(_uut.WasSuccessfull()==false);
        }

        [Test]
        public void WasUnsuccessfull_PassSuccessfullHttpRsponse_ReturnsFalse()
        {
            Assert.That(_uut.WasUnsuccessfull()==false);
        }

        [Test]
        public void WasUnsuccessfull_PassBadRequestHttpResponse_ReturnsTrue()
        {
            _responseMessage=new HttpResponseMessage(HttpStatusCode.BadRequest);
            _uut=new CreateCustomerResponse(_responseMessage);
            Assert.That(_uut.WasUnsuccessfull()==true);
        }

        [Test]
        public void WasUnauthorized_PassSuccessfullHttpResponse_ReturnFalse()
        {
            Assert.That(_uut.WasUnauthorized()==false);
        }

        [Test]
        public void WasUnauthorized_PassUnauthorizedHttpResponse_ReturnTrue()
        {
            _responseMessage=new HttpResponseMessage(HttpStatusCode.Unauthorized);
            _uut=new CreateCustomerResponse(_responseMessage);
            Assert.That(_uut.WasUnauthorized()==true);
        }

        #endregion

        #region ErrorFunctions

        //Nedenstående tests er jeg generelt i tvivl om, hvordan vi simulerer.
        //Vi skal have oprettet errors manuelt.

        [Test]
        public void HasErrors_()
        {
            //todo
        }

        [Test]
        public void GetErrors()
        {
            //todo
        }
        #endregion
    }
}
