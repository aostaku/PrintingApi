using ESCPOS_NET;
using ESCPOS_NET.Emitters;
using ESCPOS_NET.Utilities;
using PrintingApi.Model;

namespace PrintingApi.Controllers
{
    public class Printer
    {
        private readonly IConfiguration _configuration;

        public Printer(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task Print(InvoiceDetails invoice)
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
                e.PrintLine($"{invoice.Company}"),
                e.PrintLine($"{invoice.StreetAddress}"),
                e.PrintLine($"{invoice.CityZipCode}"),
                e.SetStyles(PrintStyle.Underline),
                e.PrintLine($"{invoice.Website}"),
                e.SetStyles(PrintStyle.None),
                e.PrintLine(""),
                e.LeftAlign(),
                e.PrintLine($"Order: 145       Date: {invoice.Date}"),
                e.PrintLine(""),
                e.PrintLine(""),
                e.SetStyles(PrintStyle.FontB),
                e.PrintLine($"{invoice.ProductName}"),
                e.PrintLine($"                                        {invoice.Price}"),
                e.PrintLine("--------------------------------------------------------"),
                e.RightAlign(),
                e.PrintLine($"SUBTOTAL         {invoice.Price:C}"),
                e.PrintLine($"Total Order:         {invoice.Price:C}"),
                e.PrintLine($"Total Payment:        {invoice.Price:C}"),
                e.PrintLine(""),
                e.PrintLine(""),
                e.PrintLine(""),
                e.CenterAlign(),
                e.PrintQRCode($"{invoice.Website}"),
                e.FullCutAfterFeed(40)
              )
            );
        }
    }
 
}
