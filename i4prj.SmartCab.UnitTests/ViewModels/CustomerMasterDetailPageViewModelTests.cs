using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Responses;
using i4prj.SmartCab.ViewModels;
using i4prj.SmartCab.Views;
using Newtonsoft.Json;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;

namespace i4prj.SmartCab.UnitTests.ViewModels
{
    [TestFixture]
    public class CustomerMasterDetailPageViewModelTests
    {

        private INavigationService _fakeNavigationService;
        private IPageDialogService _fakePageDialogService;
        private ISessionService _fakeSessionService;
        private CustomerMasterDetailPageViewModel _uut;

        [SetUp]
        public void SetUp()
        {
            // Fake uut (ViewModel) dependencies
            _fakeNavigationService = Substitute.For<INavigationService>();
            _fakePageDialogService = Substitute.For<IPageDialogService>();
            _fakeSessionService = Substitute.For<ISessionService>();

            // UUT
            _uut = new CustomerMasterDetailPageViewModel(_fakeNavigationService, _fakePageDialogService, _fakeSessionService);
        }

        /// <summary>
        /// Test to see whether a call
        /// </summary>
        [Test]
        public void LogoutCommand_SessionIsCleared()
        {
            // Arrange and act
            _uut.LogOutCommand.Execute();

            // Assert
            _fakeSessionService.Received(1).Clear();
        }

        /// <summary>
        /// Test to see that a successfull login triggers a navigation change
        /// </summary>
        [Test]
        public void LogoutCommand_NavigatesToCorrectPage()
        {
            // Arrange and act
            _uut.LogOutCommand.Execute();

            // Assert
            _fakeNavigationService.Received().NavigateAsync("/" + nameof(NavigationPage) + "/" + nameof(LoginPage));
        }
    }
}
