using PrintingApi.Data;
using PrintingApi.Model;

namespace PrintingApi.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly DbContextClass _dbContext;

        public InvoiceRepository(DbContextClass dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<InvoiceDetails> AddInvoice(InvoiceDetails invoiceDetails)
        {
            var result = _dbContext.Invoices.Add(invoiceDetails); 
            await _dbContext.SaveChangesAsync();     
            return result.Entity;
        }
    }
}
