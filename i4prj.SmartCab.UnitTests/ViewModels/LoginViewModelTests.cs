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
    public class LoginViewModelTests
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

        /// <summary>
        /// Test to see whether the request is handed of to BackendApiService
        /// </summary>
        [Test]
        public void LoginCommand_BackendApiIsCalledWithRequest()
        {
            // Arrange and act
            _uut.LoginCommand.Execute();

            // Assert
            _fakeApiService.Received().SubmitLoginRequest(Arg.Any<ILoginRequest>());
        }

        /// <summary>
        /// Test to see whether an error dialog is displayed when no internet connection is available
        /// </summary>
        [Test]
        public void LoginCommand_NoInternetConnection_ErrorDialogIsCalled()
        {
            // Arrange
            _fakeApiService.SubmitLoginRequest(_uut.Request).ReturnsNull();

            // Act
            _uut.LoginCommand.Execute();

            // Assert
            _fakePageDialogService.Received().DisplayAlertAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }

        /// <summary>
        /// Test to see whether an error dialog is displayed when login credentials are incorrect
        /// </summary>
        [Test]
        public void LoginCommand_InvalidCredentials_ErrorDialogIsCalled()
        {
            // Arrange request response
            var fakeRequestReponse = new LoginResponse(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent("{\n\t\"token\": null,\n\t\"customer\": null,\n\t\"errors\": {\n\t\t\"error\": [\"Bad credentials\"]\n\t}\n}", Encoding.UTF8, "application/json")
            });

            _fakeApiService.SubmitLoginRequest(Arg.Any<ILoginRequest>()).Returns(fakeRequestReponse);

            // Act
            _uut.LoginCommand.Execute();

            // Assert
            _fakePageDialogService.Received().DisplayAlertAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }

        /// <summary>
        /// Test to see that a successfull login triggers a navigation change
        /// </summary>
        [Test]
        public void LoginCommand_ValidCredentials_NavigatesToCorrectPage()
        {
            // Arrange request response
            var fakeRequestReponse = new LoginResponse(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\n  \"token\": \"my token\",\n  \"customer\": {\n    \"name\": \"customer name\",\n    \"email\": \"some@email.com\",\n    \"phoneNumber\": \"12345678\"\n  }\n}", Encoding.UTF8, "application/json")
            });

            _fakeApiService.SubmitLoginRequest(Arg.Any<ILoginRequest>()).Returns(fakeRequestReponse);

            // Act
            _uut.LoginCommand.Execute();

            // Assert
            _fakeNavigationService.Received().NavigateAsync("/" + nameof(CustomerMasterDetailPage) + "/" + nameof(NavigationPage) + "/" + nameof(RidesPage));
        }

        /// <summary>
        /// Test to see that a successfull login updates the session
        /// </summary>
        [Test]
        [TestCase("bkjdgFdhDTjFDh43hrhrH5%")]
        [TestCase("ShSdhSHjhsdRJ454y5EYR4")]
        [TestCase("sdgSDhWEhJKnBM535/h")]
        public void LoginCommand_ValidCredentials_UpdatesSession(string token)
        {
            // Arrange request response
            var fakeRequestReponse = new LoginResponse(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\n  \"token\": \"" + token + "\",\n  \"customer\": {\n    \"name\": \"customer name\",\n    \"email\": \"some@email.com\",\n    \"phoneNumber\": \"12345678\"\n  }\n}", Encoding.UTF8, "application/json")
            });

            _fakeApiService.SubmitLoginRequest(Arg.Any<ILoginRequest>()).Returns(fakeRequestReponse);

            // Act
            _uut.LoginCommand.Execute();

            // Assert
            _fakeSessionService.Received().Update(Arg.Is(token), Arg.Any<ICustomer>());
        }
    }
}
