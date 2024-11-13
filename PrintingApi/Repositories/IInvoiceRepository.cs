using PrintingApi.Model;

namespace PrintingApi.Repositories
{
    public interface IInvoiceRepository
    {

        public Task<InvoiceDetails> AddInvoice(InvoiceDetails invoiceDetails);
   


    }
}
