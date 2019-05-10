﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Requests;
using i4prj.SmartCab.Responses;
using i4prj.SmartCab.Services;
using i4prj.SmartCab.ViewModels;
using Newtonsoft.Json;
using NSubstitute;
using NSubstitute.Core.Arguments;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using Prism.Navigation;
using Prism.Services;

namespace i4prj.SmartCab.UnitTests.ViewModels
{
    [TestFixture]
    public class CreateRideViewModelTests
    {
        private CreateRideViewModel _uut;
        private IBackendApiService _fakeBackendApiService;
        private INavigationService _fakeNavigationService;
        private IPageDialogService _fakePageDialogService;
        private ISessionService _fakeSessionService;

        private PriceResponse _priceResponseOk;
        private PriceResponse _priceResponseBadRequest;

        [SetUp]
        public void SetUp()
        {
            _fakeBackendApiService = Substitute.For<IBackendApiService>();
            _fakeNavigationService = Substitute.For<INavigationService>();
            _fakePageDialogService = Substitute.For<IPageDialogService>();
            _fakeSessionService = Substitute.For<ISessionService>();
            _uut = new CreateRideViewModel(_fakeNavigationService, _fakePageDialogService, _fakeSessionService, _fakeBackendApiService);
            _uut.Request.AmountOfPassengers = 1;
            _uut.Request.DestinationCityName = "Aarhus V";
            _uut.Request.DestinationPostalCode = "8210";
            _uut.Request.DestinationStreetName = "Bispehavevej";
            _uut.Request.DestinationStreetNumber = "1";
            _uut.Request.OriginCityName = "Aarhus V";
            _uut.Request.OriginPostalCode = "8210";
            _uut.Request.OriginStreetName = "Bispehavevej";
            _uut.Request.OriginStreetNumber = "5";

            _priceResponseOk = new PriceResponse(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    price=100.00,
                }),Encoding.UTF8,"application/json"),
            });

            _priceResponseBadRequest = new PriceResponse(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    errors=new Dictionary<string, IList<string>>()
                    {
                        {"error",new List<string>{"The address is not valid"} }
                    },
                }), Encoding.UTF8, "application/json"),
            });
        }

        #region Commands

        [Test]
        public void CalculatePriceCommand_RequestIsInvalid_NothingHappens()
        {
            _uut.Request=new CreateRideRequest(new TimeService());
            _fakeBackendApiService.SubmitCalculatePriceRequest(new CalculatePriceRequest(_uut.Request)).ReturnsNull();
            _uut.CalculatePriceCommand.Execute();
            _fakeBackendApiService.DidNotReceive().SubmitCalculatePriceRequest(Arg.Any<CalculatePriceRequest>());
        }

        [Test]
        public void CalculatePriceCommand_ApiReturnsNull_DialogServiceShowsMessage()
        {
            _fakeBackendApiService.SubmitCalculatePriceRequest(new CalculatePriceRequest(_uut.Request)).ReturnsNull();
            _uut.CalculatePriceCommand.Execute();
            _fakePageDialogService.Received().DisplayAlertAsync("Forbindelse", Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void CalculatePriceCommand_ApiReturnsNull_RideInfoCleared()
        {
            _fakeBackendApiService.SubmitCalculatePriceRequest(new CalculatePriceRequest(_uut.Request)).ReturnsNull();
            _uut.CalculatePriceCommand.Execute();
            Assert.That(!_uut.RideInfo.ContainsKey("Price"));
            Assert.That(!_uut.RideInfo.ContainsKey("Ride"));
        }


        [Test]
        public void CalculatePriceCommand_ApiReturnsBadRequest_DialogServiceShowsMessage()
        {

            _fakeBackendApiService.SubmitCalculatePriceRequest(Arg.Any<ICalculatePriceRequest>())
                .Returns(_priceResponseBadRequest);

            _uut.CalculatePriceCommand.Execute();

            _fakePageDialogService.Received().DisplayAlertAsync("Ukendt fejl", Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void CalculatePriceCommand_ApiReturnsBadRequest_RideInfoCleared()
        {

            _fakeBackendApiService.SubmitCalculatePriceRequest(Arg.Any<ICalculatePriceRequest>())
                .Returns(_priceResponseBadRequest);

            _uut.CalculatePriceCommand.Execute();

            Assert.That(!_uut.RideInfo.ContainsKey("Price"));
            Assert.That(!_uut.RideInfo.ContainsKey("Ride"));
        }

        [Test]
        public void CalculatePriceCommand_ApiReturnsOkRequest_RideInfoUpdated()
        {
            _fakeBackendApiService.SubmitCalculatePriceRequest(Arg.Any<ICalculatePriceRequest>())
                .Returns(_priceResponseOk);

            _uut.CalculatePriceCommand.Execute();

            Assert.That(_uut.RideInfo.ContainsKey("Ride"));
            Assert.That(_uut.RideInfo.ContainsKey("Price"));
        }

        [Test]
        public void CreatRideCommand_Executed_ViewNavigatedToMapsPage()
        {
            _uut.CreateRideCommand.Execute();

            _fakeNavigationService.Received().NavigateAsync(Arg.Any<string>(),_uut.RideInfo);
        }
       
        #endregion

    }
}
