using ESCPOS_NET;
using ESCPOS_NET.Emitters;
using ESCPOS_NET.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PrintingApi.Commands;
using PrintingApi.Model;
using PrintingApi.Queries;

namespace PrintingApi.Controllers
{

    public class Printer
    {
        private readonly IConfiguration _configuration;

        public Printer(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task Print(string company,string street,string city,string website,string date,string productname,double price)
        {
            var hostnameOrIp = _configuration["CustomSettings:PrinterNetwork"];
            var port = _configuration["CustomSettings:PrinterPort"];
            var imageUrl = _configuration["CustomSettings:Logo"];
            byte[] imageBytes;
            using (HttpClient client = new HttpClient())
            {
                imageBytes = await client.GetByteArrayAsync(imageUrl);

            }
            var printer = new ImmediateNetworkPrinter(new ImmediateNetworkPrinterSettings() { ConnectionString = $"{hostnameOrIp}:{port}", PrinterName = "TestPrinter" });
            var e = new EPSON();
            await printer.WriteAsync(
              ByteSplicer.Combine(
                e.CenterAlign(),
                e.PrintImage(imageBytes, true),
                e.PrintLine(""),
                e.PrintLine($"{company}"),
                e.PrintLine($"{street}"),
                e.PrintLine($"{city}"),
                e.SetStyles(PrintStyle.Underline),
                e.PrintLine($"{website}"),
                e.SetStyles(PrintStyle.None),
                e.PrintLine(""),
                e.LeftAlign(),
                e.PrintLine($"Order: 145       Date: {date}"),
                e.PrintLine(""),
                e.PrintLine(""),
                e.SetStyles(PrintStyle.FontB),
                e.PrintLine($"{productname}"),
                e.PrintLine($"                                        {price}"),
                e.PrintLine("--------------------------------------------------------"),
                e.RightAlign(),
                e.PrintLine($"SUBTOTAL         {price}"),
                e.PrintLine($"Total Order:         {price}"),
                e.PrintLine($"Total Payment:        {price}"),
                e.PrintLine(""),
                e.PrintLine(""),
                e.PrintLine(""),
                e.CenterAlign(),
                e.PrintQRCode($"{website}"),
                e.FullCutAfterFeed(40)
              )
            );
        }
    }


    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly Printer _printer; 

        public InvoiceController(IMediator mediator,IConfiguration configuration)
        {
            this.mediator = mediator; 
            _printer = new Printer(configuration);
        }
         

        [HttpPost]
        public async Task<InvoiceDetails> AddInvoiceAsync(InvoiceDetails invoice)
        {
            var invoiced = await mediator.Send(new CreateInvoiceCommand(
                invoice.Company, 
                invoice.StreetAddress,
                invoice.CityZipCode,
                invoice.Website,
                invoice.Date,
                invoice.ProductName,
                invoice.Price
                ));

            await _printer.Print(
                invoice.Company,
                invoice.StreetAddress,
                invoice.CityZipCode,
                invoice.Website,
                invoice.Date,
                invoice.ProductName,
                (double)invoice.Price
                ); 

            return invoiced;
        }

        [HttpGet("invoiceId")]
        public async Task<InvoiceDetails> GetInvoiceById(int invoiceId)
        {
            var invoice = await mediator.Send(new GetInvoiceByIdQuery() { Id = invoiceId });

            await _printer.Print(
            invoice.Company,
            invoice.StreetAddress,
            invoice.CityZipCode,
            invoice.Website,
            invoice.Date,
            invoice.ProductName,
            (double)invoice.Price
            );

            return invoice;
        }


        [HttpGet]
        public async Task<PaginatedList<InvoiceDetails>> GetAllInvoices(int pageIndex,int pageSize)
        {
            var invoices = await mediator.Send(new GetInvoicesPaginationQuery() {PageIndex = pageIndex, PageSize=pageSize} );

            return invoices;
        }
    } 
 
}
