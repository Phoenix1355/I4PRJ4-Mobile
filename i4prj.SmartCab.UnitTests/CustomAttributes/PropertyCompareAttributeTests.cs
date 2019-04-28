using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using i4prj.SmartCab.CustomAttributes;
using i4prj.SmartCab.Requests;
using NUnit.Framework;

namespace i4prj.SmartCab.UnitTests.CustomAttributes
{
    public class PropertyCompareAttributeTests
    {
        #region SetUp
        private ValidationContext _validationContext;
        private PropertyCompareAttribute _uut;
        private ValidationTargetWithTrue _targetTrue;
        private ValidationTargetWithFalse _targetFalse;

        [SetUp]
        public void SetUp()
        {


        }
        #endregion

        #region Ctor
        [Test]
        public void Ctor_InstantiateAttribute_PropertiesAreSet()
        {
            _uut=new PropertyCompareAttribute(true,"SomeProperty","SomeOtherProperty","Some Error Message");
            Assert.Multiple(() =>
            {
                Assert.AreEqual(_uut.FirstValue, "SomeProperty");
                Assert.AreEqual(_uut.PropertiesShallBeEqual,true);
                Assert.AreEqual(_uut.SecondValue,"SomeOtherProperty");
            });
        }
        #endregion

        #region IsValid

        [Test]
        public void IsValid_ShouldBeEqualAndPropertiesAreEqual_PropertyIsValid()
        {

            _targetTrue = new ValidationTargetWithTrue();
            _targetTrue.EqualAttribute = "Some string";
            _targetTrue.OtherValue = "Some string";

            _validationContext = new ValidationContext(_targetTrue);

            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(_targetTrue, _validationContext, results, true);

            Assert.That(isValid == true);
        }

        [Test]
        public void IsValid_ShouldBeEqualAndPropertiesAreNotEqual_PropertyIsInvalid()
        {

            _targetTrue = new ValidationTargetWithTrue();
            _targetTrue.EqualAttribute = "Some string";
            _targetTrue.OtherValue = "Some other string";

            _validationContext = new ValidationContext(_targetTrue);

            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(_targetTrue, _validationContext, results, true);

            Assert.That(isValid == false);
        }

        [Test]
        public void IsValid_ShouldNotBeEqualAndPropertiesAreEqual_PropertyIsInvalid()
        {

            _targetFalse = new ValidationTargetWithFalse();
            _targetFalse.EqualAttribute = "Some string";
            _targetFalse.OtherValue = "Some string";

            _validationContext = new ValidationContext(_targetFalse);

            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(_targetFalse, _validationContext, results, true);

            Assert.That(isValid == false);
        }

        [Test]
        public void IsValid_ShouldNotBeEqualAndPropertiesAreNotEqual_PropertyIsValid()
        {

            _targetFalse = new ValidationTargetWithFalse();
            _targetFalse.EqualAttribute = "Some string";
            _targetFalse.OtherValue = "Some other string";

            _validationContext = new ValidationContext(_targetFalse);

            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(_targetFalse, _validationContext, results, true);

            Assert.That(isValid == true);
        }

        [Test]
        public void IsValid_ShouldNotBeEqualAndPropertyIsEmpty_PropertyIsValid()
        {

            _targetFalse = new ValidationTargetWithFalse();
            _targetFalse.EqualAttribute = "";
            _targetFalse.OtherValue = "";

            _validationContext = new ValidationContext(_targetFalse);

            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(_targetFalse, _validationContext, results, true);

            Assert.That(isValid == true);
        }
        #endregion

        private class ValidationTargetWithTrue
        {
            public bool ShouldBeEqual { get; set; }
            public string OtherValue { get; set; }

            [PropertyCompare(true,nameof(OtherValue),nameof(EqualAttribute),"Some Error")]
            public string EqualAttribute { get; set; }
        }

        private class ValidationTargetWithFalse
        {
            public bool ShouldBeEqual { get; set; }
            public string OtherValue { get; set; }

            [PropertyCompare(false, nameof(OtherValue), nameof(EqualAttribute), "Some Error")]
            public string EqualAttribute { get; set; }
        }
    }
}
