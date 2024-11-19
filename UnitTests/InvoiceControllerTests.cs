using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace UnitTests
{
    public class InvoiceControllerTests : IClassFixture<WebApplicationFactory<PrintingApi.Controllers.InvoiceController>>
    {
        private readonly HttpClient _httpClient;

        public InvoiceControllerTests(WebApplicationFactory<PrintingApi.Controllers.InvoiceController> applicationFactory) {
            _httpClient = applicationFactory.CreateClient();
        }

        [Fact]
        public async Task GET_RetrievesInvoiceByIdShouldBeValid()
        {
            int invoice_id = 1;
            var response = await _httpClient.GetAsync($"api/invoice/{invoice_id}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

        }

        [Fact]
        public async Task GET_RetrievesInvoiceButNoContent()
        {
            int invoice_id = 9999; 
            var response = await _httpClient.GetAsync($"api/invoice/{invoice_id}");
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }


    }
}
