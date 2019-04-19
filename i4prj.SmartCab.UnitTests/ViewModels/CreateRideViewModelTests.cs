using System;
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

        private PriceResponse _priceResponseOk;
        private PriceResponse _priceResponseBadRequest;
        private CreateRideResponse _rideResponseOk;
        private CreateRideResponse _rideResponseBadRequest;

        [SetUp]
        public void SetUp()
        {
            _fakeBackendApiService = Substitute.For<IBackendApiService>();
            _fakeNavigationService = Substitute.For<INavigationService>();
            _fakePageDialogService = Substitute.For<IPageDialogService>();
            _uut=new CreateRideViewModel(_fakeNavigationService,_fakePageDialogService,_fakeBackendApiService);

            
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

            _rideResponseOk=new CreateRideResponse(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    id=1,
                    startDestination = new {cityName="Test",postalCode=1234,streetName="Tester",streetNumber=1},
                    endDestination = new { cityName = "Tester", postalCode = 4321, streetName = "Test", streetNumber = 2 },
                    departureTime=DateTime.Now.Add(new TimeSpan(0,0,30)),
                    confirmationDeadline=DateTime.Now.Subtract(new TimeSpan(0,2,0)),
                    passengerCount=1,
                    createdOn=DateTime.Now,
                    price=100,
                    status=0,

                }),Encoding.UTF8,"application/json"),
            });

            _rideResponseBadRequest = new CreateRideResponse(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    errors = new Dictionary<string, IList<string>>()
                    {
                        {"error",new List<string>{"Not enough money"} }
                    },
                }), Encoding.UTF8, "application/json"),
            });
        }

        #region Commands

        [Test]
        public void CalculatePriceCommand_ApiReturnsNull_DialogServiceShowsMessage()
        {
            _fakeBackendApiService.SubmitCalculatePriceRequest(new CalculatePriceRequest(_uut.Request)).ReturnsNull();
            _uut.CalculatePriceCommand.Execute();
            _fakePageDialogService.Received().DisplayAlertAsync("Forbindelse", Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void CalculatePriceCommand_ApiReturnsBadRequest_DialogServiceShowsMessage()
        {

            _fakeBackendApiService.SubmitCalculatePriceRequest(Arg.Any<ICalculatePriceRequest>())
                .Returns(_priceResponseBadRequest);

            _uut.CalculatePriceCommand.Execute();

            _fakePageDialogService.Received().DisplayAlertAsync("Ukendt fejl", Arg.Any<string>(), Arg.Any<string>());
        }

        //Denne tester på en måde state, men staten har effekt på viewet, så føler det er relevant alligevel
        [Test]
        public void CalculatePriceCommand_ApiReturnsSuccessfullResponse_PriceIsUpdated()
        {
            _fakeBackendApiService.SubmitCalculatePriceRequest(Arg.Any<ICalculatePriceRequest>())
                .Returns(_priceResponseOk);
            _uut.CalculatePriceCommand.Execute();
            Assert.That(_uut.Price == _priceResponseOk.Body.Price);

        }

        [Test]
        public void CreateRideCommand_ApiReturnsNull_DialogServiceShowsMessage()
        {
            _fakeBackendApiService.SubmitCreateRideRequest(_uut.Request).ReturnsNull();
            _uut.CreateRideCommand.Execute();
            _fakePageDialogService.Received().DisplayAlertAsync("Forbindelse", Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void CreateRideCommand_ApiReturnsBadRequest_DialogServiceShowsMessage()
        {
            _fakeBackendApiService.SubmitCreateRideRequest(Arg.Any<ICreateRideRequest>())
                .Returns(_rideResponseBadRequest);
            _uut.CreateRideCommand.Execute();
            _fakePageDialogService.Received().DisplayAlertAsync("Fejl", Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void CreateRideCommand_ApiReturnsOKResponse_DialogIsShown()
        {
            _fakeBackendApiService.SubmitCreateRideRequest(Arg.Any<ICreateRideRequest>()).Returns(_rideResponseOk);
            _uut.CreateRideCommand.Execute();
            _fakePageDialogService.Received().DisplayAlertAsync("Success", Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void CreateRideCommand_ApiReturnsOKResponse_ViewNavigatesBack()
        {
            _fakeBackendApiService.SubmitCreateRideRequest(Arg.Any<ICreateRideRequest>()).Returns(_rideResponseOk);
            _uut.CreateRideCommand.Execute();
            _fakeNavigationService.Received().GoBackAsync();
        }


        #endregion
    }
}
