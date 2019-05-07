using i4prj.SmartCab.Services;
using NUnit.Framework;

namespace i4prj.SmartCab.UnitTests.Services
{
    [TestFixture]
    public class JWTServiceTests
    {
        #region GetHeaderValue
        [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJkZW1vQGRlbW8uZGsiLCJVc2VySWQiOiI1M2ExNGE3ZS00YzdkLTRiYWMtOWM3Ny1hYjAxOGMzZTMzMTQiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJDdXN0b21lciIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvZXhwaXJhdGlvbiI6IjA1LzA2LzIwMTkgMTQ6NDA6MDMiLCJleHAiOjE1NTcxNTM2MDN9.5ChG0-RDn6OkHVHkHMm1bRywPmmBeXTp7sMtIj-DsKE", "alg", "HS256")]
        public void GetHeaderValue_FromValidToken_ReturnsCorrectValue(string token, string key, string value)
        {
            // Arrange, Act, Assert
            Assert.That(JWTService.GetHeaderValue(token, key), Is.EqualTo(value));
        }

        [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9===.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJkZW1vQGRlbW8uZGsiLCJVc2VySWQiOiI1M2ExNGE3ZS00YzdkLTRiYWMtOWM3Ny1hYjAxOGMzZTMzMTQiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJDdXN0b21lciIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvZXhwaXJhdGlvbiI6IjA1LzA2LzIwMTkgMTQ6NDA6MDMiLCJleHAiOjE1NTcxNTM2MDN9.5ChG0-RDn6OkHVHkHMm1bRywPmmBeXTp7sMtIj-DsKE===", "exp")]
        public void GetHeaderValue_FromInvalidToken_ReturnsNull(string token, string key)
        {
            // Arrange, Act, Assert
            Assert.That(JWTService.GetHeaderValue(token, key), Is.Null);
        }
        #endregion

        #region GetPayloadValue
        [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJkZW1vQGRlbW8uZGsiLCJVc2VySWQiOiI1M2ExNGE3ZS00YzdkLTRiYWMtOWM3Ny1hYjAxOGMzZTMzMTQiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJDdXN0b21lciIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvZXhwaXJhdGlvbiI6IjA1LzA2LzIwMTkgMTQ6NDA6MDMiLCJleHAiOjE1NTcxNTM2MDN9.5ChG0-RDn6OkHVHkHMm1bRywPmmBeXTp7sMtIj-DsKE", "exp", "1557153603")]
        // The following test case is provided by Jespers crash report before padding was added.
        [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJyZXBzZWouc3Ryb2VtQGdtYWlsLmNvbSIsIlVzZXJJZCI6IjJkOWJhZDY5LTYxM2YtNDEzZS1iNGUyLTRiZTYzZmMwMjY4ZiIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvcm9sZSI6IkN1c3RvbWVyIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy9leHBpcmF0aW9uIjoiMDUvMDYvMjAxOSAxNjoxMjozNyIsImV4cCI6MTU1NzE1OTE1N30.FPkLSS_Fxj5-hk_fm9SSD5tNEbC8z0tg_Q8d_43E2Tw", "exp", "1557159157")]
        public void GetPayloadValue_FromValidToken_ReturnsCorrectValue(string token, string key, string value)
        {
            // Arrange, Act, Assert
            Assert.That(JWTService.GetPayloadValue(token, key), Is.EqualTo(value));
        }

        [TestCase("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9lbWFpbGFkZHJlc3MiOiJkZW1vQGRlbW8uZGsiLCJVc2VySWQiOiI1M2ExNGE3ZS00YzdkLTRiYWMtOWM3Ny1hYjAxOGMzZTMzMTQiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJDdXN0b21lciIsImh0dHA6Ly9zY2hlbWFzLm1pY3Jvc29mdC5jb20vd3MvMjAwOC8wNi9pZGVudGl0eS9jbGFpbXMvZXhwaXJhdGlvbiI6IjA1LzA2LzIwMTkgMTQ6NDA6MDMiLCJleHAiOjE1NTcxNTM2MDN9===.5ChG0-RDn6OkHVHkHMm1bRywPmmBeXTp7sMtIj-DsKE===", "exp")]
        public void GetPayloadValue_FromInvalidToken_ReturnsNull(string token, string key)
        {
            // Arrange, Act, Assert
            Assert.That(JWTService.GetPayloadValue(token, key), Is.Null);
        }
        #endregion
    }
}