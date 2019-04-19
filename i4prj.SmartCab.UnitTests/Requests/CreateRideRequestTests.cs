using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using i4prj.SmartCab.Requests;
using NUnit.Framework;

namespace i4prj.SmartCab.UnitTests.Requests
{
    public class CreateRideRequestTests
    {
        private CreateRideRequest _uut;


        [SetUp]
        public void SetUp()
        {
            _uut=new CreateRideRequest();
        }

        #region Ctor

        [Test]
        public void Ctor_PerformsSetup_PropertiesAreSetWithDefaultValues()
        {
            Assert.Multiple(() =>
            {
                Assert.AreEqual(_uut.AmountOfPassengers,1);
                Assert.AreEqual(_uut.OriginCityName,string.Empty);
                Assert.AreEqual(_uut.OriginPostalCode, string.Empty);
                Assert.AreEqual(_uut.OriginStreetName, string.Empty);
                Assert.AreEqual(_uut.OriginStreetNumber, string.Empty);
                Assert.AreEqual(_uut.DestinationCityName, string.Empty);
                Assert.AreEqual(_uut.DestinationPostalCode, string.Empty);
                Assert.AreEqual(_uut.DestinationStreetName, string.Empty);
                Assert.AreEqual(_uut.DestinationStreetNumber, string.Empty);
                

                //TODO
                /*
                Assert.AreEqual(_uut.DepartureDate,DateTime.Now);
                Assert.AreEqual(_uut.ConfirmationDeadlineDate,DateTime.Now);
                Assert.AreEqual(_uut.DepartureTime, DateTime.Now.TimeOfDay.Add(new TimeSpan(1, 0, 0)));
                Assert.AreEqual(_uut.ConfirmationDeadlineTime, DateTime.Now.TimeOfDay.Add(new TimeSpan(0, 30, 0)));
                Assert.AreEqual(_uut.CurrentTime,DateTime.Now);
                */
            });
        }

        #endregion

        #region IsShared

        //no tests

        #endregion

        #region DepartureDate
        #endregion

        #region DepartureTime
        #endregion

        #region ConfirmationDeadlineDate
        #endregion

        #region ConfirmationDeadlineTime
        #endregion

        #region AmountOfPassengers
        [Test]
        [TestCase(null)]
        [TestCase(0)]
        [TestCase(5)]
        public void AmountOfPassengers_SetToInvalidValue_IsInvalid(double setValue)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(_uut, null)
            {
                MemberName = "AmountOfPassengers"
            };

            _uut.AmountOfPassengers = setValue;

            Validator.TryValidateProperty(_uut.AmountOfPassengers, validationContext, validationResults);

            Assert.IsTrue(validationResults.Count>0);
        }

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void AmountOfPassengers_SetToValidValue_IsValid(double setValue)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(_uut, null)
            {
                MemberName = "AmountOfPassengers"
            };

            _uut.AmountOfPassengers = setValue;

            Validator.TryValidateProperty(_uut.AmountOfPassengers, validationContext, validationResults);

            Assert.IsTrue(validationResults.Count == 0);
        }

        #endregion

        #region OriginCityName

        [Test]
        [TestCase("bomhuset ved emdrupsveien")] //Long but not too long (this city exists, yes)
        [TestCase("Ry")] //Short but not too short
        [TestCase("Grenå")] //Contains Å
        public void OriginCityName_SetToValidValue_IsValid(string setValue)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(_uut, null)
            {
                MemberName = "OriginCityName"
            };

            _uut.OriginCityName = setValue;

            Validator.TryValidateProperty(_uut.OriginCityName, validationContext, validationResults);

            Assert.IsTrue(validationResults.Count == 0);
        }

        [Test]
        [TestCase(null)] //required
        [TestCase("")] //required
        [TestCase("bomhuset ved emdrupsveiens")] //too long
        [TestCase("A")] //too short
        [TestCase("Grenå+!?")] //contains special characters
        public void OriginCityName_SetToInvalidValue_IsInValid(string setValue)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(_uut, null)
            {
                MemberName = "OriginCityName"
            };

            _uut.OriginCityName = setValue;

            Validator.TryValidateProperty(_uut.OriginCityName, validationContext, validationResults);

            Assert.IsTrue(validationResults.Count > 0);
        }

        #endregion

        #region DestinationCityName

        //Has exactly the same test scenarios with same outcome as "OriginCityName"
        //Would argue that test is not needed

        #endregion

        #region OriginPostalCode

        [Test]
        [TestCase("1234")] //4 numbers
        [TestCase("5678")] //4 other numbers
        [TestCase("9000")] //The last numbers
        public void OriginPostalCode_SetValidValues_IsValid(string setValue)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(_uut, null)
            {
                MemberName = "OriginPostalCode"
            };

            _uut.OriginPostalCode = setValue;

            Validator.TryValidateProperty(_uut.OriginPostalCode, validationContext, validationResults);

            Assert.AreEqual(validationResults.Count,0);
        }

        [Test]
        [TestCase(null)] //cant be null
        [TestCase("")] //cant be empty
        [TestCase("123")] //too short 
        [TestCase("12345")] //too long
        [TestCase("123+")] //contains special character
        [TestCase("123 4")] //contains spaces
        public void OriginPostalCode_SetInValidValues_IsInValid(string setValue)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(_uut, null)
            {
                MemberName = "OriginPostalCode"
            };

            _uut.OriginPostalCode = setValue;

            Validator.TryValidateProperty(_uut.OriginPostalCode, validationContext, validationResults);

            Assert.IsTrue(validationResults.Count > 0);
        }

        #endregion

        #region DestinationPostalCode
        //same as origin
        #endregion

        #region OriginStreetName
        [Test]
        [TestCase(null)] //cant be null
        [TestCase("")] //cant be empty
        [TestCase("123")] //too short 
        [TestCase("12345")] //too long
        [TestCase("123+")] //contains special character
        [TestCase("123 4")] //contains spaces
        public void OriginStreetName_SetValidValues_IsValid(string setValue)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(_uut, null)
            {
                MemberName = "OriginStreetName"
            };

            _uut.OriginStreetName = setValue;

            Validator.TryValidateProperty(_uut.OriginStreetName, validationContext, validationResults);

            Assert.IsTrue(validationResults.Count > 0);
        }
        #endregion

        #region DestinationStreetName
        //same as origin
        #endregion

        #region OriginStreetNumer
        #endregion

        #region DestinationStreetNumber
        //same as origin
        #endregion

        #region IsValid
        #endregion

        #region IsInvalid
        #endregion
    }
}
