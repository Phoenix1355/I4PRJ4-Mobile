using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Castle.Core.Internal;
using i4prj.SmartCab.Interfaces;
using i4prj.SmartCab.Requests;
using i4prj.SmartCab.Responses;
using i4prj.SmartCab.Services;
using i4prj.SmartCab.ViewModels;
using Newtonsoft.Json;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using NUnit.Framework;
using Prism.Navigation;
using Prism.Services;

namespace i4prj.SmartCab.UnitTests.ViewModels
{
    public class MapsViewModelTests
    {

        private RideConfirmationViewModel _uut;
        private IBackendApiService _fakeBackendApiService;
        private INavigationService _fakeNavigationService;
        private IPageDialogService _fakePageDialogService;
        private ISessionService _fakeSessionService;
        private IMapsService _fakeMapsService;
        private INavigationParameters _fakeParameters;
        private CreateRideResponse _rideResponseOk;
        private CreateRideResponse _rideResponseBadRequest;
        private ICreateRideRequest _rideRequestParameter;

        [SetUp]
        public void SetUp()
        {
            _fakeBackendApiService = Substitute.For<IBackendApiService>();
            _fakeNavigationService = Substitute.For<INavigationService>();
            _fakePageDialogService = Substitute.For<IPageDialogService>();
            _fakeSessionService = Substitute.For<ISessionService>();
            _fakeMapsService = Substitute.For<IMapsService>();
            _fakeParameters = Substitute.For<INavigationParameters>();

            _uut= new RideConfirmationViewModel(_fakeNavigationService,_fakePageDialogService,_fakeSessionService,_fakeBackendApiService);
            _uut._mapsService = _fakeMapsService;

            _rideResponseOk = new CreateRideResponse(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    id = 1,
                    startDestination = new { cityName = "Test", postalCode = 1234, streetName = "Tester", streetNumber = 1 },
                    endDestination = new { cityName = "Tester", postalCode = 4321, streetName = "Test", streetNumber = 2 },
                    departureTime = DateTime.Now.Add(new TimeSpan(0, 0, 30)),
                    confirmationDeadline = DateTime.Now.Subtract(new TimeSpan(0, 2, 0)),
                    passengerCount = 1,
                    createdOn = DateTime.Now,
                    price = 100,
                    status = 0,

                }), Encoding.UTF8, "application/json"),
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

        [Test]
        public void OnNavigatedTo_ParametersAreEmpty_MapIsNotSetup()
        {
            _fakeParameters.ContainsKey("Ride").Returns(false);
            _fakeParameters.ContainsKey("Price").Returns(false);

            _uut.OnNavigatedTo(_fakeParameters);
            _fakeMapsService.Received(0).GetPosition(Arg.Any<string>());
        }

        [Test]
        public void OnNavigatedTo_ParametersContainsPriceButNotRequest_MapIsNotSetup()
        {
            _fakeParameters.ContainsKey("Ride").Returns(false);
            _fakeParameters.ContainsKey("Price").Returns(true);

            _uut.OnNavigatedTo(_fakeParameters);
            _fakeMapsService.Received(0).GetPosition(Arg.Any<string>());
        }

        [Test]
        public void OnNavigatedTo_ParametersContainsRequestButNotPrice_MapIsNotSetup()
        {
            _fakeParameters.ContainsKey("Ride").Returns(true);
            _fakeParameters.ContainsKey("Price").Returns(false);

            _uut.OnNavigatedTo(_fakeParameters);
            _fakeMapsService.Received(0).GetPosition(Arg.Any<string>());
        }

        [Test]
        public void OnNavigatedTo_ParametersContainsPriceAndRequest_MapIsSetup()
        {
            CreateRideRequest request = new CreateRideRequest(new TimeService());
            request.DestinationCityName = "Aarhus V";
            request.DestinationPostalCode = "8210";
            request.DestinationStreetName = "Bispehavevej";
            request.DestinationStreetNumber = "1";
            request.OriginCityName = "Aarhus V";
            request.OriginPostalCode = "8210";
            request.OriginStreetName = "Bispehavevej";
            request.OriginStreetNumber = "5";

            _fakeParameters.ContainsKey("Ride").Returns(true);
            _fakeParameters.ContainsKey("Price").Returns(true);
            _fakeParameters.GetValue<CreateRideRequest>("Ride").Returns(request);
            _fakeParameters.GetValue<string>("Price").Returns("100");

            _uut.OnNavigatedTo(_fakeParameters);
            _fakeMapsService.Received(2).GetPosition(Arg.Any<string>());
        }

        [Test]
        public void ConfirmCommand_ApiReturnsNull_DialogServiceShowsMessage()
        {
            _fakeBackendApiService.SubmitCreateRideRequest(_uut.Request).ReturnsNull();
            _uut.ConfirmCommand.Execute();
            _fakePageDialogService.Received().DisplayAlertAsync("Forbindelse", Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void ConfirmCommand_ApiReturnsNull_NavigatesBack()
        {
            _fakeBackendApiService.SubmitCreateRideRequest(_uut.Request).ReturnsNull();
            _uut.ConfirmCommand.Execute();
            _fakeNavigationService.Received().GoBackAsync();
        }

        [Test]
        public void CreateRideCommand_ApiReturnsBadRequest_DialogServiceShowsMessage()
        {
            _fakeBackendApiService.SubmitCreateRideRequest(Arg.Any<ICreateRideRequest>())
                .Returns(_rideResponseBadRequest);
            _uut.ConfirmCommand.Execute();
            _fakePageDialogService.Received().DisplayAlertAsync("Fejl", Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void CreateRideCommand_ApiReturnsBadRequest_NavigatesBack()
        {
            _fakeBackendApiService.SubmitCreateRideRequest(Arg.Any<ICreateRideRequest>())
                .Returns(_rideResponseBadRequest);
            _uut.ConfirmCommand.Execute();
            _fakeNavigationService.Received().GoBackAsync();
        }

        [Test]
        public void CreateRideCommand_ApiReturnsOKResponse_DialogIsShown()
        {
            _fakeBackendApiService.SubmitCreateRideRequest(Arg.Any<ICreateRideRequest>()).Returns(_rideResponseOk);
            _uut.ConfirmCommand.Execute();
            _fakePageDialogService.Received().DisplayAlertAsync("Succes", Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void CreateRideCommand_ApiReturnsOKResponse_ViewNavigatesBack()
        {
            _fakeBackendApiService.SubmitCreateRideRequest(Arg.Any<ICreateRideRequest>()).Returns(_rideResponseOk);
            _uut.ConfirmCommand.Execute();
            _fakeNavigationService.Received().NavigateAsync(Arg.Any<string>());
        }

        [Test]
        public void CanceCommand_Executed_NavigatesBack()
        {
            _uut.CancelCommand.Execute();
            _fakeNavigationService.Received().GoBackAsync();
        }
    }
}
