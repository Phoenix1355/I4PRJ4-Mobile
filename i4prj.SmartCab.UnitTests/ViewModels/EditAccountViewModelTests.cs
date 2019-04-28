using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Models;
using i4prj.SmartCab.Requests;
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
    public class EditAccountViewModelTests
    {

        #region SetUp
        private EditAccountViewModel _uut;
        private IBackendApiService _fakeBackendApiService;
        private INavigationService _fakeNavigationService;
        private IPageDialogService _fakePageDialogService;
        private ISessionService _fakeSessionService;

        private EditAccountResponse _editAccountResponseOk;
        private EditAccountResponse _editAccountResponseBadRequest;

        [SetUp]
        public void SetUp()
        {
            _fakePageDialogService = Substitute.For<IPageDialogService>();
            _fakeBackendApiService = Substitute.For<IBackendApiService>();
            _fakeNavigationService = Substitute.For<INavigationService>();
            _fakeSessionService = Substitute.For<ISessionService>();

            _editAccountResponseOk = new EditAccountResponse(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    customer= new
                    {
                        name="Jesper Tester",
                        email="repsej.stroem@gmail.com",
                        phoneNumber="23482222",
                    },
                }), Encoding.UTF8, "application/json"),
            });

            _editAccountResponseBadRequest = new EditAccountResponse(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    errors = new Dictionary<string, IList<string>>()
                    {
                        {"error",new List<string>{"The address is not valid"} }
                    },
                }), Encoding.UTF8, "application/json"),
            });
        }
        #endregion
        
        #region Ctor
        

        [Test]
        public void Ctor_InstantiateViewModel_PropertiesAreSet()
        {
            _fakeSessionService.Customer.Email = "Test@test.com";
            _fakeSessionService.Customer.Name = "Test Tester";
            _fakeSessionService.Customer.PhoneNumber = "12345678";

            _uut = new EditAccountViewModel(_fakeNavigationService, _fakePageDialogService, _fakeBackendApiService, _fakeSessionService);

            Assert.Multiple(() =>
            {
                Assert.NotNull(_uut.Request);
                Assert.AreEqual(_uut.Request.Email, "Test@test.com");
                Assert.AreEqual(_uut.Request.Name, "Test Tester");
                Assert.AreEqual(_uut.Request.PhoneNumber, "12345678");
            });
        }
        #endregion

        #region Commands

        [Test]
        public void EditAccountCommand_ResponseEqualsNull_AlertIsShown()
        {
            _uut = new EditAccountViewModel(_fakeNavigationService, _fakePageDialogService, _fakeBackendApiService, _fakeSessionService);
            _fakeBackendApiService.SubmitEditAccountRequest(_uut.Request).ReturnsNull();

            _uut.EditAccountCommand.Execute();

            _fakePageDialogService.Received()
                .DisplayAlertAsync("Forbindelse", Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void EditAccountCommand_ResponseReturnsBadRequest_AlertIsShown()
        {
            _uut = new EditAccountViewModel(_fakeNavigationService, _fakePageDialogService, _fakeBackendApiService, _fakeSessionService);
            _fakeBackendApiService.SubmitEditAccountRequest(_uut.Request).Returns(_editAccountResponseBadRequest);

            _uut.EditAccountCommand.Execute();

            _fakePageDialogService.Received()
                .DisplayAlertAsync("Ukendt fejl", Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void EditAccountCommand_ResponseReturnsOK_UpdateCalled()
        {
            _uut = new EditAccountViewModel(_fakeNavigationService, _fakePageDialogService, _fakeBackendApiService, _fakeSessionService);
            _fakeBackendApiService.SubmitEditAccountRequest(_uut.Request).Returns(_editAccountResponseOk);

            _uut.EditAccountCommand.Execute();

            _fakeSessionService.Received().Update(_fakeSessionService.Token,new Customer(_editAccountResponseOk.Body.customer));
        }

        [Test]
        public void EditAccountCommand_ResponseReturnsOK_DialogShown()
        {
            _uut = new EditAccountViewModel(_fakeNavigationService, _fakePageDialogService, _fakeBackendApiService, _fakeSessionService);
            _fakeBackendApiService.SubmitEditAccountRequest(_uut.Request).Returns(_editAccountResponseOk);

            _uut.EditAccountCommand.Execute();

            _fakePageDialogService.Received().DisplayAlertAsync("Success", Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void EditAccountCommand_ResponseReturnsOK_NavigationCalled()
        {
            _uut = new EditAccountViewModel(_fakeNavigationService, _fakePageDialogService, _fakeBackendApiService, _fakeSessionService);
            _fakeBackendApiService.SubmitEditAccountRequest(_uut.Request).Returns(_editAccountResponseOk);

            _uut.EditAccountCommand.Execute();

            _fakeNavigationService.Received().NavigateAsync("/" + nameof(CustomerMasterDetailPage) + "/" +
                                                            nameof(NavigationPage) + "/" + nameof(RidesPage));
        }

        #endregion 
    }
}
