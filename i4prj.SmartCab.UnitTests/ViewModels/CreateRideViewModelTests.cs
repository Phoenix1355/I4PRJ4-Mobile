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

        private PriceResponse _priceResponseOK;

        [SetUp]
        public void SetUp()
        {
            _fakeBackendApiService = Substitute.For<IBackendApiService>();
            _fakeNavigationService = Substitute.For<INavigationService>();
            _fakePageDialogService = Substitute.For<IPageDialogService>();
            _uut=new CreateRideViewModel(_fakeNavigationService,_fakePageDialogService,_fakeBackendApiService);
        }

        #region Commands

        [Test]
        public void CalculatePriceCommand_ApiReturnsNull_DialogServiceShowsMessage()
        {
            _fakeBackendApiService.SubmitCalculatePriceRequest(new CalculatePriceRequest(_uut.Request)).ReturnsNull();
            _uut.CalculatePriceCommand.Execute();
            _fakePageDialogService.Received().DisplayAlertAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }

        [Test]
        public void CalculatePriceCommand_ApiReturnsBadRequest_DialogServiceShowsMessage()
        {
            _priceResponseOK = new PriceResponse(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    errors = new Dictionary<string, IList<string>>() {
                        { "EndAddress.CityName", new List<string>() { "The CityName field is required." }},
                        { "EndAddress.StreetName",new List<string>(){ "The StreetName field is required."}}
                    },
                }), Encoding.UTF8, "application/json")
            });

            _fakeBackendApiService.SubmitCalculatePriceRequest(Arg.Any<ICalculatePriceRequest>())
                .Returns(_priceResponseOK);

            _uut.CalculatePriceCommand.Execute();

            _fakePageDialogService.Received().DisplayAlertAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
        }

        //Denne tester på en måde state, men staten har effekt på viewet.
        //Ved ikke om det er forkert
        [Test]
        public void CalculatePriceCommand_ApiReturnsSuccessfullResponse_PriceIsUpdated()
        {
            /*
            _priceResponseOK=new PriceResponse(new HttpResponseMessage()
            {
                StatusCode =HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(new
                {
                    price=100,
                }), Encoding.UTF8, "application/json")
            });
            */
            //TODO
            
        }




        #endregion
    }
}
