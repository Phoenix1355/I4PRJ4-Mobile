using System;
using System.Net;
using System.Net.Http;
using System.Text;
using i4prj.SmartCab.Responses;
using NUnit.Framework;

namespace i4prj.SmartCab.UnitTests.Responses
{
    /// <summary>
    /// NOTE: LoginResponse is used to test the abstract base class BaseResponse
    /// </summary>
    [TestFixture]
    public class BaseResponseTests
    {
        #region WasSuccessfull
        [Test]
        public void WasSuccessfull_WithSuccessfullResponse_ReturnsTrue()
        {
            // Arrange
            var httpResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\n  \"token\": \"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJkZW1vQGRlbW8uZGsiLCJVc2VySWQiOiI1M2ExNGE3ZS00YzdkLTRiYWMtOWM3Ny1hYjAxOGMzZTMzMTQiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJDdXN0b21lciIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvZXhwaXJhdGlvbiI6IjA1LzA2LzIwMTkgMTQ6NDA6MDMiLCJleHAiOjE1NTcxNTM2MDN9.5ChG0-RDn6OkHVHkHMm1bRywPmmBeXTp7sMtIj-DsKE\",\n  \"customer\": {\n    \"name\": \"Frank Andersen\",\n    \"email\": \"frank@gmail.com\",\n    \"phoneNumber\": \"31133165\"\n  }\n}", Encoding.UTF8, "application/json")
            };

            // Act
            LoginResponse uut = new LoginResponse(httpResponse);

