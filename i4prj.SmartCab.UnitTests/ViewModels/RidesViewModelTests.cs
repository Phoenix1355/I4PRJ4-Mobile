using System;
using System.Collections.Generic;
using System.Linq;
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
    public class RidesViewModelTests
    {
        //INavigationService navigationService, IPageDialogService dialogService, IBackendApiService backendApiService, ISessionService sessionService, IRidesService ridesServic
        private INavigationService _fakeNavigationService;
        private IPageDialogService _fakePageDialogService;
        private IBackendApiService _fakeApiService;
        private ISessionService _fakeSessionService;
        private IRidesService _fakeRidesService;
        private RidesViewModel _uut;

        [SetUp]
        public void SetUp()
        {
            _fakeNavigationService = Substitute.For<INavigationService>();
            _fakePageDialogService = Substitute.For<IPageDialogService>();
            _fakeApiService = Substitute.For<IBackendApiService>();
            _fakeSessionService = Substitute.For<ISessionService>();
            _fakeRidesService = Substitute.For<IRidesService>();

            _uut = new RidesViewModel(_fakeNavigationService, _fakePageDialogService, _fakeApiService, _fakeSessionService, _fakeRidesService);
        }

        #region UpdateListCommand

        /// <summary>
        /// Test to see if API is called
        /// </summary>
        [Test]
        public void UpdateListCommand_NoRidesLoaded_ApiCallIsMade()
        {
            // Arrange fake response
            var fakeHttpResponse = new CustomerRidesResponse(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("", Encoding.UTF8, "application/json")
            });
            _fakeApiService.GetCustomerRides().Returns(fakeHttpResponse);

            // Act
            _uut.UpdateListCommand.Execute();

            // Assert
            _fakeApiService.Received(1).GetCustomerRides();
        }

        /// <summary>
        /// Test to see if received open rides are populated in bindable Rides property
        /// </summary>
        [Test]
        public void UpdateListCommand_SuccessfullApiCallWithOpenRides_RidesPropertyContainsRidesGroupWithOpenRides()
        {
            // Arrange fake response
            var confirmationDeadline = DateTime.Now.Add(TimeSpan.FromHours(1));
            var fakeHttpResponse = new CustomerRidesResponse(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\n\t\"rides\": [{\n\t\t\t\"customerId\": \"string\",\n\t\t\t\"departureTime\": \"2019-04-28T10:19:36.259Z\",\n\t\t\t\"startDestination\": {\n\t\t\t\t\"cityName\": \"string\",\n\t\t\t\t\"postalCode\": 0,\n\t\t\t\t\"streetName\": \"string\",\n\t\t\t\t\"streetNumber\": 0\n\t\t\t},\n\t\t\t\"endDestination\": {\n\t\t\t\t\"cityName\": \"string\",\n\t\t\t\t\"postalCode\": 0,\n\t\t\t\t\"streetName\": \"string\",\n\t\t\t\t\"streetNumber\": 0\n\t\t\t},\n\t\t\t\""+confirmationDeadline.ToString()+"\": \"2019-04-28T10:19:36.259Z\",\n\t\t\t\"passengerCount\": 0,\n\t\t\t\"price\": 0,\n\t\t\t\"LookingForMatch\": \"string\"\n\t\t},\n\t\t{\n\t\t\t\"customerId\": \"string\",\n\t\t\t\"departureTime\": \"2019-04-28T10:19:36.259Z\",\n\t\t\t\"startDestination\": {\n\t\t\t\t\"cityName\": \"string\",\n\t\t\t\t\"postalCode\": 0,\n\t\t\t\t\"streetName\": \"string\",\n\t\t\t\t\"streetNumber\": 0\n\t\t\t},\n\t\t\t\"endDestination\": {\n\t\t\t\t\"cityName\": \"string\",\n\t\t\t\t\"postalCode\": 0,\n\t\t\t\t\"streetName\": \"string\",\n\t\t\t\t\"streetNumber\": 0\n\t\t\t},\n\t\t\t\"confirmationDeadline\": \""+confirmationDeadline.ToString()+ "\",\n\t\t\t\"passengerCount\": 0,\n\t\t\t\"price\": 0,\n\t\t\t\"status\": \"LookingForMatch\"\n\t\t}\n\t]\n}", Encoding.UTF8, "application/json")
            });

            _fakeApiService.GetCustomerRides().Returns(fakeHttpResponse);

            // Act
            _uut.UpdateListCommand.Execute();

            // Assert
            Assert.That(_uut.Rides.Count(), Is.EqualTo(1));
        }

        /// <summary>
        /// Test to see if received archived rides are populated in bindable Rides property
        /// </summary>
        [Test]
        public void UpdateListCommand_SuccessfullApiCallWithArchivedRides_RidesPropertyContainsRidesGroupWithArchivedRides()
        {
            // Arrange fake response
            var confirmationDeadline = DateTime.Now.Subtract(TimeSpan.FromHours(1));
            var fakeHttpResponse = new CustomerRidesResponse(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\n\t\"rides\": [{\n\t\t\t\"customerId\": \"string\",\n\t\t\t\"departureTime\": \"2019-04-28T10:19:36.259Z\",\n\t\t\t\"startDestination\": {\n\t\t\t\t\"cityName\": \"string\",\n\t\t\t\t\"postalCode\": 0,\n\t\t\t\t\"streetName\": \"string\",\n\t\t\t\t\"streetNumber\": 0\n\t\t\t},\n\t\t\t\"endDestination\": {\n\t\t\t\t\"cityName\": \"string\",\n\t\t\t\t\"postalCode\": 0,\n\t\t\t\t\"streetName\": \"string\",\n\t\t\t\t\"streetNumber\": 0\n\t\t\t},\n\t\t\t\"" + confirmationDeadline.ToString() + "\": \"2019-04-28T10:19:36.259Z\",\n\t\t\t\"passengerCount\": 0,\n\t\t\t\"price\": 0,\n\t\t\t\"LookingForMatch\": \"string\"\n\t\t},\n\t\t{\n\t\t\t\"customerId\": \"string\",\n\t\t\t\"departureTime\": \"2019-04-28T10:19:36.259Z\",\n\t\t\t\"startDestination\": {\n\t\t\t\t\"cityName\": \"string\",\n\t\t\t\t\"postalCode\": 0,\n\t\t\t\t\"streetName\": \"string\",\n\t\t\t\t\"streetNumber\": 0\n\t\t\t},\n\t\t\t\"endDestination\": {\n\t\t\t\t\"cityName\": \"string\",\n\t\t\t\t\"postalCode\": 0,\n\t\t\t\t\"streetName\": \"string\",\n\t\t\t\t\"streetNumber\": 0\n\t\t\t},\n\t\t\t\"confirmationDeadline\": \"" + confirmationDeadline.ToString() + "\",\n\t\t\t\"passengerCount\": 0,\n\t\t\t\"price\": 0,\n\t\t\t\"status\": \"LookingForMatch\"\n\t\t}\n\t]\n}", Encoding.UTF8, "application/json")
            });

            _fakeApiService.GetCustomerRides().Returns(fakeHttpResponse);

            // Act
            _uut.UpdateListCommand.Execute();

            // Assert
            Assert.That(_uut.Rides[0].Count(), Is.EqualTo(2));
        }

        /// <summary>
        /// Test to see if received mixed rides are populated in bindable Rides property
        /// </summary>
        [Test]
        public void UpdateListCommand_SuccessfullApiCallWithMixedRides_RidesPropertyContainsRidesGroupsWithMixedRides()
        {
            // Arrange fake response
            var openConfirmationDeadline = DateTime.Now.Add(TimeSpan.FromHours(1));
            var archivedConfirmationDeadline = DateTime.Now.Subtract(TimeSpan.FromHours(1));
            var fakeHttpResponse = new CustomerRidesResponse(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\n\t\"rides\": [{\n\t\t\t\"customerId\": \"string\",\n\t\t\t\"departureTime\": \"2019-04-28T10:19:36.259Z\",\n\t\t\t\"startDestination\": {\n\t\t\t\t\"cityName\": \"string\",\n\t\t\t\t\"postalCode\": 0,\n\t\t\t\t\"streetName\": \"string\",\n\t\t\t\t\"streetNumber\": 0\n\t\t\t},\n\t\t\t\"endDestination\": {\n\t\t\t\t\"cityName\": \"string\",\n\t\t\t\t\"postalCode\": 0,\n\t\t\t\t\"streetName\": \"string\",\n\t\t\t\t\"streetNumber\": 0\n\t\t\t},\n\t\t\t\"confirmationDeadline\": \""+openConfirmationDeadline.ToString()+"\",\n\t\t\t\"passengerCount\": 0,\n\t\t\t\"price\": 0,\n\t\t\t\"status\": \"LookingForMatch\"\n\t\t},\n\t\t{\n\t\t\t\"customerId\": \"string\",\n\t\t\t\"departureTime\": \"2019-04-28T10:19:36.259Z\",\n\t\t\t\"startDestination\": {\n\t\t\t\t\"cityName\": \"string\",\n\t\t\t\t\"postalCode\": 0,\n\t\t\t\t\"streetName\": \"string\",\n\t\t\t\t\"streetNumber\": 0\n\t\t\t},\n\t\t\t\"endDestination\": {\n\t\t\t\t\"cityName\": \"string\",\n\t\t\t\t\"postalCode\": 0,\n\t\t\t\t\"streetName\": \"string\",\n\t\t\t\t\"streetNumber\": 0\n\t\t\t},\n\t\t\t\"confirmationDeadline\": \""+archivedConfirmationDeadline.ToString()+"\",\n\t\t\t\"passengerCount\": 0,\n\t\t\t\"price\": 0,\n\t\t\t\"status\": \"LookingForMatch\"\n\t\t},\n\t\t{\n\t\t\t\"customerId\": \"string\",\n\t\t\t\"departureTime\": \"2019-04-28T10:19:36.259Z\",\n\t\t\t\"startDestination\": {\n\t\t\t\t\"cityName\": \"string\",\n\t\t\t\t\"postalCode\": 0,\n\t\t\t\t\"streetName\": \"string\",\n\t\t\t\t\"streetNumber\": 0\n\t\t\t},\n\t\t\t\"endDestination\": {\n\t\t\t\t\"cityName\": \"string\",\n\t\t\t\t\"postalCode\": 0,\n\t\t\t\t\"streetName\": \"string\",\n\t\t\t\t\"streetNumber\": 0\n\t\t\t},\n\t\t\t\"confirmationDeadline\": \""+archivedConfirmationDeadline.ToString()+"\",\n\t\t\t\"passengerCount\": 0,\n\t\t\t\"price\": 0,\n\t\t\t\"status\": \"LookingForMatch\"\n\t\t}\n\t]\n}", Encoding.UTF8, "application/json")
            });

            _fakeApiService.GetCustomerRides().Returns(fakeHttpResponse);

            // Act
            _uut.UpdateListCommand.Execute();

            Assert.Multiple(() => {
                // Assert open rides
                Assert.That(_uut.Rides[0].Count(), Is.EqualTo(1));
                // Assert archived rides
                Assert.That(_uut.Rides[1].Count(), Is.EqualTo(2));
            });
        }

        /// <summary>
        /// Test to see if an authorized API call clears session
        /// </summary>
        [Test]
        public void UpdateListCommand_UnauthorizedApiCall_SessionIsCleared()
        {
            // Arrange fake response
            var fakeHttpResponse = new CustomerRidesResponse(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Content = new StringContent("", Encoding.UTF8, "application/json")
            });

            _fakeApiService.GetCustomerRides().Returns(fakeHttpResponse);

            // Act
            _uut.UpdateListCommand.Execute();

            // Assert
            _fakeSessionService.Received(1).Clear();
        }

        /// <summary>
        /// Updates the list command unauthorized API call navigates to login page
        /// </summary>
        [Test]
        public void UpdateListCommand_UnauthorizedApiCall_NavigatedToLoginPage()
        {
            // Arrange fake response
            var fakeHttpResponse = new CustomerRidesResponse(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Content = new StringContent("", Encoding.UTF8, "application/json")
            });

            _fakeApiService.GetCustomerRides().Returns(fakeHttpResponse);

            // Act
            _uut.UpdateListCommand.Execute();

            // Assert
            _fakeNavigationService.Received(1).NavigateAsync(Arg.Is<string>(x => x.Contains(nameof(LoginPage))));
        }

        #endregion

        #region OnAppearing

        // Method just delegates to same private handler as used by UpdateListCommand
        // Therefore; tests are the same as UpdateListCommand tests

        #endregion
    }
}
