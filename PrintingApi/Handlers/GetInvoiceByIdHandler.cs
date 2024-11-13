using MediatR;
using PrintingApi.Model;
using PrintingApi.Queries;
using PrintingApi.Repositories;

namespace PrintingApi.Handlers
{
    public class GetInvoiceByIdHandler : IRequestHandler<GetInvoiceByIdQuery,InvoiceDetails>
    {

        private readonly IInvoiceRepository _invoiceRepository;

        public GetInvoiceByIdHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<InvoiceDetails> Handle(GetInvoiceByIdQuery request, CancellationToken cancellationToken)
        {
            return await _invoiceRepository.GetInvoiceById(request.Id);
        }
    }
}