            // Assert
            Assert.That(uut.WasSuccessfull(), Is.True);
        }

        [TestCase(HttpStatusCode.BadRequest)]
        [TestCase(HttpStatusCode.Forbidden)]
        [TestCase(HttpStatusCode.InternalServerError)]
        [TestCase(HttpStatusCode.MethodNotAllowed)]
        [TestCase(HttpStatusCode.NotFound)]
        [TestCase(HttpStatusCode.Moved)]
        public void WasSuccessfull_WithUnsuccessfullResponse_ReturnsFalse(HttpStatusCode code)
        {
            // Arrange
            var httpResponse = new HttpResponseMessage()
            {
                StatusCode = code,
                Content = new StringContent("{\n  \"token\": \"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJkZW1vQGRlbW8uZGsiLCJVc2VySWQiOiI1M2ExNGE3ZS00YzdkLTRiYWMtOWM3Ny1hYjAxOGMzZTMzMTQiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJDdXN0b21lciIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvZXhwaXJhdGlvbiI6IjA1LzA2LzIwMTkgMTQ6NDA6MDMiLCJleHAiOjE1NTcxNTM2MDN9.5ChG0-RDn6OkHVHkHMm1bRywPmmBeXTp7sMtIj-DsKE\",\n  \"customer\": {\n    \"name\": \"Frank Andersen\",\n    \"email\": \"frank@gmail.com\",\n    \"phoneNumber\": \"31133165\"\n  }\n}", Encoding.UTF8, "application/json")
            };

            // Act
            LoginResponse uut = new LoginResponse(httpResponse);

            // Assert
            Assert.That(uut.WasSuccessfull(), Is.False);
        }
        #endregion

        #region WasUnsuccessfull
        [Test]
        public void WasUnsuccessfull_WithSuccessfullResponse_ReturnsFalse()
        {
            // Arrange
            var httpResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\n  \"token\": \"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJkZW1vQGRlbW8uZGsiLCJVc2VySWQiOiI1M2ExNGE3ZS00YzdkLTRiYWMtOWM3Ny1hYjAxOGMzZTMzMTQiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJDdXN0b21lciIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvZXhwaXJhdGlvbiI6IjA1LzA2LzIwMTkgMTQ6NDA6MDMiLCJleHAiOjE1NTcxNTM2MDN9.5ChG0-RDn6OkHVHkHMm1bRywPmmBeXTp7sMtIj-DsKE\",\n  \"customer\": {\n    \"name\": \"Frank Andersen\",\n    \"email\": \"frank@gmail.com\",\n    \"phoneNumber\": \"31133165\"\n  }\n}", Encoding.UTF8, "application/json")
            };

            // Act
            LoginResponse uut = new LoginResponse(httpResponse);

            // Assert
            Assert.That(uut.WasUnsuccessfull(), Is.False);
        }

        [TestCase(HttpStatusCode.BadRequest)]
        [TestCase(HttpStatusCode.Forbidden)]
        [TestCase(HttpStatusCode.InternalServerError)]
        [TestCase(HttpStatusCode.MethodNotAllowed)]
        [TestCase(HttpStatusCode.NotFound)]
        [TestCase(HttpStatusCode.Moved)]
        public void WasUnsuccessfull_WithUnsuccessfullResponse_ReturnsTrue(HttpStatusCode code)
        {
            // Arrange
            var httpResponse = new HttpResponseMessage()
            {
                StatusCode = code,
                Content = new StringContent("{\n  \"token\": \"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJkZW1vQGRlbW8uZGsiLCJVc2VySWQiOiI1M2ExNGE3ZS00YzdkLTRiYWMtOWM3Ny1hYjAxOGMzZTMzMTQiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJDdXN0b21lciIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvZXhwaXJhdGlvbiI6IjA1LzA2LzIwMTkgMTQ6NDA6MDMiLCJleHAiOjE1NTcxNTM2MDN9.5ChG0-RDn6OkHVHkHMm1bRywPmmBeXTp7sMtIj-DsKE\",\n  \"customer\": {\n    \"name\": \"Frank Andersen\",\n    \"email\": \"frank@gmail.com\",\n    \"phoneNumber\": \"31133165\"\n  }\n}", Encoding.UTF8, "application/json")
            };

            // Act
            LoginResponse uut = new LoginResponse(httpResponse);

            // Assert
            Assert.That(uut.WasUnsuccessfull(), Is.True);
        }
        #endregion

        #region WasUnauthorized
        [Test]
        public void WasUnauthorized_WithAuthorizedResponse_ReturnsFalse()
        {
            // Arrange
            var httpResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\n  \"token\": \"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJkZW1vQGRlbW8uZGsiLCJVc2VySWQiOiI1M2ExNGE3ZS00YzdkLTRiYWMtOWM3Ny1hYjAxOGMzZTMzMTQiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJDdXN0b21lciIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvZXhwaXJhdGlvbiI6IjA1LzA2LzIwMTkgMTQ6NDA6MDMiLCJleHAiOjE1NTcxNTM2MDN9.5ChG0-RDn6OkHVHkHMm1bRywPmmBeXTp7sMtIj-DsKE\",\n  \"customer\": {\n    \"name\": \"Frank Andersen\",\n    \"email\": \"frank@gmail.com\",\n    \"phoneNumber\": \"31133165\"\n  }\n}", Encoding.UTF8, "application/json")
            };

            // Act
            LoginResponse uut = new LoginResponse(httpResponse);

            // Assert
            Assert.That(uut.WasUnauthorized(), Is.False);
        }

        [Test]
        public void WasUnauthorized_WithUnauthorizedResponse_ReturnsTrue()
        {
            // Arrange
            var httpResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.Unauthorized,
                Content = new StringContent("{\n  \"token\": \"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJkZW1vQGRlbW8uZGsiLCJVc2VySWQiOiI1M2ExNGE3ZS00YzdkLTRiYWMtOWM3Ny1hYjAxOGMzZTMzMTQiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJDdXN0b21lciIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvZXhwaXJhdGlvbiI6IjA1LzA2LzIwMTkgMTQ6NDA6MDMiLCJleHAiOjE1NTcxNTM2MDN9.5ChG0-RDn6OkHVHkHMm1bRywPmmBeXTp7sMtIj-DsKE\",\n  \"customer\": {\n    \"name\": \"Frank Andersen\",\n    \"email\": \"frank@gmail.com\",\n    \"phoneNumber\": \"31133165\"\n  }\n}", Encoding.UTF8, "application/json")
            };

            // Act
            LoginResponse uut = new LoginResponse(httpResponse);

            // Assert
            Assert.That(uut.WasUnauthorized(), Is.True);
        }
        #endregion

        #region HasErrors
        [Test]
        public void HasErrors_ResponseHasNoErrors_ReturnsFalse()
        {
            // Arrange
            var httpResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\n  \"token\": \"eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJkZW1vQGRlbW8uZGsiLCJVc2VySWQiOiI1M2ExNGE3ZS00YzdkLTRiYWMtOWM3Ny1hYjAxOGMzZTMzMTQiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJDdXN0b21lciIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvZXhwaXJhdGlvbiI6IjA1LzA2LzIwMTkgMTQ6NDA6MDMiLCJleHAiOjE1NTcxNTM2MDN9.5ChG0-RDn6OkHVHkHMm1bRywPmmBeXTp7sMtIj-DsKE\",\n  \"customer\": {\n    \"name\": \"Frank Andersen\",\n    \"email\": \"frank@gmail.com\",\n    \"phoneNumber\": \"31133165\"\n  }\n}", Encoding.UTF8, "application/json")
            };

            // Act
            LoginResponse uut = new LoginResponse(httpResponse);

            // Assert
            Assert.That(uut.HasErrors(), Is.False);
        }

        [Test]
        public void HasErrors_ResponseHasErrors_ReturnsTrue()
        {
            // Arrange
            var httpResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\n\t\"token\": null,\n\t\"customer\": null,\n\t\"errors\": {\n\t\t\"error\": [\"Username already taken\"]\n\t}\n}", Encoding.UTF8, "application/json")
            };

            // Act
            LoginResponse uut = new LoginResponse(httpResponse);

            // Assert
            Assert.That(uut.HasErrors(), Is.True);
        }
        #endregion

        #region GetErrors
        [Test]
        public void GetErrors_ResponseHasMultipleErrors_ReturnsListWithAllErrors()
        {
            // Arrange
            /*
            {
                "token": null,
                "customer": null,
                "errors": {
                    "error": ["Username already taken"],
                    "password": ["Too short", "Invalid format"]
                }
            }           
            */

            var httpResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\n\t\"token\": null,\n\t\"customer\": null,\n\t\"errors\": {\n\t\t\"error\": [\"Username already taken\"],\n\t\t\"password\": [\"Too short\", \"Invalid format\"]\n\t}\n}", Encoding.UTF8, "application/json")
            };

            // Act
            LoginResponse uut = new LoginResponse(httpResponse);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(uut.GetErrors(), Does.Contain("Username already taken"));
                Assert.That(uut.GetErrors(), Does.Contain("Too short"));
                Assert.That(uut.GetErrors(), Does.Contain("Invalid format"));
            });
        }
        #endregion

        #region GetFirstError
        [Test]
        public void GetFirstError_ResponseHasMultipleErrors_ReturnFirstError()
        {
            // Arrange
            /*
            {
                "token": null,
                "customer": null,
                "errors": {
                    "error": ["Username already taken"],
                    "password": ["Too short", "Invalid format"]
                }
            }           
            */

            var httpResponse = new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("{\n\t\"token\": null,\n\t\"customer\": null,\n\t\"errors\": {\n\t\t\"error\": [\"Username already taken\"],\n\t\t\"password\": [\"Too short\", \"Invalid format\"]\n\t}\n}", Encoding.UTF8, "application/json")
            };

            // Act
            LoginResponse uut = new LoginResponse(httpResponse);

            // Assert
            Assert.That(uut.GetFirstError(), Does.Contain("Username already taken"));
        }
        #endregion
    }
}
