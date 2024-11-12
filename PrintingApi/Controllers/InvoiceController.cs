using ESCPOS_NET;
using ESCPOS_NET.Emitters;
using ESCPOS_NET.Utilities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PrintingApi.Commands;
using PrintingApi.Model;

namespace PrintingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController:ControllerBase 
    {
        private readonly IMediator mediator;

        public InvoiceController(IMediator mediator)
        {
            this.mediator = mediator;
        }



        [HttpPost]
        public async Task<InvoiceDetails> AddStudentAsync(InvoiceDetails invoice)
        {
            var invoiced = await mediator.Send(new CreateInvoiceCommand(
                invoice.Company, 
                invoice.StreetAdress,
                invoice.CityZipCode,
                invoice.Website,
                invoice.Date,
                invoice.ProductName,
                invoice.Price
                ));


            var hostnameOrIp = "10.35.91.177";
            var port = 9100;
            var printer = new ImmediateNetworkPrinter(new ImmediateNetworkPrinterSettings() { ConnectionString = $"{hostnameOrIp}:{port}", PrinterName = "TestPrinter" });

            var e = new EPSON();
            await printer.WriteAsync(
              ByteSplicer.Combine(
                e.CenterAlign(),
                e.PrintLine($"{invoice.Company}"),
                e.PrintLine($"{invoice.StreetAdress}"),
                e.PrintLine($"{invoice.CityZipCode}"),
                e.SetStyles(PrintStyle.Underline),
                e.PrintLine($"{invoice.Website}"),
                e.SetStyles(PrintStyle.None),
                e.PrintLine(""),
                e.LeftAlign(),
                e.PrintLine($"Order: 123456789        Date: {invoice.Date}"),
                e.PrintLine(""),
                e.PrintLine(""),
                e.SetStyles(PrintStyle.FontB),
                e.PrintLine($"{invoice.ProductName}"),
                e.PrintLine($"                                                 {invoice.Price}"),
                e.PrintLine("----------------------------------------------------------------"),
                e.RightAlign(),
                e.PrintLine($"SUBTOTAL         {invoice.Price}"),
                e.PrintLine($"Total Order:         {invoice.Price}"),
                e.PrintLine($"Total Payment:        {invoice.Price}"),
                e.PrintLine(""),
                e.LeftAlign(),
                e.SetStyles(PrintStyle.Bold | PrintStyle.FontB),
                e.PrintLine("SOLD TO:                        SHIP TO:"),
                e.SetStyles(PrintStyle.FontB),
                e.PrintLine("  FIRSTN LASTNAME                 FIRSTN LASTNAME"),
                e.PrintLine("  123 FAKE ST.                    123 FAKE ST."),
                e.PrintLine("  DECATUR, IL 12345               DECATUR, IL 12345"),
                e.PrintLine("  (123)456-7890                   (123)456-7890"),
                e.PrintLine("  CUST: 87654321"),
                e.PrintLine(""),
                e.PrintLine(""),
                e.CenterAlign(),
                e.PrintQRCode($"{invoice.Website}"),
                e.FullCutAfterFeed(40)
              )
            );

            return invoiced;
        }
    }
}
