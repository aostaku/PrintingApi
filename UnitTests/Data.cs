using PrintingApi.Model;


namespace UnitTests
{
    public static class TestData
        {

        public static InvoiceDetails GetSampleInvoice()
        {
            return new InvoiceDetails
            {
                Id = 1,
                Company = "Axess AG",
                StreetAddress = "Sonyastrasse 18",
                CityZipCode = "5081 Anif",
                Website = "www.teamaxess.com",
                Date = "18/11/2024",
                ProductName = "One Day Ticket",
                Price = 24.6
            };
        }
          
        }
    }

 
