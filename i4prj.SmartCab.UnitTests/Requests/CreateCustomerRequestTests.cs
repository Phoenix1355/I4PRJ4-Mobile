using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using i4prj.SmartCab.Requests;
using NUnit.Framework;

namespace i4prj.SmartCab.UnitTests.Requests
{
    [TestFixture]
    public class CreateCustomerRequestTests
    {
        #region Setup

        private CreateCustomerRequest _validRequest;
        private CreateCustomerRequest _invalidNameRequest;
        private CreateCustomerRequest _invalidEmailRequest;
        private CreateCustomerRequest _invalidPhoneRequest;
        private CreateCustomerRequest _invalidPasswordRequest;
        private CreateCustomerRequest _invalidPasswordConfirmationRequest;

        [SetUp]
        public void SetUp()
        {
            _validRequest = new CreateCustomerRequest
            {
                Name = "Frank Andersen",
                Email = "frank.andersen@example.com",
                Phone = "12345678",
                Password = "!Ab#_ghuT7",
                PasswordConfirmation = "!Ab#_ghuT7"
            };

            _invalidNameRequest = new CreateCustomerRequest
            {
                Name = "Fr",
                Email = "frank.andersen@example.com",
                Phone = "12345678",
                Password = "!Ab#_ghuT7",
                PasswordConfirmation = "!Ab#_ghuT7"
            };

            _invalidEmailRequest = new CreateCustomerRequest
            {
                Name = "Frank Andersen",
                Email = "frank.andersen@example",
                Phone = "12345678",
                Password = "!Ab#_ghuT7",
                PasswordConfirmation = "!Ab#_ghuT7"
            };

            _invalidPhoneRequest = new CreateCustomerRequest
            {
                Name = "Frank Andersen",
                Email = "frank.andersen@example.com",
                Phone = "12345",
                Password = "!Ab#_ghuT7",
                PasswordConfirmation = "!Ab#_ghuT7"
            };

            _invalidPasswordRequest = new CreateCustomerRequest
            {
                Name = "Frank Andersen",
                Email = "frank.andersen@example.com",
                Phone = "12345678",
                Password = "password",
                PasswordConfirmation = "password"
            };

            _invalidPasswordConfirmationRequest = new CreateCustomerRequest
            {
                Name = "Frank Andersen",
                Email = "frank.andersen@example.com",
                Phone = "12345678",
                Password = "!Ab#_ghuT7",
                PasswordConfirmation = "123456"
            };
        }

        #endregion

        #region Ctor

        [Test]
        public void Ctor_Default_EmptyStringProperties()
        {
            // Arrange and act
            var uut = new CreateCustomerRequest();

            // Assert.
            // Multiple continues even if the first assert inside fails
            Assert.Multiple(() =>
            {
                Assert.AreEqual(uut.Name, string.Empty);
                Assert.AreEqual(uut.Email, string.Empty);
                Assert.AreEqual(uut.Phone, string.Empty);
                Assert.AreEqual(uut.Password, string.Empty);
                Assert.AreEqual(uut.PasswordConfirmation, string.Empty);
            });
        }

        #endregion

        #region Name

