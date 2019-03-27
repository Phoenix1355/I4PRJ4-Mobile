using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Models;
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
            _uut = LocalSessionService.Instance;
        }

        [Test]
        public void Test()
        {
            
        }
    }
}
