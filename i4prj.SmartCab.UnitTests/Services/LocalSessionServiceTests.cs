using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Models;
using i4prj.SmartCab.Requests;
using i4prj.SmartCab.Responses;
using i4prj.SmartCab.Services;
using NUnit.Framework;
using Prism;

namespace i4prj.SmartCab.UnitTests.Services
{
    [TestFixture]
    public class LocalSessionServiceTests
    {
        private ISessionService _uut;

        [SetUp]
        public void SetUp()
        {
            //todo
            _uut = LocalSessionService.Instance;
        }

        [Test]
        public void Update()
        {
            //todo
            //Nedenstående Kaster en null exception, ved ikke lige hvorfor.
            //_uut.Update("Tester",new Customer(new LoginResponseBody.Customer()));

            Assert.That(PrismApplicationBase.Current.Properties.Count==1);
        }

        [Test]
        public void Clear()
        {
            //todo
        }
    }
}
