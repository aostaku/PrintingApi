using MediatR;
using PrintingApi.Model;
using PrintingApi.Queries;
using PrintingApi.Repositories;

namespace PrintingApi.Handlers
{
    public class GetInvoicePaginationHandler : IRequestHandler<GetInvoicesPaginationQuery,PaginatedList<InvoiceDetails>>
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public GetInvoicePaginationHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<PaginatedList<InvoiceDetails>> Handle(GetInvoicesPaginationQuery request, CancellationToken cancellationToken)
        {
            return await _invoiceRepository.GetAllInvoices(request.PageIndex,request.PageSize);
        }
    }
}
