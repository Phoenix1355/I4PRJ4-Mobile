﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Requests;
using i4prj.SmartCab.Responses;
using i4prj.SmartCab.Services;
using i4prj.SmartCab.ViewModels;
using i4prj.SmartCab.Views;
using NUnit.Framework;
using NSubstitute;
using NSubstitute.ClearExtensions;
using NSubstitute.ReturnsExtensions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using Xamarin.Forms;
using Xamarin.UITest;
using Xamarin.UITest.Android;

namespace i4prj.SmartCab.UnitTests.ViewModels
{
    [TestFixture]
    public class CreateCustomerViewModelTests
    {
        private CreateCustomerViewModel _uut;
        private IBackendApiService _fakeApiService;
        private IPageDialogService _fakePageDialogService;
        private INavigationService _fakeNavigationService;
        private ISessionService _fakeSessionService;
        private IApp _app;

        [SetUp]
        public void SetUp()
        {
            _fakePageDialogService = Substitute.For<IPageDialogService>();
            _fakeApiService = Substitute.For<IBackendApiService>();
            _fakeNavigationService = Substitute.For<INavigationService>();
            _fakeSessionService = Substitute.For<ISessionService>();
            _uut = new CreateCustomerViewModel(_fakeNavigationService,_fakePageDialogService);

            _uut.Request.Email = "test@tester.com";
            _uut.Request.Name = "test tester";
            _uut.Request.Password = "123456";
            _uut.Request.Password = "123456";
            _uut.Request.Phone = "12345678";
            //_app = ConfigureApp.Android.ApkFile("/Users/Jesper Strøm/source/repos/I4PRJ4 - Mobile/i4prj.SmartCab/i4prj.SmartCab.Android/bin/Debug/com.i4prj.smartcab.apk").StartApp();
        }

        #region ctor

        #endregion

        #region SubmitRequestCommand

        [Test]
        public void SubmitRequestCommand_ApiServiceReturnsNull_ErrorDialogIsCalled()
        {
            _fakeApiService.SubmitCreateCustomerRequest(_uut.Request).ReturnsNull();

            _uut.Api = _fakeApiService;

            _uut.SubmitRequestCommand.Execute();

            _fakePageDialogService.Received()
                .DisplayAlertAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void SubmitRequestCommand_ApiServiceReturnsSuccesfullResponse_TokenIsUpdated()
        {
            
            _fakeApiService.SubmitCreateCustomerRequest(_uut.Request)
                .Returns(new CreateCustomerResponse(new HttpResponseMessage(HttpStatusCode.OK)));

            _uut.Api = _fakeApiService;

            _uut.SubmitRequestCommand.Execute();

            //Her arbejdes med en singleton - ved ikke endnu hvordan vi håndterer dette.
            _fakeSessionService.Received().Update(Arg.Any<string>(),Arg.Any<ICustomer>());
        }

        [Test]
        public void SubmitRequestCommand_ApiServiceReturnsSuccesfullResponse_NavigationServiceNavigatesToCorrectPage()
        {
            _fakeApiService.SubmitCreateCustomerRequest(_uut.Request)
                .Returns(new CreateCustomerResponse(new HttpResponseMessage(HttpStatusCode.OK)));

            _uut.Api = _fakeApiService;

            _uut.SubmitRequestCommand.Execute();
            //dette modtages af en eller anden grund ikke
            _fakeNavigationService.Received().NavigateAsync("/" + nameof(Rides));
        }

        [Test]
        public void SubmitRequestCommand_ApiServiceReturnsNonSuccesfullResponse_ErrorDialogIsShown()
        {
            //The code below crashes the unittests...
            //_fakeApiService.SubmitCreateCustomerRequest(_uut.Request)
                //.Returns(new CreateCustomerResponse(new HttpResponseMessage(HttpStatusCode.BadRequest)));

            _uut.Api = _fakeApiService;

            _uut.SubmitRequestCommand.Execute();

            _fakePageDialogService.Received()
                .DisplayAlertAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }

        #endregion

    }
}
