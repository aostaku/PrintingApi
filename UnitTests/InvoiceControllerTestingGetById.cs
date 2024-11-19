using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using PrintingApi.Model;

namespace UnitTests
{
    public class InvoiceControllerTestingGetById : IClassFixture<WebApplicationFactory<PrintingApi.Controllers.InvoiceController>>
    {

        readonly HttpClient _httpClient;

        public InvoiceControllerTestingGetById(WebApplicationFactory<PrintingApi.Controllers.InvoiceController> application)
        {
            _httpClient = application.CreateClient();
        }

        [Fact]
        public async Task GET_RetrievesInvoicesById()
        {
            var newInvoice = TestData.MockSampleInvoice();
            
            var response = await _httpClient.GetAsync($"api/Invoice/{newInvoice.Id}");

            var responseBody = await response.Content.ReadAsStringAsync();
            var invoice = JsonConvert.DeserializeObject<InvoiceDetails>(responseBody);

            Assert.Equal(newInvoice.Id, invoice.Id); 
        }

        
        [Fact]
        public async Task GET_RetrievesNotFoundInvoicesById()
        {
            var newInvoice = TestData.MockSampleInvoiceNotExisting();
            var response = await _httpClient.GetAsync($"api/Invoice/{newInvoice.Id}");
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode); 
        } 
        
        
        
    }
}
