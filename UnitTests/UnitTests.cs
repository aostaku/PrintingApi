using Moq;
using PrintingApi.Model;
using PrintingApi.Repositories;



namespace UnitTests
{

    public class UnitTests 
    {

        [Fact]
        public async Task ShouldAddInvoiceDetailsAndReturnCorrectInvoice()
        {
            var mockRepository = new Mock<IInvoiceRepository>();

            mockRepository.Setup(repo => repo.AddInvoice(It.IsAny<InvoiceDetails>()))
           .ReturnsAsync((InvoiceDetails invoice) => new InvoiceDetails
           {
           Id = invoice.Id,
           Company = invoice.Company,
           StreetAddress = invoice.StreetAddress,
           CityZipCode = invoice.CityZipCode,
           Website = invoice.Website,
           Date = invoice.Date,
           ProductName = invoice.ProductName,
           Price = invoice.Price
           });

            var newInvoice = TestData.GetSampleInvoice();
            var result = await mockRepository.Object.AddInvoice(newInvoice);

            Assert.NotNull(result);
            Assert.Equal(newInvoice.Id, result.Id);
            Assert.Equal(newInvoice.Company, result.Company);
            Assert.Equal(newInvoice.StreetAddress, result.StreetAddress);
            Assert.Equal(newInvoice.CityZipCode, result.CityZipCode);
            Assert.Equal(newInvoice.Website, result.Website);
            Assert.Equal(newInvoice.Date, result.Date);
            Assert.Equal(newInvoice.ProductName, result.ProductName);
            Assert.Equal(newInvoice.Price, result.Price);
        }

        [Fact] 
        public async Task InvoiceIsNotNull()
        {

        }

        }
  
}