        [Test]
        [TestCase(null)] // Required
        [TestCase("A")] // Too short
        [TestCase("Ab")] // Too short
        [TestCase("Frank Andersen Hansen Henning Preben Ibsen Pedersen McKinney Møller Ulriksen Lykke Marianne Zimmermann Henningsen Axelsen Breum Bjerre Falktoft Lund Madsen Breinholdt Henning Preben Ibsen Pedersen McKinney Møller Ulriksen Lykke Marianne Zimmermann Henninge")] // Too long (= 256)
        [TestCase("Frank Andersen Hansen Henning Preben Ibsen Pedersen McKinney Møller Ulriksen Lykke Marianne Zimmermann Henningsen Axelsen Breum Bjerre Falktoft Lund Madsen Breinholdt Henning Preben Ibsen Pedersen McKinney Møller Ulriksen Lykke Marianne Zimmermann Henning Mikkel")] // Too long (> 256)
        public void NameProperty_SetToInvalidValue_IsInvalid(string setValue)
        {
            // Arange
            var uut = new CreateCustomerRequest();

            // Arrange validation
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(uut, null)
            {
                MemberName = "Name"
            };

            // Act
            uut.Name = setValue;
            Validator.TryValidateProperty(uut.Name, validationContext, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
        }

        [Test]
        [TestCase("Frank Andersen")]
        [TestCase("Michael Møller-Hansen")]
        public void NameProperty_SetToValidValue_IsValid(string setValue)
        {
            // Arange
            var uut = new CreateCustomerRequest();

            // Arrange validation
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(uut, null)
            {
                MemberName = "Name"
            };

            // Act
            uut.Name = setValue;
            Validator.TryValidateProperty(uut.Name, validationContext, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count == 0);
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
            // Arange
            var uut = new CreateCustomerRequest();

            // Arrange validation
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(uut, null)
            {
                MemberName = "Email"
            };

            // Act
            uut.Email = setValue;
            Validator.TryValidateProperty(uut.Email, validationContext, validationResults);

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
            // Arange
            var uut = new CreateCustomerRequest();

            // Arrange validation
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(uut, null)
            {
                MemberName = "Email"
            };

            // Act
            uut.Email = setValue;
            Validator.TryValidateProperty(uut.Email, validationContext, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count == 0);
        }

        #endregion

        #region Phone

        [Test]
        [TestCase(null)] // Required
        [TestCase("1")] // Too short
        [TestCase("6543")] // Too short
        [TestCase("1234567")] // Too short
        [TestCase("123456789")] // Too long
        [TestCase("956478987405")] // Too long
        [TestCase("15896577987247")] // Too long
        [TestCase("01234567")] // Starts with zero
        public void PhoneProperty_SetToInvalidValue_IsInvalid(string setValue)
        {
            // Arange
            var uut = new CreateCustomerRequest();

            // Arrange validation
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(uut, null)
            {
                MemberName = "Phone"
            };

            // Act
            uut.Phone = setValue;
            Validator.TryValidateProperty(uut.Phone, validationContext, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
        }

        [Test]
        [TestCase("86573165")]
        [TestCase("27293217")]
        [TestCase("55555555")]
        public void PhoneProperty_SetToValidValue_IsValid(string setValue)
        {
            // Arange
            var uut = new CreateCustomerRequest();

            // Arrange validation
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(uut, null)
            {
                MemberName = "Phone"
            };

            // Act
            uut.Phone = setValue;
            Validator.TryValidateProperty(uut.Phone, validationContext, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count == 0);
        }

        #endregion

        #region Password

        [Test]
        // Required
        [TestCase(null)]
        // Too short
        [TestCase("1!#Ac")] 
        [TestCase("GGb)a9")]
        [TestCase("tYYu6&")]
        // Missing integers
        [TestCase("HtrE%YohkJ")]
        [TestCase("TpNLgf)(f")]
        [TestCase("wQQ__vLiKK")]
        // Missing special characters
        [TestCase("Agj39Kg74")]
        [TestCase("qqlgohGR8RR3")]
        [TestCase("64GgHhlke23")]
        // Missing capital letters
        [TestCase("!ghfj46_gj")]
        [TestCase("4o_#gght_!")]
        [TestCase("ngh)&gj_34_3")]
        public void PasswordProperty_SetToInvalidValue_IsInvalid(string setValue)
        {
            // Arange
            var uut = new CreateCustomerRequest();

            // Arrange validation
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(uut, null)
            {
                MemberName = "Password"
            };

            // Act
            uut.Password = setValue;
            Validator.TryValidateProperty(uut.Password, validationContext, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
        }

        [Test]
        [TestCase("Gg2G_59i")]
        [TestCase("aA#gi!G9")]
        [TestCase("9(GFghtTOP")]
        [TestCase("?gjTiho4_&")]
        [TestCase("+gjTT85:12")]
        [TestCase("Otug§ui9§")]
        public void PasswordProperty_SetToValidValue_IsValid(string setValue)
        {
            // Arange
            var uut = new CreateCustomerRequest();

            // Arrange validation
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(uut, null)
            {
                MemberName = "Password"
            };

            // Act
            uut.Password = setValue;
            Validator.TryValidateProperty(uut.Password, validationContext, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count == 0);
        }

        #endregion

        #region PasswordConfirmation

        [Test]
        public void PasswordConfirmationProperty_SetToNull_IsInvalid()
        {
            // Arange
            var uut = new CreateCustomerRequest();

            // Arrange validation
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(uut, null)
            {
                MemberName = "PasswordConfirmation"
            };

            // Act
            uut.PasswordConfirmation = null;
            Validator.TryValidateProperty(uut.PasswordConfirmation, validationContext, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
        }

        [Test]
        [TestCase("fgh€&_34!A")]
        public void PasswordConfirmationProperty_SetToValueOfPassword_IsValid(string passwordValue)
        {
            // Arange
            var uut = new CreateCustomerRequest();

            // Arrange validation
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(uut, null)
            {
                MemberName = "PasswordConfirmation"
            };

            // Act
            uut.Password = passwordValue;
            uut.PasswordConfirmation = passwordValue;
            Validator.TryValidateProperty(uut.PasswordConfirmation, validationContext, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count == 0);
        }

        [TestCase("!fgjG_67wpgG", "AB34_g%5sdg")]
        [TestCase("!fhjit€19", "!fhjit€19A")]
        [TestCase("!fhjit€19", "!fhjit€1")]
        public void PasswordConfirmationProperty_SetToValueOtherThanPassword_IsInvalid(string passwordValue, string passwordConfirmationValue)
        {
            // Arange
            var uut = new CreateCustomerRequest();

            // Arrange validation
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(uut, null)
            {
                MemberName = "PasswordConfirmation"
            };

            // Act
            uut.Password = passwordValue;
            uut.PasswordConfirmation = passwordConfirmationValue;
            Validator.TryValidateProperty(uut.PasswordConfirmation, validationContext, validationResults);

            // Assert
            Assert.IsTrue(validationResults.Count > 0);
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
        public void IsValidProperty_RequestHasInvalidName_ReturnsFalse()
        {
            // Arange and act
            var uut = _invalidNameRequest;

            // Assert
            Assert.IsFalse(uut.IsValid);
        }

        [Test]
        public void IsValidProperty_RequestHasInvalidEmail_ReturnsFalse()
        {
            // Arange and act
            var uut = _invalidEmailRequest;

            // Assert
            Assert.IsFalse(uut.IsValid);
        }

        [Test]
        public void IsValidProperty_RequestHasInvalidPhone_ReturnsFalse()
        {
            // Arange and act
            var uut = _invalidPhoneRequest;

            // Assert
            Assert.IsFalse(uut.IsValid);
        }

        [Test]
        public void IsValidProperty_RequestHasInvalidPassword_ReturnsFalse()
        {
            // Arange and act
            var uut = _invalidPasswordRequest;

            // Assert
            Assert.IsFalse(uut.IsValid);
        }

        [Test]
        public void IsValidProperty_RequestHasInvalidPasswordConfirmation_ReturnsFalse()
        {
            // Arange and act
            var uut = _invalidPasswordConfirmationRequest;

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
        public void IsInvalidProperty_RequestHasInvalidName_ReturnsTrue()
        {
            // Arange and act
            var uut = _invalidNameRequest;

            // Assert
            Assert.IsTrue(uut.IsInvalid);
        }

        [Test]
        public void IsInvalidProperty_RequestHasInvalidEmail_ReturnsTrue()
        {
            // Arange and act
            var uut = _invalidEmailRequest;

            // Assert
            Assert.IsTrue(uut.IsInvalid);
        }

        [Test]
        public void IsInvalidProperty_RequestHasInvalidPhone_ReturnsTrue()
        {
            // Arange and act
            var uut = _invalidPhoneRequest;

            // Assert
            Assert.IsTrue(uut.IsInvalid);
        }

        [Test]
        public void IsInvalidProperty_RequestHasInvalidPassword_ReturnsTrue()
        {
            // Arange and act
            var uut = _invalidPasswordRequest;

            // Assert
            Assert.IsTrue(uut.IsInvalid);
        }

        [Test]
        public void IsInvalidProperty_RequestHasInvalidPasswordConfirmation_ReturnsTrue()
        {
            // Arange and act
            var uut = _invalidPasswordConfirmationRequest;

            // Assert
            Assert.IsTrue(uut.IsInvalid);
        }

        #endregion
    }
}
