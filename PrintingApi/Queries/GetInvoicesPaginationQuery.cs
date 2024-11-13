using MediatR;
using PrintingApi.Model;

namespace PrintingApi.Queries
{
    public class GetInvoicesPaginationQuery : IRequest<PaginatedList<InvoiceDetails>>
    { 
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
}
