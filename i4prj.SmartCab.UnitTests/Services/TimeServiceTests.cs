using System;
using System.Collections.Generic;
using System.Text;
using i4prj.SmartCab.Services;
using NUnit.Framework;

namespace i4prj.SmartCab.UnitTests.Services
{
    [TestFixture]
    public class TimeServiceTests
    {

        private TimeService _uut;

        [SetUp]
        public void SetUp()
        {
            _uut=new TimeService();
        }

        [TestCase(5,30,7,45,13,15)]
        [TestCase(22,50,1,9,23,59)]
        [TestCase(0,0,23,59,23,59)]
        public void AddTimeSpans_AddWithNoOverflow_OverflowIsFalseAndTimeIsRight(int t1Hours, int t1Minutes, int t2Hours, int t2Minutes,int resultHours, int resultMinutes)
        {
            TimeSpan t1 = new TimeSpan(t1Hours,t1Minutes,0);
            TimeSpan t2 = new TimeSpan(t2Hours, t2Minutes, 0);

            bool overflow = false;

            TimeSpan result = _uut.AddTimeSpans(t1, t2, ref overflow);

            Assert.Multiple((() =>
            {
                Assert.AreEqual(overflow,false);
                Assert.AreEqual(result.Hours,resultHours  );
                Assert.AreEqual(result.Minutes, resultMinutes);
            }));
        }

        [TestCase(1,10,22,50,0,0)]
        [TestCase(23,59,23,59,23,58)]
        [TestCase(0,1,23,59,0,0)]
        [TestCase(15,15,15,15,6,30)]
        public void AddTimeSpans_AddWithOverflow_OverflowIsTrueAndTimeIsRight(int t1Hours, int t1Minutes, int t2Hours, int t2Minutes,int resultHours, int resultMinutes)
        {
            TimeSpan t1 = new TimeSpan(t1Hours,t1Minutes,0);
            TimeSpan t2 = new TimeSpan(t2Hours, t2Minutes, 0);

            bool overflow = false;

            TimeSpan result = _uut.AddTimeSpans(t1, t2, ref overflow);

            Assert.Multiple((() =>
            {
                Assert.AreEqual(overflow,true);
                Assert.AreEqual(result.Hours,resultHours  );
                Assert.AreEqual(result.Minutes, resultMinutes);
            }));
        }
    }
}
