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
        private ICustomer uut;
        private LoginResponseBody.Customer loginObject;
        private CreateCustomerResponseBody.Customer createCustomerObject;

        #region ctor
    
        [Test]
        public void CreateCustomerResponseCtor_PassObjectThatIsNotNull_UutEqualsParameterObject()
        {
            createCustomerObject=new CreateCustomerResponseBody.Customer();
            createCustomerObject.email = "test@tester.com";
            createCustomerObject.name = "Test Subject";
            createCustomerObject.phoneNumber = "99887766";

            uut=new Customer(createCustomerObject);

            Assert.Multiple(new TestDelegate(CreateCustomerResponseCtorAssertions));
        }

        private void CreateCustomerResponseCtorAssertions()
        {
            Assert.AreEqual(uut.Email,createCustomerObject.email);
            Assert.AreEqual(uut.Name, createCustomerObject.name);
            Assert.AreEqual(uut.PhoneNumber, createCustomerObject.phoneNumber);
        }

        [Test]
        public void LoginResponseCtor_PassObjectThatIsNotNull_UutEqualsParameterObject()
        {
            loginObject = new LoginResponseBody.Customer();
            loginObject.email = "test@tester.com";
            loginObject.name = "Test Subject";
            loginObject.phoneNumber = "99887766";

            uut = new Customer(loginObject);

            Assert.Multiple(new TestDelegate(LoginResponseCtorAssertions));
        }

        private void LoginResponseCtorAssertions()
        {
            Assert.AreEqual(uut.Email, loginObject.email);
            Assert.AreEqual(uut.Name, loginObject.name);
            Assert.AreEqual(uut.PhoneNumber, loginObject.phoneNumber);
        }

        #endregion
    }
}
