using MediatR;
using PrintingApi.Model;

namespace PrintingApi.Queries
{
    public class GetInvoiceByIdQuery : IRequest<InvoiceDetails>
    { 
        public int Id { get; set; }
    }
}
