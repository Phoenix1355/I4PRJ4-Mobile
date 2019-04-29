using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using i4prj.SmartCab.CustomAttributes;
using NUnit.Framework;
using Xamarin.Forms;

namespace i4prj.SmartCab.UnitTests.CustomAttributes
{
    public class RequiredIfAttributeTests
    {
        #region SetUp

        private ValidationContext _validationContext;
        private RequiredIfAttribute _uut;
        private ValidationTarget _target;

        [SetUp]
        public void SetUp()
        {
            

        }
        #endregion

        #region Ctor

        [Test]
        public void Ctor_InstantiateNewAttribute_PropertyIsSet()
        {
            _uut = new RequiredIfAttribute("Password", "Some error");
            Assert.That(_uut.BoolProperty=="Password");
        }
        #endregion

        #region IsValid

        [Test]
        public void IsValid_EmptyStringAndSwichIsOn_PropertyIsNotValid()
        {

            _target = new ValidationTarget();
            _target.X = true;
            _target.RequiredIfX = "";

            _validationContext =new ValidationContext(_target);

            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(_target, _validationContext,results,true);

            Assert.That(isValid==false);
        }

        [Test]
        public void IsValid_EmptyStringAndSwichIsOff_PropertyIsValid()
        {

            _target = new ValidationTarget();
            _target.X = false;
            _target.RequiredIfX = "";

            _validationContext = new ValidationContext(_target);

            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(_target, _validationContext, results, true);

            Assert.That(isValid == true);
        }

        [Test]
        public void IsValid_StringNotEmptySwitchOn_PropertyIsValid()
        {

            _target = new ValidationTarget();
            _target.X = true;
            _target.RequiredIfX = "Test123!";

            _validationContext = new ValidationContext(_target);

            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(_target, _validationContext, results, true);

            Assert.That(isValid == true);
        }

        private class ValidationTarget
        {
            public bool X { get; set; }

            [RequiredIf(nameof(X),"Some error")]
            public string RequiredIfX { get; set; }
        }
        
        #endregion
    }
}
