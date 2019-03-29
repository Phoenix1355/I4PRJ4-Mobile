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
            //todo
            //Vil gerne en injecte instansen af singleton, men det lader sig ikke gøre.
        }

        [Test]
        public void Test()
        {
            //todo
            //Kan ikke finde en god måde at teste Singleton klassen på.
        }
    }
}
