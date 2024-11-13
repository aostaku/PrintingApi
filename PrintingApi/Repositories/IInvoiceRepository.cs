using PrintingApi.Model;

namespace PrintingApi.Repositories
{
    public interface IInvoiceRepository
    {

        public Task<InvoiceDetails> AddInvoice(InvoiceDetails invoiceDetails);
        public Task<InvoiceDetails> GetInvoiceById(int Id); 

        public Task<PaginatedList<InvoiceDetails>> GetAllInvoices(int pageIndex,int PageSize);

    }
}
