using MediatR;
using PrintingApi.Model;

namespace PrintingApi.Commands
{
    public class CreateInvoiceCommand : IRequest<InvoiceDetails>
    {
        public string? Company { get; set; }
        public string? StreetAdress { get; set; }
        public string? CityZipCode { get; set; }

        public string? Website { get; set; }

        public DateTime? Date { get; set; }

        public string? ProductName { get; set; }
        public double? Price { get; set; } 


        public CreateInvoiceCommand(string? company, string? streetAdress, string? cityZipCode, string? website, DateTime? date, string? productName, double? price)
        {
            Company = company;
            StreetAdress = streetAdress;
            CityZipCode = cityZipCode;
            Website = website;
            Date = date;
            ProductName = productName;
            Price = price;
        }
    }
}



