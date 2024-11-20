using Microsoft.AspNetCore.Mvc.Testing;

namespace UnitTests
{
    public abstract class BaseControllerTest<TController> : IClassFixture<WebApplicationFactory<TController>> where TController : class
    {
        protected readonly HttpClient _httpClient;
        protected BaseControllerTest(WebApplicationFactory<TController> application)
        {
            _httpClient = application.CreateClient();
        }
    }

}