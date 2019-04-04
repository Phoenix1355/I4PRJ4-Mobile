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
    public class CreateCustomerViewModelTests
    {

        private INavigationService _fakeNavigationService;
        private IPageDialogService _fakePageDialogService;
        private IBackendApiService _fakeApiService;
        private ISessionService _fakeSessionService;
        private CreateCustomerViewModel _uut;

        private CreateCustomerResponse _fakeHttpCreateCustomerSuccessResponse;
        private CreateCustomerResponse _fakeHttpCreateCustomerBadRequestResponse;

        [SetUp]
        public void SetUp()
        {
            // INavigationService navigationService, IPageDialogService dialogService, IBackendApiService backendApiService, ISessionService sessionService
            _fakeNavigationService = Substitute.For<INavigationService>();
            _fakePageDialogService = Substitute.For<IPageDialogService>();
            _fakeApiService = Substitute.For<IBackendApiService>();
            _fakeSessionService = Substitute.For<ISessionService>();

            _uut = new CreateCustomerViewModel(_fakeNavigationService, _fakePageDialogService, _fakeApiService, _fakeSessionService);

            _fakeHttpCreateCustomerSuccessResponse = new CreateCustomerResponse(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    token = "Some Valid Token",
                    customer = new
                    {
                        name = "Some name",
                        email = "email@somehost.com",
                        phone = "12345678"

                    }
                }), Encoding.UTF8, "application/json")
            });

            _fakeHttpCreateCustomerBadRequestResponse = new CreateCustomerResponse(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    errors = new Dictionary<string, IList<string>>() {
                        { "error", new List<string>() { "User name is already taken" } }
                    },

                }), Encoding.UTF8, "application/json")
            });

            _uut.Request.Email = "test@tester.com";
            _uut.Request.Name = "test tester";
            _uut.Request.Password = "123456";
            _uut.Request.Password = "123456";
            _uut.Request.Phone = "12345678";
        }

        [Test]
        public void SubmitRequestCommand_ApiServiceReturnsNull_ErrorDialogIsCalled()
        {
            // Arrange
            _fakeApiService.SubmitCreateCustomerRequest(_uut.Request).ReturnsNull();

            // Act
            _uut.SubmitRequestCommand.Execute();

            // Assert
            _fakePageDialogService.Received().DisplayAlertAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void SubmitRequestCommand_ApiServiceReturnsSuccesfullResponse_TokenIsUpdated()
        {
            // Arrange
            _fakeApiService.SubmitCreateCustomerRequest(Arg.Any<ICreateCustomerRequest>()).Returns(_fakeHttpCreateCustomerSuccessResponse);

            // Act
            _uut.SubmitRequestCommand.Execute();

            // Assert
            _fakeSessionService.Received().Update(Arg.Any<string>(), Arg.Any<ICustomer>());
        }

        [Test]
        public void SubmitRequestCommand_ApiServiceReturnsSuccesfullResponse_NavigationServiceNavigatesToCorrectPage()
        {
            // Arrange
            _fakeApiService.SubmitCreateCustomerRequest(Arg.Any<ICreateCustomerRequest>()).Returns(_fakeHttpCreateCustomerSuccessResponse);

            // Act
            _uut.SubmitRequestCommand.Execute();

            // Assert
            _fakeNavigationService.Received().NavigateAsync("/" + nameof(CustomerMasterDetailPage) + "/" + nameof(NavigationPage) + "/" + nameof(RidesPage));
        }

        [Test]
        public void SubmitRequestCommand_ApiServiceReturnsNonSuccesfullResponse_ErrorDialogIsShown()
        {
            // Arrange
            _fakeApiService.SubmitCreateCustomerRequest(Arg.Any<ICreateCustomerRequest>()).Returns(_fakeHttpCreateCustomerBadRequestResponse);

            // Act
            _uut.SubmitRequestCommand.Execute();

            // Assert
            _fakePageDialogService.Received().DisplayAlertAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }
    }
}
