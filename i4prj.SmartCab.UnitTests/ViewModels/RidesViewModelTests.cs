using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
                Content = new StringContent("{\n\t\"rides\": [{\n\t\t\t\"customerId\": \"string\",\n\t\t\t\"departureTime\": \"2019-04-28T10:19:36.259Z\",\n\t\t\t\"startDestination\": {\n\t\t\t\t\"cityName\": \"string\",\n\t\t\t\t\"postalCode\": 0,\n\t\t\t\t\"streetName\": \"string\",\n\t\t\t\t\"streetNumber\": 0\n\t\t\t},\n\t\t\t\"endDestination\": {\n\t\t\t\t\"cityName\": \"string\",\n\t\t\t\t\"postalCode\": 0,\n\t\t\t\t\"streetName\": \"string\",\n\t\t\t\t\"streetNumber\": 0\n\t\t\t},\n\t\t\t\"confirmationDeadline\": \"2019-04-28T10:19:36.259Z\",\n\t\t\t\"passengerCount\": 0,\n\t\t\t\"price\": 0,\n\t\t\t\"status\": \"LookingForMatch\"\n\t\t},\n\t\t{\n\t\t\t\"customerId\": \"string\",\n\t\t\t\"departureTime\": \"2019-04-28T10:19:36.259Z\",\n\t\t\t\"startDestination\": {\n\t\t\t\t\"cityName\": \"string\",\n\t\t\t\t\"postalCode\": 0,\n\t\t\t\t\"streetName\": \"string\",\n\t\t\t\t\"streetNumber\": 0\n\t\t\t},\n\t\t\t\"endDestination\": {\n\t\t\t\t\"cityName\": \"string\",\n\t\t\t\t\"postalCode\": 0,\n\t\t\t\t\"streetName\": \"string\",\n\t\t\t\t\"streetNumber\": 0\n\t\t\t},\n\t\t\t\"confirmationDeadline\": \"2019-04-28T10:19:36.259Z\",\n\t\t\t\"passengerCount\": 0,\n\t\t\t\"price\": 0,\n\t\t\t\"status\": \"LookingForMatch\"\n\t\t},\n\t\t{\n\t\t\t\"customerId\": \"string\",\n\t\t\t\"departureTime\": \"2019-04-28T10:19:36.259Z\",\n\t\t\t\"startDestination\": {\n\t\t\t\t\"cityName\": \"string\",\n\t\t\t\t\"postalCode\": 0,\n\t\t\t\t\"streetName\": \"string\",\n\t\t\t\t\"streetNumber\": 0\n\t\t\t},\n\t\t\t\"endDestination\": {\n\t\t\t\t\"cityName\": \"string\",\n\t\t\t\t\"postalCode\": 0,\n\t\t\t\t\"streetName\": \"string\",\n\t\t\t\t\"streetNumber\": 0\n\t\t\t},\n\t\t\t\"confirmationDeadline\": \"2019-04-28T10:19:36.259Z\",\n\t\t\t\"passengerCount\": 0,\n\t\t\t\"price\": 0,\n\t\t\t\"status\": \"LookingForMatch\"\n\t\t}\n\t]\n}", Encoding.UTF8, "application/json")
            });
            _fakeApiService.GetCustomerRides().Returns(fakeHttpResponse);

            // Act
            _uut.UpdateListCommand.Execute();

            // Assert
            _fakeApiService.Received(1).GetCustomerRides();
        }

        // TODO: Not working for some reason!
        /// <summary>
        /// Test to see if received open rides are populated in bindable Rides property
        /// </summary>
        [Test]
        public void UpdateListCommand_SuccessfulApiCallWithOpenRides_RidesPropertyContainsRidesGroupWithOpenRides()
        {
            // Arrange fake response
            var fakeHttpResponse = new CustomerRidesResponse(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                // Data OK!
                Content = new StringContent("{\n\t\"rides\": [{\n\t\t\"customerId\": \"53a14a7e-4c7d-4bac-9c77-ab018c3e3314\",\n\t\t\"departureTime\": \"2019-05-02T16:06:38\",\n\t\t\"startDestination\": {\n\t\t\t\"cityName\": \"Aarhus C\",\n\t\t\t\"postalCode\": 8000,\n\t\t\t\"streetName\": \"Læssøesgade\",\n\t\t\t\"streetNumber\": 45,\n\t\t\t\"lat\": 0.0,\n\t\t\t\"lng\": 0.0\n\t\t},\n\t\t\"endDestination\": {\n\t\t\t\"cityName\": \"Aarhus C\",\n\t\t\t\"postalCode\": 8000,\n\t\t\t\"streetName\": \"Lundbyesgade\",\n\t\t\t\"streetNumber\": 8,\n\t\t\t\"lat\": 0.0,\n\t\t\t\"lng\": 0.0\n\t\t},\n\t\t\"confirmationDeadline\": \"2019-05-02T15:36:38\",\n\t\t\"passengerCount\": 1,\n\t\t\"price\": 19.33,\n\t\t\"status\": \"LookingForMatch\"\n\t}, {\n\t\t\"customerId\": \"53a14a7e-4c7d-4bac-9c77-ab018c3e3314\",\n\t\t\"departureTime\": \"2019-05-02T17:08:16\",\n\t\t\"startDestination\": {\n\t\t\t\"cityName\": \"Aarhus\",\n\t\t\t\"postalCode\": 8200,\n\t\t\t\"streetName\": \"Finlandsgade\",\n\t\t\t\"streetNumber\": 22,\n\t\t\t\"lat\": 56.1719587,\n\t\t\t\"lng\": 10.1916533\n\t\t},\n\t\t\"endDestination\": {\n\t\t\t\"cityName\": \"Aarhus\",\n\t\t\t\"postalCode\": 8000,\n\t\t\t\"streetName\": \"Banegårdspladsen\",\n\t\t\t\"streetNumber\": 1,\n\t\t\t\"lat\": 56.150356,\n\t\t\t\"lng\": 10.204572\n\t\t},\n\t\t\"confirmationDeadline\": \"2019-05-02T16:38:16\",\n\t\t\"passengerCount\": 2,\n\t\t\"price\": 23.81,\n\t\t\"status\": \"LookingForMatch\"\n\t}]\n}", Encoding.UTF8, "application/json")
            });

            _fakeApiService.GetCustomerRides().Returns(fakeHttpResponse);

            // Act
            _uut.UpdateListCommand.Execute();

            // Assert
            Assert.That(_uut.Rides.Count(), Is.GreaterThan(0));
        }

        // TODO: Not working for some reason!
        /// <summary>
        /// Test to see if received archived rides are populated in bindable Rides property
        /// </summary>
        //[Test]
        public void UpdateListCommand_SuccessfulApiCallWithArchivedRides_RidesPropertyContainsRidesGroupWithArchivedRides()
        {
            // Arrange fake response
            var confirmationDeadline = DateTime.Now.Subtract(TimeSpan.FromHours(1));
            var fakeHttpResponse = new CustomerRidesResponse(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\r\n\t\"rides\": [{\r\n\t\t\t\"customerId\": \"string\",\r\n\t\t\t\"departureTime\": \"2019-04-28T10:19:36.259Z\",\r\n\t\t\t\"startDestination\": {\r\n\t\t\t\t\"cityName\": \"string\",\r\n\t\t\t\t\"postalCode\": 0,\r\n\t\t\t\t\"streetName\": \"string\",\r\n\t\t\t\t\"streetNumber\": 0\r\n\t\t\t},\r\n\t\t\t\"endDestination\": {\r\n\t\t\t\t\"cityName\": \"string\",\r\n\t\t\t\t\"postalCode\": 0,\r\n\t\t\t\t\"streetName\": \"string\",\r\n\t\t\t\t\"streetNumber\": 0\r\n\t\t\t},\r\n\t\t\t\"confirmationDeadline\": \""+ confirmationDeadline + "\",\r\n\t\t\t\"passengerCount\": 0,\r\n\t\t\t\"price\": 0,\r\n\t\t\t\"status\": \"LookingForMatch\"\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"customerId\": \"string\",\r\n\t\t\t\"departureTime\": \"2019-04-28T10:19:36.259Z\",\r\n\t\t\t\"startDestination\": {\r\n\t\t\t\t\"cityName\": \"string\",\r\n\t\t\t\t\"postalCode\": 0,\r\n\t\t\t\t\"streetName\": \"string\",\r\n\t\t\t\t\"streetNumber\": 0\r\n\t\t\t},\r\n\t\t\t\"endDestination\": {\r\n\t\t\t\t\"cityName\": \"string\",\r\n\t\t\t\t\"postalCode\": 0,\r\n\t\t\t\t\"streetName\": \"string\",\r\n\t\t\t\t\"streetNumber\": 0\r\n\t\t\t},\r\n\t\t\t\"confirmationDeadline\": \""+ confirmationDeadline + "\",\r\n\t\t\t\"passengerCount\": 0,\r\n\t\t\t\"price\": 0,\r\n\t\t\t\"status\": \"LookingForMatch\"\r\n\t\t}\r\n\t]\r\n}", Encoding.UTF8, "application/json")
            });

            _fakeApiService.GetCustomerRides().Returns(fakeHttpResponse);

            // Act
            _uut.UpdateListCommand.Execute();

            // Assert
            Assert.That(_uut.Rides.First().Count(), Is.EqualTo(2));
        }

        // TODO: Not working for some reason!
        /// <summary>
        /// Test to see if received mixed rides are populated in bindable Rides property
        /// </summary>
        //[Test]
        public void UpdateListCommand_SuccessfulApiCallWithMixedRides_RidesPropertyContainsRidesGroupsWithMixedRides()
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

        #endregion

        #region OnAppearing

        // Method just delegates to same private handler as used by UpdateListCommand
        // Therefore; tests are the same as UpdateListCommand tests

        #endregion
    }
}
