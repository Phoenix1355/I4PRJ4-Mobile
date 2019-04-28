using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using i4prj.SmartCab.Requests;
using NUnit.Framework;

namespace i4prj.SmartCab.UnitTests.Requests
{
    public class EditAccountRequestTests
    {
        #region SetUp
        private EditAccountRequest _uut;

        [SetUp]
        public void SetUp()
        {
            _uut=new EditAccountRequest();
        }
        #endregion

        #region Ctor

        [Test]
        public void Ctor_RequestInstantiated_PropertiesAreSet()
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual(_uut.Name,"");
                Assert.AreEqual(_uut.PhoneNumber,"");
                Assert.AreEqual(_uut.Email,"");
                Assert.AreEqual(_uut.ChangePassword,false);
            });
        }
        #endregion

        #region Name

        [Test]
        [TestCase("Ida")]
        [TestCase("Jesper Strøm")]
        public void Name_SetToValidValue_IsValid(string setValue)
        {
            var validationResults = new List<ValidationResult>();

            var validationContext = new ValidationContext(_uut, null)
            {
                MemberName = "Name"
            };

            _uut.Name = setValue;

            Validator.TryValidateProperty(_uut.Name, validationContext, validationResults);

            Assert.IsTrue(validationResults.Count == 0);
        }

        [Test]
        [TestCase("")]
        [TestCase("Ib")]
        public void Name_SetToValidValue_IsInvalid(string setValue)
        {
            var validationResults = new List<ValidationResult>();

            var validationContext = new ValidationContext(_uut, null)
            {
                MemberName = "Name"
            };

            _uut.Name = setValue;

            Validator.TryValidateProperty(_uut.Name, validationContext, validationResults);

            Assert.IsTrue(validationResults.Count >0);
        }
        #endregion

        #region PhoneNumber
        [Test]
        [TestCase("12345678")]
        [TestCase("90123456")]
        public void PhoneNumber_SetToValidValue_IsValid(string setValue)
        {
            var validationResults = new List<ValidationResult>();

            var validationContext = new ValidationContext(_uut, null)
            {
                MemberName = "PhoneNumber"
            };

            _uut.PhoneNumber = setValue;

            Validator.TryValidateProperty(_uut.PhoneNumber, validationContext, validationResults);

            Assert.IsTrue(validationResults.Count == 0);
        }

        [Test]
        [TestCase("1234567")]
        [TestCase("")]
        public void PhoneNumber_SetToInvalidValue_IsInvalid(string setValue)
        {
            var validationResults = new List<ValidationResult>();

            var validationContext = new ValidationContext(_uut, null)
            {
                MemberName = "PhoneNumber"
            };

            _uut.PhoneNumber = setValue;

            Validator.TryValidateProperty(_uut.PhoneNumber, validationContext, validationResults);

            Assert.IsTrue(validationResults.Count > 0);
        }


        #endregion

        #region Email
        [Test]
        [TestCase("repsej.stroem@gmail.com")]
        [TestCase("tester@tester.com")]
        public void Email_SetToValidValue_IsValid(string setValue)
        {
            var validationResults = new List<ValidationResult>();

            var validationContext = new ValidationContext(_uut, null)
            {
                MemberName = "Email"
            };

            _uut.Email = setValue;

            Validator.TryValidateProperty(_uut.Email, validationContext, validationResults);

            Assert.IsTrue(validationResults.Count == 0);
        }

        [Test]
        [TestCase("")]
        [TestCase("tester@tester")]
        public void Email_SetToInvalidValue_IsInvalid(string setValue)
        {
            var validationResults = new List<ValidationResult>();

            var validationContext = new ValidationContext(_uut, null)
            {
                MemberName = "Email"
            };

            _uut.Email = setValue;

            Validator.TryValidateProperty(_uut.Email, validationContext, validationResults);

            Assert.IsTrue(validationResults.Count > 0);
        }

        #endregion

        #region ChangePassword
        //No tests needed
        #endregion

        #region OldPassword
        [Test]
        [TestCase("Test123!",true)]
        [TestCase("",false)]
        public void OldPassword_SetToValidValue_IsValid(string setValue,bool requiredIf)
        {
            var validationResults = new List<ValidationResult>();

            var validationContext = new ValidationContext(_uut, null)
            {
                MemberName = "OldPassword"
            };

        
            _uut.ChangePassword = requiredIf;
            _uut.OldPassword = setValue;

            Validator.TryValidateProperty(_uut.OldPassword, validationContext, validationResults);

            Assert.AreEqual(validationResults.Count , 0);
        }

        [Test]
        [TestCase("", true)]
        [TestCase("Test123", true)]
        [TestCase("123456!!", true)]
        public void OldPassword_SetToInvalidValue_IsInvalid(string setValue, bool requiredIf)
        {
            var validationResults = new List<ValidationResult>();

            var validationContext = new ValidationContext(_uut, null)
            {
                MemberName = "OldPassword"
            };


            _uut.ChangePassword = requiredIf;
            _uut.OldPassword = setValue;

            Validator.TryValidateProperty(_uut.OldPassword, validationContext, validationResults);

            Assert.IsTrue(validationResults.Count>0);
        }


        #endregion

        #region Password
        [Test]
        [TestCase("Test123!","Test123", true)]
        [TestCase("","Test123!", false)]
        public void Password_SetToValidValue_IsValid(string setValue,string valueOfOldPassword, bool requiredIf)
        {
            var validationResults = new List<ValidationResult>();

            var validationContext = new ValidationContext(_uut, null)
            {
                MemberName = "Password"
            };


            _uut.ChangePassword = requiredIf;
            _uut.OldPassword = valueOfOldPassword;
            _uut.Password = setValue;

            Validator.TryValidateProperty(_uut.Password, validationContext, validationResults);

            Assert.AreEqual(validationResults.Count, 0);
        }

        [Test]
        [TestCase("Test123", "Test123", true)]
        [TestCase("", "Test123!", true)]
        public void Password_SetToInvalidValue_IsInvalid(string setValue, string valueOfOldPassword, bool requiredIf)
        {
            var validationResults = new List<ValidationResult>();

            var validationContext = new ValidationContext(_uut, null)
            {
                MemberName = "Password"
            };


            _uut.ChangePassword = requiredIf;
            _uut.OldPassword = valueOfOldPassword;
            _uut.Password = setValue;

            Validator.TryValidateProperty(_uut.Password, validationContext, validationResults);

            Assert.IsTrue(validationResults.Count>0);
        }
        #endregion

        #region RepeatedPassword
        [Test]
        [TestCase("Test123!","Test123!", true)]
        [TestCase("","", false)]
        public void RepeatedPassword_SetToValidValue_IsValid(string setValue, string valueOfNewPassword, bool requiredIf)
        {
            var validationResults = new List<ValidationResult>();

            var validationContext = new ValidationContext(_uut, null)
            {
                MemberName = "Password"
            };


            _uut.ChangePassword = requiredIf;
            _uut.Password = valueOfNewPassword;
            _uut.RepeatedPassword = setValue;

            Validator.TryValidateProperty(_uut.Password, validationContext, validationResults);

            Assert.IsTrue(validationResults.Count== 0);
        }

        [Test]
        [TestCase("Test123!", "Test123", true)]
        [TestCase("", "", true)]
        public void RepeatedPassword_SetToInvalidValue_IsInvalid(string setValue, string valueOfNewPassword, bool requiredIf)
        {
            var validationResults = new List<ValidationResult>();

            var validationContext = new ValidationContext(_uut, null)
            {
                MemberName = "Password"
            };


            _uut.ChangePassword = requiredIf;
            _uut.Password = valueOfNewPassword;
            _uut.RepeatedPassword = setValue;

            Validator.TryValidateProperty(_uut.Password, validationContext, validationResults);

            Assert.IsTrue(validationResults.Count >0);
        }
        #endregion

    }
}
