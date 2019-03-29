using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Models;
using i4prj.SmartCab.Responses;
using NUnit.Framework;

namespace i4prj.SmartCab.UnitTests.Models
{
    [TestFixture]
    public class CustomerTests
    {
        private ICustomer _uut;
        private LoginResponseBody.Customer _loginObject;
        private CreateCustomerResponseBody.Customer _createCustomerObject;

        #region ctor

        [SetUp]
        public void SetUp()
        {
            _loginObject = new LoginResponseBody.Customer()
            {
                email = "test@tester.com",
                name = "Test Subject",
                phoneNumber = "99887766",
            };

            _createCustomerObject = new CreateCustomerResponseBody.Customer()
            {
                email = "test@tester.com",
                name = "Test Subject",
                phoneNumber = "99887766",
            };
        }

        [Test]
        public void CreateCustomerResponseCtor_PassObjectThatIsNotNull_UutEqualsParameterObject()
        {
            _uut=new Customer(_createCustomerObject);

            Assert.Multiple(new TestDelegate(CreateCustomerResponseCtorAssertions));
        }

        private void CreateCustomerResponseCtorAssertions()
        {
            Assert.AreEqual(_uut.Email,_createCustomerObject.email);
            Assert.AreEqual(_uut.Name, _createCustomerObject.name);
            Assert.AreEqual(_uut.PhoneNumber, _createCustomerObject.phoneNumber);
        }

        [Test]
        public void LoginResponseCtor_PassObjectThatIsNotNull_UutEqualsParameterObject()
        {
            _uut = new Customer(_loginObject);

            Assert.Multiple(new TestDelegate(LoginResponseCtorAssertions));
        }

        private void LoginResponseCtorAssertions()
        {
            Assert.AreEqual(_uut.Email, _loginObject.email);
            Assert.AreEqual(_uut.Name, _loginObject.name);
            Assert.AreEqual(_uut.PhoneNumber, _loginObject.phoneNumber);
        }

        #endregion
    }
}
