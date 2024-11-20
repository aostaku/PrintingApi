using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using PrintingApi.Model;
namespace UnitTests
{
 
    public class InvoiceControllerTestingGetById : BaseControllerTest<PrintingApi.Controllers.InvoiceController>
    {
        public InvoiceControllerTestingGetById(WebApplicationFactory<PrintingApi.Controllers.InvoiceController> application)
            : base(application)
        {
        }

        [Fact]
        public async Task GET_RetrievesInvoicesById()
        {
            var mockedInvoice = TestData.MockSampleInvoice(); 
            try
            {
                var response = await _httpClient.GetAsync($"api/Invoice/{mockedInvoice.Id}");
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                var invoice = JsonConvert.DeserializeObject<InvoiceDetails>(responseBody);
                Assert.NotNull(invoice);
                Assert.Equal(mockedInvoice.Id, invoice.Id);
            }
            catch (HttpRequestException ex)
            {
                Assert.Fail($"{ex.Message}");
            }
        }


        [Fact]
        public async Task GET_RetrievesNotFoundInvoicesById()
        {
            var mockedInvoice = TestData.MockSampleInvoiceNotExisting();

            try
            {
                var response = await _httpClient.GetAsync($"api/Invoice/{mockedInvoice.Id}");
                Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            }
            catch (HttpRequestException ex)
            {
                
                Assert.Fail($"{ex.Message}");
            }
        }
        
        
        
    }
}