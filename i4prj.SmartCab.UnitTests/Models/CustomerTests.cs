using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Models;
using i4prj.SmartCab.Responses;
using NSubstitute;
using NUnit.Framework;

namespace i4prj.SmartCab.UnitTests.Models
{
    [TestFixture]
    public class CustomerTests
    {
        [Test]
        [TestCase("Frank Andersen", "31133165", "frank.andersen@gmail.com")]
        public void Ctor_WithResponseFromCreateCustomerRequest_HasTheRightProperties(string name, string phoneNumber, string email)
        {
            // Arrange
            var customerFromResponseBody = Substitute.For<IApiResponseCustomer>();
            customerFromResponseBody.name.Returns(name);
            customerFromResponseBody.phoneNumber.Returns(phoneNumber);
            customerFromResponseBody.email.Returns(email);

            // Act
            var uut = new Customer(customerFromResponseBody);

            // Assert
            Assert.That(uut.Name, Is.EqualTo(name));
            Assert.That(uut.PhoneNumber, Is.EqualTo(phoneNumber));
            Assert.That(uut.Email, Is.EqualTo(email));
        }
    }
}