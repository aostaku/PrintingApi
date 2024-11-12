using MediatR;
using PrintingApi.Commands;
using PrintingApi.Model;
using PrintingApi.Repositories;

namespace PrintingApi.Handlers
{
    public class CreateInvoiceHandler : IRequestHandler<CreateInvoiceCommand,InvoiceDetails>
    { 
        private readonly IInvoiceRepository _invoiceRepository;

        public CreateInvoiceHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<InvoiceDetails> Handle(CreateInvoiceCommand command, CancellationToken cancellationToken)
        {
            var invoicedetails = new InvoiceDetails()
            {
                Company = command.Company, 
                StreetAdress = command.StreetAdress,
                CityZipCode = command.CityZipCode,
                Date = command.Date,
                Price = command.Price,
                ProductName = command.ProductName,
                Website = command.Website,
            }; 

            return await _invoiceRepository.AddInvoice(invoicedetails);
        }
    }
}
