using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.ViewModels;
using NSubstitute;
using NUnit.Framework;
using Prism.Navigation;
using Prism.Services;

namespace i4prj.SmartCab.UnitTests.ViewModels
{
    // Using LoginViewModel to test this abstract class

    [TestFixture]
    public class ViewModelBaseTests
    {
        private INavigationService _fakeNavigationService;
        private IPageDialogService _fakePageDialogService;
        private IBackendApiService _fakeApiService;
        private ISessionService _fakeSessionService;
        private LoginViewModel _uut;

        [SetUp]
        public void SetUp()
        {
            // Fake uut (ViewModel) dependencies
            _fakeNavigationService = Substitute.For<INavigationService>();
            _fakePageDialogService = Substitute.For<IPageDialogService>();
            _fakeApiService = Substitute.For<IBackendApiService>();
            _fakeSessionService = Substitute.For<ISessionService>();

            // UUT
            _uut = new LoginViewModel(_fakeNavigationService, _fakePageDialogService, _fakeApiService, _fakeSessionService);
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsReadyProperty_SetWithValue_HasSetValue(bool value)
        {
            // Act
            _uut.IsReady = value;

            // Assert
            Assert.That(_uut.IsReady, Is.EqualTo(value));
        }

        [TestCase(true)]
        [TestCase(false)]
        public void IsBusyProperty_SetWithValue_HasSetValue(bool value)
        {
            // Act
            _uut.IsBusy = value;

            // Assert
            Assert.That(_uut.IsBusy, Is.EqualTo(value));
        }

        [TestCase("some title")]
        [TestCase("another title")]
        public void TitleProperty_SetWithValue_HasSetValue(string title)
        {
            // Act
            _uut.Title = title;

            // Assert
            Assert.That(_uut.Title, Is.EqualTo(title));
        }
    }
}