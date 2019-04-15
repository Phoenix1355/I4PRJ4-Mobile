using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using i4prj.SmartCab.Requests;
using NUnit.Framework;

namespace i4prj.SmartCab.UnitTests.Requests
{
    [TestFixture]
    public class LoginRequestTests
    {
        #region Setup

        private LoginRequest _validRequest;
        private LoginRequest _invalidEmailRequest;
        private LoginRequest _uut;

        [SetUp]
        public void SetUp()
        {
            _validRequest = new LoginRequest()
            {
                Email = "frank.andersen@example.com",
                Password = "!Ab#_ghuT7"
            };

            _invalidEmailRequest = new LoginRequest()
            {
                Email = "frank.andersen@example",
                Password = "!Ab#_ghuT7"
            };

            _uut = new LoginRequest();
        }

        #endregion

        #region Email

        // Test cases from: https://blogs.msdn.microsoft.com/testing123/2009/02/06/email-address-test-cases/
        [Test]
        [TestCase(null)] // Required
        [TestCase("plainaddress")]
        [TestCase("#@%^%#$@#$@#.com")]
        [TestCase("@domain.com")]
        [TestCase("Joe Smith <email @domain.com>")]
        [TestCase("email@domain @domain.com")]
        [TestCase(".email @domain.com")]
        [TestCase("email.@domain.com")]
        [TestCase("email..email @domain.com")]
        [TestCase("あいうえお@domain.com")]
        [TestCase("email@domain.com (Joe Smith)")]
        [TestCase("email@domain")]
        [TestCase("email@-domain.com")]
        //[TestCase("email@domain.web")]
        //[TestCase("email@111.222.333.44444")]
        [TestCase("email @domain..com")]
        public void EmailProperty_SetToInvalidValue_IsInvalid(string setValue)
        {
            // Arrange validation
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(_uut, null)
            {
                MemberName = "Email"
            };

            // Act
            _uut.Email = setValue;
            Validator.TryValidateProperty(_uut.Email, validationContext, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
        }

        // Test cases from: https://blogs.msdn.microsoft.com/testing123/2009/02/06/email-address-test-cases/
        [Test]
        [TestCase("email@domain.com")]
        [TestCase("firstname.lastname@domain.com")]
        [TestCase("email@subdomain.domain.com")]
        [TestCase("firstname+lastname@domain.com")]
        [TestCase("email@123.123.123.123")]
        [TestCase("email@[123.123.123.123]")]
        [TestCase("\"email\"@domain.com")]
        [TestCase("1234567890@domain.com")]
        [TestCase("email@domain-one.com")]
        //[TestCase("_______@domain.com")]
        [TestCase("email@domain.name")]
        [TestCase("email@domain.co.jp")]
        [TestCase("firstname-lastname@domain.com")]
        public void EmailProperty_SetToValidValue_IsValid(string setValue)
        {
            // Arrange validation
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(_uut, null)
            {
                MemberName = "Email"
            };

            // Act
            _uut.Email = setValue;
            Validator.TryValidateProperty(_uut.Email, validationContext, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count == 0);
        }

        #endregion

        #region IsValid

        [Test]
        public void IsValidProperty_RequestHasOnlyValidValues_ReturnsTrue()
        {
            // Arange and act
            var uut = _validRequest;

            // Assert
            Assert.IsTrue(uut.IsValid);
        }

        [Test]
        public void IsValidProperty_RequestHasInvalidEmail_ReturnsFalse()
        {
            // Arange and act
            var uut = _invalidEmailRequest;

            // Assert
            Assert.IsFalse(uut.IsValid);
        }

        #endregion

        #region IsInvalid

        [Test]
        public void IsInvalidProperty_RequestHasOnlyValidValues_ReturnsFalse()
        {
            // Arange and act
            var uut = _validRequest;

            // Assert
            Assert.IsFalse(uut.IsInvalid);
        }

        [Test]
        public void IsInvalidProperty_RequestHasInvalidEmail_ReturnsTrue()
        {
            // Arange and act
            var uut = _invalidEmailRequest;

            // Assert
            Assert.IsTrue(uut.IsInvalid);
        }

        #endregion

        #region EmailErrors
        [Test]
        public void EmailErrorsProperty_RequestHasInvalidEmail_IsNotEmpty()
        {
            // Arange and act
            var uut = _invalidEmailRequest;

            // Assert
            Assert.IsNotEmpty(uut.EmailErrors);
        }

        [Test]
        public void EmailErrorsProperty_RequestHasValidEmail_IsEmpty()
        {
            // Arange and act
            var uut = _validRequest;

            // Assert
            Assert.IsEmpty(uut.EmailErrors);
        }

        #endregion

        #region EmailHasErrors
        [Test]
        public void EmailHasErrorsProperty_RequestHasInvalidEmail_ReturnsTrue()
        {
            // Arange and act
            var uut = _invalidEmailRequest;

            // Assert
            Assert.IsTrue(uut.EmailHasErrors);
        }

        [Test]
        public void EmailHasErrorsProperty_RequestHasValidEmail_ReturnsFalse()
        {
            // Arange and act
            var uut = _validRequest;

            // Assert
            Assert.IsFalse(uut.EmailHasErrors);
        }
        #endregion
    }
}
