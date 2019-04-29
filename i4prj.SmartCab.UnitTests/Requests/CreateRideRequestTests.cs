using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DryIoc;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Requests;
using i4prj.SmartCab.ViewModels;
using i4prj.SmartCab.Views;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace i4prj.SmartCab.UnitTests.Requests
{
    public class CreateRideRequestTests
    {
        private CreateRideRequest _uut;
        private ITimeService _fakeTimeService;

        private DateTime _validDateTime;
        private DateTime _invalidDateTime;

        [SetUp]
        public void SetUp()
        {

            _fakeTimeService = Substitute.For<ITimeService>();
            _fakeTimeService.GetCurrentDate().Returns(new DateTime(2020, 1, 1));
            _fakeTimeService.GetCurrentTime().Returns(new TimeSpan(0, 12, 0, 0));
            _uut=new CreateRideRequest(_fakeTimeService);
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
                
                Assert.AreEqual(_uut.DepartureDate,_fakeTimeService.GetCurrentDate());
                Assert.AreEqual(_uut.ConfirmationDeadlineDate,_fakeTimeService.GetCurrentDate());
                Assert.AreEqual(_uut.DepartureTime, _fakeTimeService.GetCurrentTime().Add(new TimeSpan(1, 0, 0)));
                Assert.AreEqual(_uut.ConfirmationDeadlineTime, _fakeTimeService.GetCurrentTime().Add(new TimeSpan(0, 30, 0)));
                Assert.AreEqual(_uut.CurrentDate,_fakeTimeService.GetCurrentDate());
            });
        }

        #endregion

        #region IsShared

        //no tests

        #endregion

        #region DepartureDate

        [Test]
        public void DepartureDate_SetValidValue_ValueIsAccepted()
        {

            _uut.DepartureDate = _fakeTimeService.GetCurrentDate().AddDays(1);

            Assert.AreEqual(_uut.DepartureDate,new DateTime(2020,1,2));
        }

        //invalid value not possible as it stands
        //DateTime objects cant be null, and databinding makes it so the user cant change it to before DateTime.Now.

        #endregion

        //As it is right now, this is not testable
        #region DepartureTime
        /*
        [Test]
        public void DepartureTime_SetLaterThanConfirmationWithSameDate_ValueAccepted()
        {
            _uut.ConfirmationDeadlineDate = _fakeTimeService.GetCurrentDate();
            _uut.ConfirmationDeadlineTime = _fakeTimeService.GetCurrentTime();

            _uut.DepartureDate = _fakeTimeService.GetCurrentDate();
            _uut.DepartureTime = _fakeTimeService.GetCurrentTime().Add(new TimeSpan(0, 1, 0, 0));


            Assert.AreEqual(_uut.DepartureTime, _fakeTimeService.GetCurrentTime().Add(new TimeSpan(0, 1, 0, 0)));
        }

        [Test]
        public void DepartureTime_SetLaterThanConfirmationWithDifferentDate_ValueAccepted()
        {
            _uut.ConfirmationDeadlineDate = _fakeTimeService.GetCurrentDate();
            _uut.ConfirmationDeadlineTime = _fakeTimeService.GetCurrentTime();

            _uut.DepartureDate = _fakeTimeService.GetCurrentDate().AddDays(1);
            _uut.DepartureTime = _fakeTimeService.GetCurrentTime().Add(new TimeSpan(0, 1, 0, 0));

            Assert.AreEqual(_uut.DepartureTime, _fakeTimeService.GetCurrentTime().Add(new TimeSpan(0, 1, 0, 0)));
        }

        [Test]
        public void DepartureTime_SetEarlierThanConfirmation_ValueNotAccepted()
        {
            _uut.ConfirmationDeadlineDate = _fakeTimeService.GetCurrentDate();
            _uut.ConfirmationDeadlineTime = _fakeTimeService.GetCurrentTime();

            _uut.DepartureDate = _fakeTimeService.GetCurrentDate();
            _uut.DepartureTime = _fakeTimeService.GetCurrentTime().Subtract(new TimeSpan(0, 1, 0, 0));

            Assert.AreEqual(_uut.DepartureTime,_uut.ConfirmationDeadlineTime);
        }
        */
        #endregion


        #region ConfirmationDeadlineDate
        [Test]
        public void ConfirmationDeadlineDate_SetValidValue_ValueIsAccepted()
        {
            _uut.ConfirmationDeadlineDate = _fakeTimeService.GetCurrentDate().AddDays(1);

            Assert.AreEqual(_uut.DepartureDate,new DateTime(2020,1,1));
        }

        //invalid value not possible as it stands
        //DateTime objects cant be null, and databinding makes it so the user cant change it to before DateTime.Now.
        #endregion

        //As it is right now, this is not testable
        #region ConfirmationDeadlineTime
        /*
        [Test]
        public void ConfirmationDeadlineTime_SetEarlierThanDepartureWithSameDate_ValueAccepted()
        {
            _uut.DepartureDate = _fakeTimeService.GetCurrentDate();
            _uut.DepartureTime = _fakeTimeService.GetCurrentTime();

            _uut.ConfirmationDeadlineDate = _fakeTimeService.GetCurrentDate();
            _uut.ConfirmationDeadlineTime = _fakeTimeService.GetCurrentTime().Subtract(new TimeSpan(0, 1, 0, 0));

            Assert.AreEqual(_uut.ConfirmationDeadlineTime,
                _fakeTimeService.GetCurrentTime().Subtract(new TimeSpan(0, 1, 0, 0)));
        }

        [Test]
        public void ConfirmationDeadlineTime_SetEarlierThanDepartureWithDifferentDate_ValueAccepted()
        {
            _uut.DepartureDate = _fakeTimeService.GetCurrentDate().AddDays(1);
            _uut.DepartureTime = _fakeTimeService.GetCurrentTime();

            _uut.ConfirmationDeadlineDate = _fakeTimeService.GetCurrentDate();
            _uut.ConfirmationDeadlineTime = _fakeTimeService.GetCurrentTime().Subtract(new TimeSpan(0, 1, 0, 0));


            Assert.AreEqual(_uut.ConfirmationDeadlineTime, _fakeTimeService.GetCurrentTime().Subtract(new TimeSpan(0, 1, 0, 0)));
        }

        [Test]
        public void ConfirmationDeadlineTime_SetLaterThanDeparture_ValueNotAccepted()
        {
            _uut.DepartureDate = _fakeTimeService.GetCurrentDate();
            _uut.DepartureTime = _fakeTimeService.GetCurrentTime();

            _uut.ConfirmationDeadlineDate = _fakeTimeService.GetCurrentDate();
            _uut.ConfirmationDeadlineTime = _fakeTimeService.GetCurrentTime().Add(new TimeSpan(0, 1, 0, 0));

            Assert.AreEqual(_uut.ConfirmationDeadlineTime,_uut.DepartureTime);
        }
        */
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
        [TestCase("Ålebakken")] //Contains Å
        [TestCase("Eg")] //2 letters
        [TestCase("Testvejmeddetretlangenavn")] //25 letters
        [TestCase("Hunde vej")] //contains spaces
        public void OriginStreetName_SetValidValues_IsValid(string setValue)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(_uut, null)
            {
                MemberName = "OriginStreetName"
            };

            _uut.OriginStreetName = setValue;

            Validator.TryValidateProperty(_uut.OriginStreetName, validationContext, validationResults);

            Assert.IsTrue(validationResults.Count == 0);
        }

        [Test]
        [TestCase(null)] //cant be null
        [TestCase("")] //cant be empty
        [TestCase("A")] //too short 
        [TestCase("Testvejemeddetretlangenavn")] //too long
        [TestCase("123+")] //contains special character
        public void OriginStreetName_SetInvalidValues_IsInvalid(string setValue)
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

        #region OriginStreetNumber

        [Test]
        [TestCase("1")]//1 character
        [TestCase("123 st. th.")] //Spaces and letters
        [TestCase("1234567 st. th.")]//15 characters
        [TestCase("12 ved åen")] //contains å
        public void OriginStreetNumber_SetValidValues_IsValid(string setValue)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(_uut, null)
            {
                MemberName = "OriginStreetNumber"
            };

            _uut.OriginStreetNumber = setValue;

            Validator.TryValidateProperty(_uut.OriginStreetNumber, validationContext, validationResults);

            Assert.IsTrue(validationResults.Count == 0);
        }

        [Test]
        [TestCase(null)] //cant be null
        [TestCase("")] //cant be empty
        [TestCase("1234567 st. th. nr. 3")] //too long
        public void OriginStreetNumber_SetInvalidValues_IsInvalid(string setValue)
        {
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(_uut, null)
            {
                MemberName = "OriginStreetNumber"
            };

            _uut.OriginStreetNumber = setValue;

            Validator.TryValidateProperty(_uut.OriginStreetNumber, validationContext, validationResults);

            Assert.IsTrue(validationResults.Count > 0);
        }
        #endregion

        #region DestinationStreetNumber
        //same as origin
        #endregion

        #region IsValid

        [Test]
        public void IsValid_RequestIsValid_ReturnsTrue()
        {
            _uut.AmountOfPassengers = 1;
            _uut.OriginCityName = "Aarhus V";
            _uut.OriginPostalCode = "8210";
            _uut.OriginStreetName = "Bispehavevej";
            _uut.OriginStreetNumber = "3";
            _uut.DestinationCityName = "Aarhus C";
            _uut.DestinationPostalCode = "8000";
            _uut.DestinationStreetName = "Banegårdspladsen";
            _uut.DestinationStreetNumber = "1";

            Assert.That(_uut.IsValid);
        }

        [Test]
        public void IsValid_RequestIsInvalid_ReturnsFalse()
        {
            _uut.AmountOfPassengers = 5;
            _uut.OriginCityName = "Aarhus V";
            _uut.OriginPostalCode = "8210";
            _uut.OriginStreetName = "Bispehavevej";
            _uut.OriginStreetNumber = "3";
            _uut.DestinationCityName = "Aarhus C";
            _uut.DestinationPostalCode = "8000";
            _uut.DestinationStreetName = "Banegårdspladsen";
            _uut.DestinationStreetNumber = "1";

            Assert.That(!_uut.IsValid);
        }

        #endregion

        #region IsInvalid

        [Test]
        public void IsInValid_RequestIsValid_ReturnsFalse()
        {
            _uut.AmountOfPassengers = 4;
            _uut.OriginCityName = "Aarhus V";
            _uut.OriginPostalCode = "8210";
            _uut.OriginStreetName = "Bispehavevej";
            _uut.OriginStreetNumber = "3";
            _uut.DestinationCityName = "Aarhus C";
            _uut.DestinationPostalCode = "8000";
            _uut.DestinationStreetName = "Banegårdspladsen";
            _uut.DestinationStreetNumber = "1";

            Assert.That(!_uut.IsInvalid);
        }

        [Test]
        public void IsInValid_RequestIsInvalid_ReturnsTrue()
        {
            _uut.AmountOfPassengers = 5;
            _uut.OriginCityName = "Aarhus V";
            _uut.OriginPostalCode = "8210";
            _uut.OriginStreetName = "Bispehavevej";
            _uut.OriginStreetNumber = "3";
            _uut.DestinationCityName = "Aarhus C";
            _uut.DestinationPostalCode = "8000";
            _uut.DestinationStreetName = "Banegårdspladsen";
            _uut.DestinationStreetNumber = "1";

            Assert.That(_uut.IsInvalid);
        }
        #endregion
    }
}
