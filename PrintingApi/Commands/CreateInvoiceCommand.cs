using MediatR;
using PrintingApi.Model;

namespace PrintingApi.Commands
{
    public class CreateInvoiceCommand : IRequest<InvoiceDetails>
    {
        public string? Company { get; set; }
        public string? StreetAddress { get; set; }
        public string? CityZipCode { get; set; }

        public string? Website { get; set; }

        public string? Date { get; set; }

        public string? ProductName { get; set; }
        public double? Price { get; set; } 


        public CreateInvoiceCommand(string? company, string? streetAdress, string? cityZipCode, string? website, string? date, string? productName, double? price)
        {
            Company = company;
            StreetAddress = streetAdress;
            CityZipCode = cityZipCode;
            Website = website;
            Date = date;
            ProductName = productName;
            Price = price;
        }
    }
}



