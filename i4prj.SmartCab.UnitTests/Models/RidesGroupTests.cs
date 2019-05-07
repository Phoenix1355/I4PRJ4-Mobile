using i4prj.SmartCab.Models;
using NUnit.Framework;

namespace i4prj.SmartCab.UnitTests.Models
{
    [TestFixture]
    public class RidesGroupTests
    {
        #region ctor

        [TestCase("some title")]
        [TestCase("another title")]
        public void Ctor_WithTitleParameter_SetsTitleProperty(string title)
        {
            // Arrange and Act
            var uut = new RidesGroup(title);

            // Assert
            Assert.That(uut.Title, Is.EqualTo(title));
        }
        #endregion
    }
}